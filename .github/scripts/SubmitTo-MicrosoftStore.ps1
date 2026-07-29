# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$StorePackagePath,

    [Parameter(Mandatory)]
    [string]$StoreListingsPath,

    [Parameter(Mandatory)]
    [string]$StoreLocalesPath,

    [Parameter(Mandatory)]
    [string]$PartnerCenterClientId,

    [Parameter(Mandatory)]
    [string]$PartnerCenterClientSecret,

    [Parameter(Mandatory)]
    [string]$PartnerCenterSellerId,

    [Parameter(Mandatory)]
    [string]$PartnerCenterStoreId,

    [Parameter(Mandatory)]
    [string]$PartnerCenterTenantId,

    [bool]$Submit = $false,

    [bool]$ReplaceExistingDraft = $false,

    [string]$ReleaseNotes = "",

    [string]$FlightId = "",

    [string]$PackageRolloutPercentage = ""
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

function Invoke-MsStore
{
    param(
        [Parameter(Mandatory)]
        [string[]]$Arguments
    )

    & msstore @Arguments

    if ($LASTEXITCODE -ne 0)
    {
        throw "msstore $($Arguments[0]) failed with exit code $LASTEXITCODE."
    }
}

function Invoke-MsStoreJson
{
    param(
        [Parameter(Mandatory)]
        [string[]]$Arguments
    )

    $output = & msstore @Arguments | Out-String

    if ($LASTEXITCODE -ne 0)
    {
        throw "msstore $($Arguments[0]) failed with exit code $LASTEXITCODE."
    }

    $jsonStart = $output.IndexOf("{", [StringComparison]::Ordinal)
    $jsonEnd = $output.LastIndexOf("}", [StringComparison]::Ordinal)

    if ($jsonStart -lt 0 -or $jsonEnd -lt $jsonStart)
    {
        throw "msstore $($Arguments[0]) did not return a JSON object."
    }

    return $output.Substring($jsonStart, $jsonEnd - $jsonStart + 1)
}

function Test-PendingSubmission
{
    if ([string]::IsNullOrWhiteSpace($FlightId))
    {
        $applicationJson = Invoke-MsStoreJson -Arguments @(
            "apps",
            "get",
            $PartnerCenterStoreId
        )
        $application = $applicationJson | ConvertFrom-Json -Depth 100
        $pendingSubmission = $application.PSObject.Properties["pendingApplicationSubmission"]
        return $null -ne $pendingSubmission -and $null -ne $pendingSubmission.Value
    }

    $flightJson = Invoke-MsStoreJson -Arguments @(
        "flights",
        "get",
        $PartnerCenterStoreId,
        $FlightId
    )
    $flight = $flightJson | ConvertFrom-Json -Depth 100
    $pendingSubmission = $flight.PSObject.Properties["pendingFlightSubmission"]
    return $null -ne $pendingSubmission -and $null -ne $pendingSubmission.Value
}

$requiredValues = @{
    PartnerCenterClientId       = $PartnerCenterClientId
    PartnerCenterClientSecret   = $PartnerCenterClientSecret
    PartnerCenterSellerId       = $PartnerCenterSellerId
    PartnerCenterStoreId        = $PartnerCenterStoreId
    PartnerCenterTenantId       = $PartnerCenterTenantId
}

foreach ($requiredValue in $requiredValues.GetEnumerator())
{
    if ([string]::IsNullOrWhiteSpace($requiredValue.Value))
    {
        throw "Missing required Microsoft Store publish value: $($requiredValue.Key)."
    }
}

if (-not (Test-Path -LiteralPath $StorePackagePath -PathType Leaf))
{
    throw "The Store package does not exist: $StorePackagePath"
}

if (-not (Test-Path -LiteralPath $StoreListingsPath -PathType Container))
{
    throw "The Store listings directory does not exist: $StoreListingsPath"
}

if (-not (Test-Path -LiteralPath $StoreLocalesPath -PathType Leaf))
{
    throw "The Store locales file does not exist: $StoreLocalesPath"
}

