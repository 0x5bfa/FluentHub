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

    [string]$PackageRolloutPercentage = "",

    [ValidateRange(1, 3600)]
    [int]$PackageValidationTimeoutSeconds = 300,

    [ValidateRange(1, 3600)]
    [int]$SubmissionVerificationTimeoutSeconds = 300
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

function Get-PropertyByName
{
    param(
        [Parameter(Mandatory)]
        [psobject]$InputObject,

        [Parameter(Mandatory)]
        [string]$Name
    )

    return $InputObject.PSObject.Properties |
        Where-Object { $_.Name.Equals($Name, [StringComparison]::OrdinalIgnoreCase) } |
        Select-Object -First 1
}

function Normalize-ComparableValue
{
    param(
        [AllowNull()]
        [object]$Value
    )

    if ($null -eq $Value)
    {
        return $null
    }

    if ($Value -is [string])
    {
        return [regex]::Replace($Value, '\s+', ' ').Trim()
    }

    if ($Value -is [System.Collections.IEnumerable] -and
        $Value -isnot [System.Collections.IDictionary])
    {
        return @($Value | ForEach-Object {
            Normalize-ComparableValue -Value $_
        })
    }

    return $Value
}

function ConvertTo-ComparableJson
{
    param(
        [AllowNull()]
        [object]$Value
    )

    $normalizedValue = Normalize-ComparableValue -Value $Value

    if ($null -eq $normalizedValue)
    {
        return "null"
    }

    return $normalizedValue | ConvertTo-Json -Depth 100 -Compress
}

function Get-ApplicationPackage
{
    param(
        [Parameter(Mandatory)]
        [psobject]$Submission,

        [Parameter(Mandatory)]
        [string]$FileName
    )

    $applicationPackagesProperty = Get-PropertyByName `
        -InputObject $Submission `
        -Name "ApplicationPackages"

    if ($null -eq $applicationPackagesProperty)
    {
        return $null
    }

    foreach ($applicationPackage in @($applicationPackagesProperty.Value))
    {
        $fileNameProperty = Get-PropertyByName `
            -InputObject $applicationPackage `
            -Name "FileName"

        if ($null -ne $fileNameProperty -and
            [string]$fileNameProperty.Value -ieq $FileName)
        {
            return $applicationPackage
        }
    }

    return $null
}

function Assert-SubmissionLocales
{
    param(
        [Parameter(Mandatory)]
        [psobject]$Submission,

        [Parameter(Mandatory)]
        [string[]]$ConfiguredLocales
    )

    $listingsProperty = Get-PropertyByName `
        -InputObject $Submission `
        -Name "Listings"

    if ($null -eq $listingsProperty -or $null -eq $listingsProperty.Value)
    {
        throw "The Store submission does not contain Listings."
    }

    $configuredLocaleSet = [Collections.Generic.HashSet[string]]::new(
        [StringComparer]::OrdinalIgnoreCase
    )

    foreach ($locale in $ConfiguredLocales)
    {
        $configuredLocaleSet.Add($locale) | Out-Null
    }

    $submissionLocales = @($listingsProperty.Value.PSObject.Properties.Name)
    $unmanagedLocales = @(
        $submissionLocales |
            Where-Object { -not $configuredLocaleSet.Contains($_) }
    )
    $missingLocales = @(
        $ConfiguredLocales |
            Where-Object { $submissionLocales -inotcontains $_ }
    )

    if ($unmanagedLocales.Count -gt 0 -or $missingLocales.Count -gt 0)
    {
        $expectedLocales = @($ConfiguredLocales | Sort-Object) -join ", "
        $actualLocales = @($submissionLocales | Sort-Object) -join ", "
        throw "The Store listing locale set does not match locales.json. Expected: [$expectedLocales]. Actual: [$actualLocales]."
    }

    return $listingsProperty.Value
}

function Wait-MicrosoftStorePackageValidation
{
    $expectedPackageName = [IO.Path]::GetFileName($StorePackagePath)
    $deadline = [DateTime]::UtcNow.AddSeconds($PackageValidationTimeoutSeconds)
    $delaySeconds = 5
    $attempt = 0
    $lastError = "The package has not been returned by Partner Center."

    while ($true)
    {
        $attempt++

        try
        {
            $submissionJson = Invoke-MsStoreJson -Arguments @(
                "submission",
                "get",
                $PartnerCenterStoreId
            )
            $submission = $submissionJson | ConvertFrom-Json -Depth 100
            $applicationPackage = Get-ApplicationPackage `
                -Submission $submission `
                -FileName $expectedPackageName

            if ($null -eq $applicationPackage)
            {
                $lastError = "The draft does not contain '$expectedPackageName'."
            }
            else
            {
                $versionProperty = Get-PropertyByName `
                    -InputObject $applicationPackage `
                    -Name "Version"
                $languagesProperty = Get-PropertyByName `
                    -InputObject $applicationPackage `
                    -Name "Languages"
                $version = ""
                $languages = @()

                if ($null -ne $versionProperty)
                {
                    $version = [string]$versionProperty.Value
                }

                if ($null -ne $languagesProperty)
                {
                    $languages = @(
                        $languagesProperty.Value |
                            Where-Object {
                                $_ -is [string] -and
                                -not [string]::IsNullOrWhiteSpace($_)
                            }
                    )
                }

                if (-not [string]::IsNullOrWhiteSpace($version) -and
                    $languages.Count -gt 0)
                {
                    Write-Host @"
Store package validation completed: $expectedPackageName (version=$version, languages=$($languages.Count)).
"@
                    return $submissionJson
                }

                $lastError = @"
Partner Center has not finished validating '$expectedPackageName' (version='$version', languages=$($languages.Count)).
"@.Trim()
            }
        }
        catch
        {
            $lastError = $_.Exception.Message
        }

        $remainingSeconds = [int][Math]::Floor(
            ($deadline - [DateTime]::UtcNow).TotalSeconds
        )

        if ($remainingSeconds -le 0)
        {
            throw "Timed out waiting for Store package validation after $attempt attempt(s). Last result: $lastError"
        }

        $sleepSeconds = [Math]::Min($delaySeconds, $remainingSeconds)
        Write-Warning @"
Store package validation attempt $attempt is not ready: $lastError Retrying in $sleepSeconds second(s).
"@
        Start-Sleep -Seconds $sleepSeconds
        $delaySeconds = [Math]::Min($delaySeconds * 2, 30)
    }
}

