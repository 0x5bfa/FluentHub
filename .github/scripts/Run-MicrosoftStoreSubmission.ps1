# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$submit = [Convert]::ToBoolean($env:STORE_SUBMIT)
$replaceExistingDraft = [Convert]::ToBoolean($env:STORE_REPLACE_EXISTING_DRAFT)
$manifestPath = $env:APP_MANIFEST_PATH
$releaseNotesDirectory = $env:STORE_RELEASE_NOTES_DIR

if ([string]::IsNullOrWhiteSpace($manifestPath) -or
    -not (Test-Path -LiteralPath $manifestPath -PathType Leaf))
{
    throw "The app package manifest does not exist: $manifestPath"
}

if ([string]::IsNullOrWhiteSpace($releaseNotesDirectory) -or
    -not (Test-Path -LiteralPath $releaseNotesDirectory -PathType Container))
{
    throw "The Store release notes directory does not exist: $releaseNotesDirectory"
}

$manifest = [xml](Get-Content -LiteralPath $manifestPath -Raw)
$identity = $manifest.SelectSingleNode(
    "/*[local-name()='Package']/*[local-name()='Identity']"
)

if ($null -eq $identity)
{
    throw "The app package manifest does not contain an Identity element: $manifestPath"
}

$packageVersion = [Version]$identity.GetAttribute("Version")
$releaseVersion = "$($packageVersion.Major).$($packageVersion.Minor).$($packageVersion.Build)"
$releaseNotesPath = Join-Path $releaseNotesDirectory "$releaseVersion.md"

if (-not (Test-Path -LiteralPath $releaseNotesPath -PathType Leaf))
{
    throw "The Store release notes file does not exist: $releaseNotesPath"
}

$releaseNotes = (Get-Content -LiteralPath $releaseNotesPath -Raw).Trim()

if ([string]::IsNullOrWhiteSpace($releaseNotes))
{
    throw "The Store release notes file is empty: $releaseNotesPath"
}

if ($releaseNotes.Length -gt 1500)
{
    throw "The Store release notes file exceeds the 1500 character limit: $releaseNotesPath"
}

Write-Host "Invoking Microsoft Store submission script (submit=$submit, replaceExistingDraft=$replaceExistingDraft)."
Write-Host "Using Store release notes for version $releaseVersion."

$submissionScript = Join-Path $PSScriptRoot "SubmitTo-MicrosoftStore.ps1"

$submissionParameters = @{
    StorePackagePath = $env:STORE_PACKAGE_PATH
    StoreListingsPath = $env:STORE_LISTINGS_DIR
    StoreLocalesPath = $env:STORE_LOCALES_PATH
    PartnerCenterClientId = $env:PARTNER_CENTER_CLIENT_ID
    PartnerCenterClientSecret = $env:PARTNER_CENTER_CLIENT_SECRET
    PartnerCenterSellerId = $env:PARTNER_CENTER_SELLER_ID
    PartnerCenterStoreId = $env:STORE_PRODUCT_ID
    PartnerCenterTenantId = $env:PARTNER_CENTER_TENANT_ID
    Submit = $submit
    ReplaceExistingDraft = $replaceExistingDraft
    ReleaseNotes = $releaseNotes
    FlightId = $env:STORE_FLIGHT_ID
    PackageRolloutPercentage = $env:STORE_ROLLOUT_PERCENTAGE
    SubmissionMetadataPath = $env:STORE_SUBMISSION_METADATA_PATH
}

& $submissionScript @submissionParameters