Invoke-MsStore -Arguments @(
    "reconfigure",
    "--tenantId",
    $PartnerCenterTenantId,
    "--sellerId",
    $PartnerCenterSellerId,
    "--clientId",
    $PartnerCenterClientId,
    "--clientSecret",
    $PartnerCenterClientSecret
)

$hasPendingSubmission = Test-PendingSubmission

if ($hasPendingSubmission -and -not $ReplaceExistingDraft)
{
    throw @"
Partner Center already has a pending submission for this target.
The Microsoft Store CLI replaces pending submissions when publishing a package.
Review or remove the draft in Partner Center, or rerun with ReplaceExistingDraft enabled.
"@
}

if ($hasPendingSubmission)
{
    Write-Warning "The existing Partner Center draft will be replaced."
}

$publishArgs = @(
    "publish",
    $StorePackagePath,
    "--appId",
    $PartnerCenterStoreId,
    "--noCommit"
)

if (-not [string]::IsNullOrWhiteSpace($FlightId))
{
    $publishArgs += @("--flightId", $FlightId)
}

if (-not [string]::IsNullOrWhiteSpace($PackageRolloutPercentage))
{
    $publishArgs += @("--packageRolloutPercentage", $PackageRolloutPercentage)
}

Write-Host "Uploading package '$StorePackagePath' to Microsoft Store app '$PartnerCenterStoreId'."
Invoke-MsStore -Arguments $publishArgs

if (-not [string]::IsNullOrWhiteSpace($FlightId))
{
    if (-not $Submit)
    {
        Write-Host "The flight submission is ready and will remain in draft state."
        return
    }

    Invoke-MsStore -Arguments @(
        "flights",
        "submission",
        "publish",
        $PartnerCenterStoreId,
        $FlightId
    )
    Invoke-MsStore -Arguments @(
        "flights",
        "submission",
        "poll",
        $PartnerCenterStoreId,
        $FlightId
    )
    return
}

$tempRoot = [IO.Path]::GetFullPath([IO.Path]::GetTempPath())
$tempPath = Join-Path $tempRoot "FluentHub-MicrosoftStore-$([Guid]::NewGuid().ToString('N'))"
[IO.Directory]::CreateDirectory($tempPath) | Out-Null

try
{
    $submissionPath = Join-Path $tempPath "submission.json"
    $updatedSubmissionPath = Join-Path $tempPath "submission.updated.json"
    $submissionJson = Invoke-MsStoreJson -Arguments @(
        "submission",
        "get",
        $PartnerCenterStoreId
    )
    [IO.File]::WriteAllText(
        $submissionPath,
        $submissionJson + [Environment]::NewLine,
        [Text.UTF8Encoding]::new($false)
    )

    $mergeScriptPath = Join-Path $PSScriptRoot "Merge-MicrosoftStoreListings.ps1"
    & $mergeScriptPath `
        -SubmissionPath $submissionPath `
        -ListingsPath $StoreListingsPath `
        -LocalesPath $StoreLocalesPath `
        -OutputPath $updatedSubmissionPath `
        -ReleaseNotes $ReleaseNotes

    $updatedMetadata = Get-Content -LiteralPath $updatedSubmissionPath -Raw
    Invoke-MsStore -Arguments @(
        "submission",
        "updateMetadata",
        $PartnerCenterStoreId,
        $updatedMetadata
    )

    if (-not $Submit)
    {
        Write-Host "The package and localized listings are ready and will remain in draft state."
        return
    }

    Invoke-MsStore -Arguments @(
        "submission",
        "publish",
        $PartnerCenterStoreId
    )
    Invoke-MsStore -Arguments @(
        "submission",
        "poll",
        $PartnerCenterStoreId
    )
}
finally
{
    $resolvedTempPath = [IO.Path]::GetFullPath($tempPath)

    if ($resolvedTempPath.StartsWith($tempRoot, [StringComparison]::OrdinalIgnoreCase) -and
        [IO.Path]::GetFileName($resolvedTempPath).StartsWith("FluentHub-MicrosoftStore-", [StringComparison]::Ordinal) -and
        [IO.Directory]::Exists($resolvedTempPath))
    {
        [IO.Directory]::Delete($resolvedTempPath, $true)
    }
}
