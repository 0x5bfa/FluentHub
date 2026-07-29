# Copyright (c) 2026 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$SubmissionPath,

    [Parameter(Mandatory)]
    [string]$ListingsPath,

    [Parameter(Mandatory)]
    [string]$LocalesPath,

    [Parameter(Mandatory)]
    [string]$OutputPath,

    [string]$ReleaseNotes = ""
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

function Set-JsonProperty
{
    param(
        [Parameter(Mandatory)]
        [psobject]$InputObject,

        [Parameter(Mandatory)]
        [string]$Name,

        [AllowNull()]
        [object]$Value
    )

    if ($InputObject.PSObject.Properties.Name -contains $Name)
    {
        $InputObject.$Name = $Value
    }
    else
    {
        $InputObject | Add-Member -NotePropertyName $Name -NotePropertyValue $Value
    }
}

if (-not (Test-Path -LiteralPath $SubmissionPath -PathType Leaf))
{
    throw "The Store submission file does not exist: $SubmissionPath"
}

$validationScriptPath = Join-Path $PSScriptRoot "Validate-MicrosoftStoreListings.ps1"
& $validationScriptPath -ListingsPath $ListingsPath -LocalesPath $LocalesPath

$localeConfiguration = Get-Content -LiteralPath $LocalesPath -Raw | ConvertFrom-Json -Depth 10
$submission = Get-Content -LiteralPath $SubmissionPath -Raw | ConvertFrom-Json -Depth 100
$listingsProperty = $submission.PSObject.Properties["listings"]

if ($null -eq $listingsProperty -or $null -eq $listingsProperty.Value)
{
    throw "The Store submission does not contain any listings."
}

$locales = @($localeConfiguration.PSObject.Properties["locales"].Value)
$defaultLocale = [string]$localeConfiguration.PSObject.Properties["defaultLocale"].Value

foreach ($locale in $locales)
{
    $submissionListing = $listingsProperty.Value.PSObject.Properties |
        Where-Object { $_.Name.Equals($locale, [StringComparison]::OrdinalIgnoreCase) } |
        Select-Object -First 1

    if ($null -eq $submissionListing)
    {
        throw @"
The Partner Center draft does not contain a '$locale' listing.
Add the language and its screenshots in Partner Center once, then rerun this workflow.
"@
    }

    $baseListingProperty = $submissionListing.Value.PSObject.Properties["baseListing"]

    if ($null -eq $baseListingProperty -or $null -eq $baseListingProperty.Value)
    {
        throw "The Partner Center '$locale' listing does not contain baseListing metadata."
    }

    $baseListing = $baseListingProperty.Value
    $listingPath = Join-Path $ListingsPath "$locale.json"
    $localizedListing = Get-Content -LiteralPath $listingPath -Raw | ConvertFrom-Json -Depth 20

    foreach ($property in $localizedListing.PSObject.Properties)
    {
        Set-JsonProperty -InputObject $baseListing -Name $property.Name -Value $property.Value
    }

    if ($locale.Equals($defaultLocale, [StringComparison]::OrdinalIgnoreCase) -and
        -not [string]::IsNullOrWhiteSpace($ReleaseNotes))
    {
        if ($ReleaseNotes.Length -gt 1500)
        {
            throw "ReleaseNotes exceeds the 1500 character limit."
        }

        Set-JsonProperty -InputObject $baseListing -Name "releaseNotes" -Value $ReleaseNotes
    }
}

$outputDirectory = Split-Path -Parent $OutputPath

if (-not [string]::IsNullOrWhiteSpace($outputDirectory))
{
    [IO.Directory]::CreateDirectory($outputDirectory) | Out-Null
}

$updatedJson = $submission | ConvertTo-Json -Depth 100
[IO.File]::WriteAllText(
    $OutputPath,
    $updatedJson + [Environment]::NewLine,
    [Text.UTF8Encoding]::new($false)
)

Write-Host "Merged $($locales.Count) localized Store listing(s)."
