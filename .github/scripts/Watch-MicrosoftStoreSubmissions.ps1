# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$Repository,

    [Parameter(Mandatory)]
    [string]$GitHubToken,

    [Parameter(Mandatory)]
    [string]$PartnerCenterTenantId,

    [Parameter(Mandatory)]
    [string]$PartnerCenterClientId,

    [Parameter(Mandatory)]
    [string]$PartnerCenterClientSecret,

    [string]$Mention = "0x5bfa",

    [string]$Environment = "microsoft-store-certification"
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$terminalFailureStatuses = @(
    "Canceled",
    "CommitFailed",
    "PreProcessingFailed",
    "CertificationFailed",
    "ReleaseFailed",
    "PublishFailed"
)

function Get-PropertyValue
{
    param(
        [AllowNull()]
        [psobject]$InputObject,

        [Parameter(Mandatory)]
        [string]$Name
    )

    if ($null -eq $InputObject)
    {
        return $null
    }

    $property = $InputObject.PSObject.Properties |
        Where-Object { $_.Name.Equals($Name, [StringComparison]::OrdinalIgnoreCase) } |
        Select-Object -First 1

    if ($null -eq $property)
    {
        return $null
    }

    return $property.Value
}

function Invoke-GitHubApi
{
    param(
        [Parameter(Mandatory)]
        [ValidateSet("GET", "POST")]
        [string]$Method,

        [Parameter(Mandatory)]
        [string]$Path,

        [AllowNull()]
        [object]$Body
    )

    $parameters = @{
        Method = $Method
        Uri = "https://api.github.com$Path"
        Headers = @{
            Accept = "application/vnd.github+json"
            Authorization = "Bearer $GitHubToken"
            "X-GitHub-Api-Version" = "2022-11-28"
        }
    }

    if ($null -ne $Body)
    {
        $parameters.ContentType = "application/json"
        $parameters.Body = $Body | ConvertTo-Json -Depth 30 -Compress
    }

    return Invoke-RestMethod @parameters
}

function New-DeploymentStatus
{
    param(
        [Parameter(Mandatory)]
        [long]$DeploymentId,

        [Parameter(Mandatory)]
        [ValidateSet("in_progress", "success", "failure")]
        [string]$State,

        [Parameter(Mandatory)]
        [string]$Description,

        [Parameter(Mandatory)]
        [string]$LogUrl,

        [Parameter(Mandatory)]
        [string]$EnvironmentUrl
    )

    Invoke-GitHubApi `
        -Method "POST" `
        -Path "/repos/$Repository/deployments/$DeploymentId/statuses" `
        -Body @{
            state = $State
            description = $Description
            log_url = $LogUrl
            environment_url = $EnvironmentUrl
            auto_inactive = $false
        } | Out-Null
}

function Test-PullRequestCommentExists
{
    param(
        [Parameter(Mandatory)]
        [string]$PullRequest,

        [Parameter(Mandatory)]
        [string]$Marker
    )

    $comments = @(
        Invoke-GitHubApi `
            -Method "GET" `
            -Path "/repos/$Repository/issues/$PullRequest/comments?per_page=100" `
            -Body $null
    )

    return $null -ne ($comments |
        Where-Object { [string]$_.body -like "*$Marker*" } |
        Select-Object -First 1)
}

function Find-FailureIssue
{
    param(
        [Parameter(Mandatory)]
        [string]$SubmissionId,

        [Parameter(Mandatory)]
        [string]$Marker
    )

    $query = [Uri]::EscapeDataString("repo:$Repository is:issue in:body $SubmissionId")
    $searchResult = Invoke-GitHubApi `
        -Method "GET" `
        -Path "/search/issues?q=$query&per_page=100" `
        -Body $null
    $items = @(Get-PropertyValue -InputObject $searchResult -Name "items")

    return $items |
        Where-Object { [string]$_.body -like "*$Marker*" } |
        Select-Object -First 1
}

function ConvertTo-StatusDetailsMarkdown
{
    param(
        [AllowNull()]
        [object]$StatusDetails
    )

    if ($null -eq $StatusDetails)
    {
        return "Partner Center did not return status details."
    }

    $json = $StatusDetails | ConvertTo-Json -Depth 30

    if ($json.Length -gt 30000)
    {
        $json = $json.Substring(0, 30000) + "`n... truncated"
    }

    return "``````json`n$json`n``````"
}

