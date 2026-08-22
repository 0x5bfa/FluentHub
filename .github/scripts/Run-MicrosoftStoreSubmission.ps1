# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$submit = [Convert]::ToBoolean($env:STORE_SUBMIT)
$replaceExistingDraft = [Convert]::ToBoolean($env:STORE_REPLACE_EXISTING_DRAFT)

Write-Host "Invoking Microsoft Store submission script (submit=$submit, replaceExistingDraft=$replaceExistingDraft)."

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
    ReleaseNotes = $env:STORE_RELEASE_NOTES
    FlightId = $env:STORE_FLIGHT_ID
    PackageRolloutPercentage = $env:STORE_ROLLOUT_PERCENTAGE
    SubmissionMetadataPath = $env:STORE_SUBMISSION_METADATA_PATH
}

& $submissionScript @submissionParameters
