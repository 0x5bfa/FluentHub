# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$SubmissionMetadataPath,

    [Parameter(Mandatory)]
    [string]$Repository,

    [Parameter(Mandatory)]
    [string]$GitHubToken,

    [Parameter(Mandatory)]
    [string]$SourceSha,

    [string]$SourcePullRequest = "",

    [Parameter(Mandatory)]
    [string]$WorkflowRunUrl,

    [string]$Environment = "microsoft-store-certification"
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

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
        $parameters.Body = $Body | ConvertTo-Json -Depth 20 -Compress
    }

    return Invoke-RestMethod @parameters
}

$metadataPath = [IO.Path]::GetFullPath($SubmissionMetadataPath)

if (-not [IO.File]::Exists($metadataPath))
{
    Write-Host "No committed Store submission metadata was produced; no certification deployment is required."
    return
}

if ($Repository -notmatch '^[^/]+/[^/]+$')
{
    throw "The GitHub repository must use the 'owner/name' format."
}

if (-not [string]::IsNullOrWhiteSpace($SourcePullRequest) -and
    $SourcePullRequest -notmatch '^[1-9]\d*$')
{
    throw "The source pull request must be a positive integer."
}

$metadata = Get-Content -LiteralPath $metadataPath -Raw | ConvertFrom-Json -Depth 20

foreach ($propertyName in @("ProductId", "SubmissionId", "Status", "SubmittedAtUtc"))
{
    $property = $metadata.PSObject.Properties[$propertyName]

    if ($null -eq $property -or [string]::IsNullOrWhiteSpace([string]$property.Value))
    {
        throw "The Store submission metadata does not contain '$propertyName'."
    }
}

$partnerCenterUrl = if ([string]::IsNullOrWhiteSpace([string]$metadata.FlightId))
{
    "https://partner.microsoft.com/dashboard/products/$($metadata.ProductId)/submissions/$($metadata.SubmissionId)"
}
else
{
    "https://partner.microsoft.com/dashboard/products/$($metadata.ProductId)/flights/$($metadata.FlightId)/submissions/$($metadata.SubmissionId)"
}

$deploymentPayload = [ordered]@{
    product_id = [string]$metadata.ProductId
    submission_id = [string]$metadata.SubmissionId
    flight_id = [string]$metadata.FlightId
    source_pr = $SourcePullRequest
    workflow_run_url = $WorkflowRunUrl
    submitted_at_utc = [string]$metadata.SubmittedAtUtc
}

$deployment = Invoke-GitHubApi `
    -Method "POST" `
    -Path "/repos/$Repository/deployments" `
    -Body @{
        ref = $SourceSha
        task = "deploy"
        auto_merge = $false
        required_contexts = @()
        environment = $Environment
        description = "Microsoft Store certification"
        transient_environment = $false
        production_environment = $true
        payload = $deploymentPayload
    }

if ($null -eq $deployment -or $null -eq $deployment.id)
{
    throw "GitHub did not create a certification deployment."
}

Invoke-GitHubApi `
    -Method "POST" `
    -Path "/repos/$Repository/deployments/$($deployment.id)/statuses" `
    -Body @{
        state = "in_progress"
        description = "Microsoft Store status: $($metadata.Status)"
        log_url = $WorkflowRunUrl
        environment_url = $partnerCenterUrl
        auto_inactive = $false
    } | Out-Null

if (-not [string]::IsNullOrWhiteSpace($env:GITHUB_OUTPUT))
{
    "deployment_id=$($deployment.id)" >> $env:GITHUB_OUTPUT
    "submission_id=$($metadata.SubmissionId)" >> $env:GITHUB_OUTPUT
}

Write-Host "Registered GitHub deployment $($deployment.id) for Store submission '$($metadata.SubmissionId)'."