if ($Repository -notmatch '^[^/]+/[^/]+$')
{
    throw "The GitHub repository must use the 'owner/name' format."
}

$mentionLogin = $Mention.Trim().TrimStart('@')

if ([string]::IsNullOrWhiteSpace($mentionLogin))
{
    throw "The GitHub mention cannot be empty."
}

$tokenResponse = Invoke-RestMethod `
    -Method "POST" `
    -Uri "https://login.microsoftonline.com/$PartnerCenterTenantId/oauth2/token" `
    -ContentType "application/x-www-form-urlencoded" `
    -Body @{
        grant_type = "client_credentials"
        client_id = $PartnerCenterClientId
        client_secret = $PartnerCenterClientSecret
        resource = "https://manage.devcenter.microsoft.com"
    }
$accessToken = [string](Get-PropertyValue -InputObject $tokenResponse -Name "access_token")

if ([string]::IsNullOrWhiteSpace($accessToken))
{
    throw "Microsoft Entra ID did not return an access token."
}

$encodedEnvironment = [Uri]::EscapeDataString($Environment)
$deployments = @(
    Invoke-GitHubApi `
        -Method "GET" `
        -Path "/repos/$Repository/deployments?environment=$encodedEnvironment&per_page=100" `
        -Body $null
)
$errors = [Collections.Generic.List[string]]::new()
$activeDeploymentCount = 0

foreach ($deployment in $deployments)
{
    try
    {
        $statuses = @(
            Invoke-GitHubApi `
                -Method "GET" `
                -Path "/repos/$Repository/deployments/$($deployment.id)/statuses?per_page=1" `
                -Body $null
        )
        $latestDeploymentStatus = $statuses | Select-Object -First 1

        if ($null -eq $latestDeploymentStatus -or
            [string]$latestDeploymentStatus.state -notin @("queued", "in_progress"))
        {
            continue
        }

        $activeDeploymentCount++
        $payload = $deployment.payload

        if ($payload -is [string])
        {
            $payload = $payload | ConvertFrom-Json -Depth 20
        }

        $productId = [string](Get-PropertyValue -InputObject $payload -Name "product_id")
        $submissionId = [string](Get-PropertyValue -InputObject $payload -Name "submission_id")
        $flightId = [string](Get-PropertyValue -InputObject $payload -Name "flight_id")
        $sourcePullRequest = [string](Get-PropertyValue -InputObject $payload -Name "source_pr")
        $workflowRunUrl = [string](Get-PropertyValue -InputObject $payload -Name "workflow_run_url")

        if ([string]::IsNullOrWhiteSpace($productId) -or
            [string]::IsNullOrWhiteSpace($submissionId))
        {
            throw "Deployment $($deployment.id) does not contain Store submission identifiers."
        }

        $statusPath = if ([string]::IsNullOrWhiteSpace($flightId))
        {
            "/v1.0/my/applications/$productId/submissions/$submissionId/status"
        }
        else
        {
            "/v1.0/my/applications/$productId/flights/$flightId/submissions/$submissionId/status"
        }
        $storeStatusResponse = Invoke-RestMethod `
            -Method "GET" `
            -Uri "https://manage.devcenter.microsoft.com$statusPath" `
            -Headers @{ Authorization = "Bearer $accessToken" }
        $storeStatus = [string](Get-PropertyValue -InputObject $storeStatusResponse -Name "status")
        $statusDetails = Get-PropertyValue -InputObject $storeStatusResponse -Name "statusDetails"

        if ([string]::IsNullOrWhiteSpace($storeStatus))
        {
            throw "Partner Center did not return a status for submission '$submissionId'."
        }

        $partnerCenterUrl = if ([string]::IsNullOrWhiteSpace($flightId))
        {
            "https://partner.microsoft.com/dashboard/products/$productId/submissions/$submissionId"
        }
        else
        {
            "https://partner.microsoft.com/dashboard/products/$productId/flights/$flightId/submissions/$submissionId"
        }
        $marker = "<!-- microsoft-store-submission:$submissionId -->"
        $detailsMarkdown = ConvertTo-StatusDetailsMarkdown -StatusDetails $statusDetails
        $runLink = if ([string]::IsNullOrWhiteSpace($workflowRunUrl))
        {
            ""
        }
        else
        {
            "- [Submission workflow]($workflowRunUrl)`n"
        }

        if ($storeStatus -eq "Published")
        {
            if (-not [string]::IsNullOrWhiteSpace($sourcePullRequest) -and
                -not (Test-PullRequestCommentExists `
                    -PullRequest $sourcePullRequest `
                    -Marker $marker))
            {
                $commentBody = @"
$marker
@$mentionLogin Microsoft Store submission `$submissionId` has been published successfully.

- [Partner Center]($partnerCenterUrl)
$runLink
"@.Trim()

                Invoke-GitHubApi `
                    -Method "POST" `
                    -Path "/repos/$Repository/issues/$sourcePullRequest/comments" `
                    -Body @{ body = $commentBody } | Out-Null
            }

            New-DeploymentStatus `
                -DeploymentId $deployment.id `
                -State "success" `
                -Description "Microsoft Store status: Published" `
                -LogUrl $workflowRunUrl `
                -EnvironmentUrl $partnerCenterUrl

            Write-Host "Store submission '$submissionId' was published."
            continue
        }

        if ($storeStatus -in $terminalFailureStatuses)
        {
            $failureIssue = Find-FailureIssue `
                -SubmissionId $submissionId `
                -Marker $marker

            if ($null -eq $failureIssue)
            {
                $issueBody = @"
$marker
@$mentionLogin Microsoft Store submission `$submissionId` ended with **$storeStatus**.

- [Partner Center]($partnerCenterUrl)
$runLink
<details>
<summary>Partner Center status details</summary>

$detailsMarkdown

</details>
"@.Trim()

                $failureIssue = Invoke-GitHubApi `
                    -Method "POST" `
                    -Path "/repos/$Repository/issues" `
                    -Body @{
                        title = "Microsoft Store submission failed: $storeStatus ($submissionId)"
                        body = $issueBody
                        assignees = @($mentionLogin)
                    }
            }

            if (-not [string]::IsNullOrWhiteSpace($sourcePullRequest) -and
                -not (Test-PullRequestCommentExists `
                    -PullRequest $sourcePullRequest `
                    -Marker $marker))
            {
                $commentBody = @"
$marker
@$mentionLogin Microsoft Store submission `$submissionId` ended with **$storeStatus**.

Tracking issue: $($failureIssue.html_url)
"@.Trim()

                Invoke-GitHubApi `
                    -Method "POST" `
                    -Path "/repos/$Repository/issues/$sourcePullRequest/comments" `
                    -Body @{ body = $commentBody } | Out-Null
            }

            New-DeploymentStatus `
                -DeploymentId $deployment.id `
                -State "failure" `
                -Description "Microsoft Store status: $storeStatus" `
                -LogUrl $workflowRunUrl `
                -EnvironmentUrl $partnerCenterUrl

            Write-Host "Store submission '$submissionId' failed with status '$storeStatus'."
            continue
        }

        $expectedDescription = "Microsoft Store status: $storeStatus"

        if ([string]$latestDeploymentStatus.description -ne $expectedDescription)
        {
            New-DeploymentStatus `
                -DeploymentId $deployment.id `
                -State "in_progress" `
                -Description $expectedDescription `
                -LogUrl $workflowRunUrl `
                -EnvironmentUrl $partnerCenterUrl
        }

        Write-Host "Store submission '$submissionId' remains in '$storeStatus'."
    }
    catch
    {
        $errors.Add("Deployment $($deployment.id): $($_.Exception.Message)")
    }
}

Write-Host "Checked $activeDeploymentCount active Microsoft Store certification deployment(s)."

if ($errors.Count -gt 0)
{
    throw ($errors -join [Environment]::NewLine)
}
