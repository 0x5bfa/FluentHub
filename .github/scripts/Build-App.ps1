# Copyright (c) Files Community
# Licensed under the MIT License.

param(
    [string]$Platform =       "x64",
    [string]$Configuration =  "Debug",
    [bool]$IsStorePublish  = $false,
    [string]$PackageManifestPath = "",
    [string]$PackageCertificateKeyFile = "",
    [string]$AppxBundlePlatforms = ""
)

# Load Package.appxmanifest
[xml]$xmlDoc = Get-Content $PackageManifestPath

if ($IsStorePublish)
{
    # Set identities
    $xmlDoc.Package.Identity.Name="49462fluenthub-uwp.FluentHub"
    $xmlDoc.Package.Identity.Publisher = "CN=4E3C0825-4D00-4ECC-89F8-528E9200B125"
    $xmlDoc.Package.Properties.DisplayName="FluentHub"
    $xmlDoc.Package.Applications.Application.VisualElements.DisplayName="FluentHub"
    $xmlDoc.Package.Applications.Application.VisualElements.DefaultTile.ShortName="FluentHub"

    # Save modified Package.appxmanifest
    $xmlDoc.Save($PackageManifestPath)

    Get-ChildItem $WorkingDir -Include *.csproj, *.appxmanifest, *.xaml -recurse | ForEach-Object -Process `
    { `
        (Get-Content $_ -Raw | ForEach-Object -Process { $_ -replace "Assets\\AppTiles\\Dev", "Assets\AppTiles\Release" }) | `
        Set-Content $_ -NoNewline `
    }
}

$buildArguments = @(
    "FluentHub",
    "-restore",
    "/clp:ErrorsOnly",
    "/p:Platform=$Platform",
    "/p:Configuration=$Configuration"
)

if ($IsStorePublish)
{
    $packageBuildMode = if ([string]::IsNullOrWhiteSpace($PackageCertificateKeyFile))
    {
        "StoreOnly"
    }
    else
    {
        "StoreUpload"
    }

    $buildArguments += "/p:UapAppxPackageBuildMode=$packageBuildMode"
}

if (-not [string]::IsNullOrWhiteSpace($AppxBundlePlatforms))
{
    $buildArguments += "/p:AppxBundlePlatforms=$AppxBundlePlatforms"
}

if (-not [string]::IsNullOrWhiteSpace($PackageCertificateKeyFile))
{
    $certificatePath = [IO.Path]::GetFullPath($PackageCertificateKeyFile)

    if (-not [IO.File]::Exists($certificatePath))
    {
        throw "The package signing certificate does not exist: $certificatePath"
    }

    $buildArguments += @(
        "/p:AppxPackageSigningEnabled=true",
        "/p:PackageCertificateKeyFile=$certificatePath"
    )
}

& msbuild @buildArguments

if ($LASTEXITCODE -ne 0)
{
    throw "MSBuild failed with exit code $LASTEXITCODE."
}
