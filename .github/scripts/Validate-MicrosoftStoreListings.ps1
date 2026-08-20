# Copyright (c) 2026 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$ListingsPath,

    [Parameter(Mandatory)]
    [string]$LocalesPath
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$allowedProperties = @(
    "description",
    "shortDescription",
    "features",
    "keywords",
    "releaseNotes",
    "shortTitle",
    "voiceTitle",
    "devStudio",
    "copyrightAndTrademarkInfo",
    "licenseTerms",
    "recommendedHardware",
    "minimumHardware"
)

function Assert-StringLength
{
    param(
        [Parameter(Mandatory)]
        [string]$Locale,

        [Parameter(Mandatory)]
        [string]$PropertyName,

        [AllowEmptyString()]
        [string]$Value,

        [Parameter(Mandatory)]
        [int]$MaximumLength,

        [switch]$Required
    )

    if ($Required -and [string]::IsNullOrWhiteSpace($Value))
    {
        throw "$Locale.$PropertyName must not be empty."
    }

    if ($Value.Length -gt $MaximumLength)
    {
        throw "$Locale.$PropertyName exceeds the $MaximumLength character limit."
    }
}

function Assert-StringArray
{
    param(
        [Parameter(Mandatory)]
        [string]$Locale,

        [Parameter(Mandatory)]
        [string]$PropertyName,

        [AllowEmptyCollection()]
        [object[]]$Values,

        [Parameter(Mandatory)]
        [int]$MaximumItems,

        [Parameter(Mandatory)]
        [int]$MaximumItemLength
    )

    if ($Values.Count -gt $MaximumItems)
    {
        throw "$Locale.$PropertyName exceeds the $MaximumItems item limit."
    }

    foreach ($value in $Values)
    {
        if ($value -isnot [string] -or [string]::IsNullOrWhiteSpace($value))
        {
            throw "$Locale.$PropertyName must contain only non-empty strings."
        }

        if ($value.Length -gt $MaximumItemLength)
        {
            throw "$Locale.$PropertyName contains an item longer than $MaximumItemLength characters."
        }
    }
}

if (-not (Test-Path -LiteralPath $ListingsPath -PathType Container))
{
    throw "The Store listings directory does not exist: $ListingsPath"
}

if (-not (Test-Path -LiteralPath $LocalesPath -PathType Leaf))
{
    throw "The Store locales file does not exist: $LocalesPath"
}

$localeConfiguration = Get-Content -LiteralPath $LocalesPath -Raw | ConvertFrom-Json -Depth 10
$localesProperty = $localeConfiguration.PSObject.Properties["locales"]
$defaultLocaleProperty = $localeConfiguration.PSObject.Properties["defaultLocale"]

if ($null -eq $localesProperty)
{
    throw "The Microsoft Store locales file must contain a locales array."
}

if ($null -eq $defaultLocaleProperty)
{
    throw "The Microsoft Store locales file must contain defaultLocale."
}

$locales = @($localesProperty.Value)
$defaultLocale = [string]$defaultLocaleProperty.Value

if ($locales.Count -eq 0)
{
    throw "At least one Microsoft Store locale must be enabled."
}

if ([string]::IsNullOrWhiteSpace($defaultLocale))
{
    throw "The Microsoft Store defaultLocale must be specified."
}

for ($localeIndex = 0; $localeIndex -lt $locales.Count; $localeIndex++)
{
    $locale = $locales[$localeIndex]

    if ($locale -isnot [string] -or [string]::IsNullOrWhiteSpace($locale))
    {
        throw "The Microsoft Store locale at index $localeIndex must be a non-empty string."
    }
}

if ($locales -inotcontains $defaultLocale)
{
    throw "The Microsoft Store defaultLocale must be included in locales."
}

$normalizedLocales = @($locales | ForEach-Object { $_.ToLowerInvariant() })

if (@($normalizedLocales | Select-Object -Unique).Count -ne $locales.Count)
{
    throw "The Microsoft Store locales list contains duplicates."
}

foreach ($locale in $locales)
{
    if ($locale -notmatch "^[a-z]{2,3}(?:-[A-Za-z0-9]{2,8})+$")
    {
        throw "The Microsoft Store locale '$locale' is not a supported locale format."
    }

    $listingPath = Join-Path $ListingsPath "$locale.json"

    if (-not (Test-Path -LiteralPath $listingPath -PathType Leaf))
    {
        throw "The Store listing for '$locale' does not exist: $listingPath"
    }

    $listing = Get-Content -LiteralPath $listingPath -Raw | ConvertFrom-Json -Depth 20
    $unknownProperties = @(
        $listing.PSObject.Properties.Name |
            Where-Object { $allowedProperties -notcontains $_ }
    )

    if ($unknownProperties.Count -gt 0)
    {
        throw "$locale contains unsupported properties: $($unknownProperties -join ', ')."
    }

    $descriptionProperty = $listing.PSObject.Properties["description"]

    if ($null -eq $descriptionProperty -or $descriptionProperty.Value -isnot [string])
    {
        throw "$locale.description must be a string."
    }

    Assert-StringLength -Locale $locale -PropertyName "description" `
        -Value $descriptionProperty.Value -MaximumLength 10000 -Required

    $stringLimits = @{
        shortDescription             = 1000
        releaseNotes                 = 1500
        shortTitle                   = 50
        voiceTitle                   = 255
        devStudio                    = 255
        copyrightAndTrademarkInfo    = 200
        licenseTerms                 = 10000
    }

    foreach ($entry in $stringLimits.GetEnumerator())
    {
        $property = $listing.PSObject.Properties[$entry.Key]

        if ($null -eq $property)
        {
            continue
        }

        if ($property.Value -isnot [string])
        {
            throw "$locale.$($entry.Key) must be a string."
        }

        Assert-StringLength -Locale $locale -PropertyName $entry.Key `
            -Value $property.Value -MaximumLength $entry.Value
    }

    $arrayLimits = @{
        features                = @(20, 200)
        keywords                = @(7, 40)
        recommendedHardware     = @(11, 200)
        minimumHardware         = @(11, 200)
    }

    foreach ($entry in $arrayLimits.GetEnumerator())
    {
        $property = $listing.PSObject.Properties[$entry.Key]

        if ($null -eq $property)
        {
            continue
        }

        if ($property.Value -is [string] -or $property.Value -isnot [Collections.IEnumerable])
        {
            throw "$locale.$($entry.Key) must be an array."
        }

        Assert-StringArray -Locale $locale -PropertyName $entry.Key `
            -Values @($property.Value) `
            -MaximumItems $entry.Value[0] `
            -MaximumItemLength $entry.Value[1]
    }

    $keywordsProperty = $listing.PSObject.Properties["keywords"]

    if ($null -ne $keywordsProperty)
    {
        $keywordWordCount = [regex]::Matches(
            (@($keywordsProperty.Value) -join " "),
            "\S+"
        ).Count

        if ($keywordWordCount -gt 21)
        {
            throw "$locale.keywords exceeds the 21 word limit."
        }
    }
}

Write-Host "Validated $($locales.Count) Microsoft Store listing(s): $($locales -join ', ')"