function Test-UpdatedSubmission
{
    $submissionJson = Invoke-MsStoreJson -Arguments @(
        "submission",
        "get",
        $PartnerCenterStoreId
    )
    $submission = $submissionJson | ConvertFrom-Json -Depth 100

    $expectedPackageName = [IO.Path]::GetFileName($StorePackagePath)
    $packageMatch = Get-ApplicationPackage `
        -Submission $submission `
        -FileName $expectedPackageName

    if ($null -eq $packageMatch)
    {
        throw "The updated submission does not contain the uploaded package '$expectedPackageName'."
    }

    Write-Host "Verified Store package in submission: $expectedPackageName"

    $localeConfiguration = Get-Content -LiteralPath $StoreLocalesPath -Raw | ConvertFrom-Json -Depth 10
    $locales = @($localeConfiguration.PSObject.Properties["locales"].Value)
    $submissionListings = Assert-SubmissionLocales `
        -Submission $submission `
        -ConfiguredLocales $locales

    foreach ($locale in $locales)
    {
        $submissionListing = $submissionListings.PSObject.Properties |
            Where-Object { $_.Name.Equals($locale, [StringComparison]::OrdinalIgnoreCase) } |
            Select-Object -First 1

        if ($null -eq $submissionListing)
        {
            throw "The updated submission does not contain the '$locale' listing."
        }

        $baseListingProperty = Get-PropertyByName `
            -InputObject $submissionListing.Value `
            -Name "BaseListing"

        if ($null -eq $baseListingProperty)
        {
            throw "The updated '$locale' listing does not contain BaseListing."
        }

        $listingPath = Join-Path $StoreListingsPath "$locale.json"
        $localizedListing = Get-Content -LiteralPath $listingPath -Raw | ConvertFrom-Json -Depth 20

        foreach ($property in $localizedListing.PSObject.Properties)
        {
            $updatedProperty = Get-PropertyByName `
                -InputObject $baseListingProperty.Value `
                -Name $property.Name

            if ($null -eq $updatedProperty)
            {
                $expectedValue = ConvertTo-ComparableJson -Value $property.Value
                throw @"
The updated '$locale' listing does not contain '$($property.Name)'. Expected: $expectedValue. Actual: <missing>.
"@
            }

            $expectedValue = ConvertTo-ComparableJson -Value $property.Value
            $actualValue = ConvertTo-ComparableJson -Value $updatedProperty.Value

            if ($expectedValue -cne $actualValue)
            {
                throw @"
The updated '$locale' listing property '$($property.Name)' does not match the repository listing. Expected: $expectedValue. Actual: $actualValue.
"@
            }
        }

        Write-Host "Verified Store listing in submission: $locale"
    }
}

function Wait-MicrosoftStoreSubmissionVerification
{
    $deadline = [DateTime]::UtcNow.AddSeconds($SubmissionVerificationTimeoutSeconds)
    $delaySeconds = 5
    $attempt = 0
    $lastError = "The Store submission has not been verified."

    while ($true)
    {
        $attempt++

        try
        {
            Test-UpdatedSubmission
            Write-Host "Verified the updated Store submission after $attempt attempt(s)."
            return
        }
        catch
        {
            $lastError = $_.Exception.Message
        }

        $remainingSeconds = [int][Math]::Floor(
            ($deadline - [DateTime]::UtcNow).TotalSeconds
        )

        if ($remainingSeconds -le 0)
        {
            throw "Timed out verifying the Store submission after $attempt attempt(s). Last error: $lastError"
        }

        $sleepSeconds = [Math]::Min($delaySeconds, $remainingSeconds)
        Write-Warning @"
Store submission verification attempt $attempt failed: $lastError Retrying in $sleepSeconds second(s).
"@
        Start-Sleep -Seconds $sleepSeconds
        $delaySeconds = [Math]::Min($delaySeconds * 2, 30)
    }
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
    $submissionJson = Wait-MicrosoftStorePackageValidation
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

    Wait-MicrosoftStoreSubmissionVerification

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
