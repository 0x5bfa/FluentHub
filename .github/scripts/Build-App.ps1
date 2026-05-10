# Copyright (c) Files Community
# Licensed under the MIT License.

param(
    [string]$Platform =       "x64",
    [string]$Configuration =  "Debug",
    [bool]$IsStorePublish  = $false,
    [string]$PackageManifestPath = ""
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

if ($IsStorePublish)
{
    msbuild FluentHub.App -restore /clp:ErrorsOnly /p:Platform=$Platform /p:Configuration=$Configuration /p:UapAppxPackageBuildMode=StoreOnly
}
else
{
    msbuild FluentHub.App -restore /clp:ErrorsOnly /p:Platform=$Platform /p:Configuration=$Configuration
}
