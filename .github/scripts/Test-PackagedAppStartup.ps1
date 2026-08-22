# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

#Requires -Version 7.4

[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    [Parameter(Mandatory)]
    [string]$CertificatePath,

    [Parameter(Mandatory)]
    [string]$PackageName,

    [Parameter(Mandatory)]
    [ValidateSet("x64", "arm64")]
    [string]$Architecture,

    [Parameter(Mandatory)]
    [string]$DiagnosticsPath,

    [ValidateRange(5, 120)]
    [int]$StartupTimeoutSeconds = 30
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

function Save-StartupDiagnostics
{
    param(
        [Parameter(Mandatory)]
        [DateTime]$StartTime,

        [Parameter(Mandatory)]
        [string]$ExecutableName,

        [AllowNull()]
        [psobject]$InstalledPackage
    )

    $eventLogPath = Join-Path $diagnosticsDirectory "ApplicationEvents.txt"
    $events = @(
        Get-WinEvent `
            -FilterHashtable @{
                LogName = "Application"
                StartTime = $StartTime.AddSeconds(-5)
            } `
            -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Id -in @(1000, 1001, 1026) -or
                $_.Message -like "*$ExecutableName*"
            }
    )

    $events |
        Format-List TimeCreated, Id, LevelDisplayName, ProviderName, Message |
        Out-File -LiteralPath $eventLogPath -Encoding utf8

    if ($null -ne $InstalledPackage)
    {
        $appLogPath = Join-Path `
            $env:LOCALAPPDATA `
            "Packages\$($InstalledPackage.PackageFamilyName)\LocalState\FluentHub.Logs\Log.log"

        if (Test-Path -LiteralPath $appLogPath -PathType Leaf)
        {
            Copy-Item `
                -LiteralPath $appLogPath `
                -Destination (Join-Path $diagnosticsDirectory "FluentHub.Log.log") `
                -Force
        }
    }
}

$resolvedPackagePath = [IO.Path]::GetFullPath($PackagePath)
$resolvedCertificatePath = [IO.Path]::GetFullPath($CertificatePath)
$diagnosticsDirectory = [IO.Path]::GetFullPath($DiagnosticsPath)

if (-not [IO.File]::Exists($resolvedPackagePath))
{
    throw "The MSIX package does not exist: $resolvedPackagePath"
}

if (-not [IO.File]::Exists($resolvedCertificatePath))
{
    throw "The package certificate does not exist: $resolvedCertificatePath"
}

[IO.Directory]::CreateDirectory($diagnosticsDirectory) | Out-Null

$certificateThumbprint = $null
$certificateStore = "Cert:\LocalMachine\TrustedPeople"
$installedPackage = $null
$activatedProcess = $null
$dumpRegistryPath = $null
$launchStartTime = [DateTime]::UtcNow
$executableName = ""

try
{
    $certificate = Import-Certificate `
        -FilePath $resolvedCertificatePath `
        -CertStoreLocation $certificateStore

    $certificateThumbprint = $certificate.Thumbprint

    Get-AppxPackage -Name $PackageName -ErrorAction SilentlyContinue |
        Remove-AppxPackage -ErrorAction SilentlyContinue

    $packageDirectory = [IO.Path]::GetDirectoryName($resolvedPackagePath)
    $dependenciesRoot = Join-Path $packageDirectory "Dependencies"
    $dependencyPackages = @()

    if (Test-Path -LiteralPath $dependenciesRoot -PathType Container)
    {
        $dependencyPackages += @(
            Get-ChildItem -LiteralPath $dependenciesRoot -File -ErrorAction SilentlyContinue |
                Where-Object { $_.Extension -in @(".appx", ".msix") }
        )

        $architectureDependencies = Join-Path $dependenciesRoot $Architecture

        if (Test-Path -LiteralPath $architectureDependencies -PathType Container)
        {
            $dependencyPackages += @(
                Get-ChildItem -LiteralPath $architectureDependencies -File |
                    Where-Object { $_.Extension -in @(".appx", ".msix") }
            )
        }
    }

    $addPackageParameters = @{
        Path = $resolvedPackagePath
        ForceApplicationShutdown = $true
    }

    if ($dependencyPackages.Count -gt 0)
    {
        $addPackageParameters.DependencyPath = @(
            $dependencyPackages |
                Sort-Object FullName -Unique |
                Select-Object -ExpandProperty FullName
        )
    }

    Add-AppxPackage @addPackageParameters

    $installedPackage = Get-AppxPackage -Name $PackageName |
        Sort-Object Version -Descending |
        Select-Object -First 1

    if ($null -eq $installedPackage)
    {
        throw "The package '$PackageName' was not installed."
    }

    $manifest = Get-AppxPackageManifest -Package $installedPackage.PackageFullName
    $application = @($manifest.Package.Applications.Application) | Select-Object -First 1

    if ($null -eq $application -or [string]::IsNullOrWhiteSpace([string]$application.Id))
    {
        throw "The installed package does not contain an application entry."
    }

    $executableName = [IO.Path]::GetFileName([string]$application.Executable)
    $applicationUserModelId = "$($installedPackage.PackageFamilyName)!$($application.Id)"

    $dumpRegistryPath = "HKCU:\Software\Microsoft\Windows\Windows Error Reporting\LocalDumps\$executableName"
    New-Item -Path $dumpRegistryPath -Force | Out-Null
    New-ItemProperty `
        -Path $dumpRegistryPath `
        -Name "DumpFolder" `
        -PropertyType ExpandString `
        -Value $diagnosticsDirectory `
        -Force | Out-Null
    New-ItemProperty `
        -Path $dumpRegistryPath `
        -Name "DumpType" `
        -PropertyType DWord `
        -Value 2 `
        -Force | Out-Null

    if (-not ("FluentHub.CI.ApplicationActivation" -as [type]))
    {
        Add-Type -TypeDefinition @"
using System;
using System.Runtime.InteropServices;

namespace FluentHub.CI
{
    [ComImport]
    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    internal class ApplicationActivationManagerClass
    {
    }

    [ComImport]
    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IApplicationActivationManager
    {
        int ActivateApplication(
            [MarshalAs(UnmanagedType.LPWStr)] string appUserModelId,
            [MarshalAs(UnmanagedType.LPWStr)] string arguments,
            uint options,
            out uint processId);
    }

    public static class ApplicationActivation
    {
        public static int Activate(string appUserModelId)
        {
            var manager = (IApplicationActivationManager)new ApplicationActivationManagerClass();
            var result = manager.ActivateApplication(appUserModelId, null, 2, out var processId);
            Marshal.ThrowExceptionForHR(result);
            return checked((int)processId);
        }
    }
}
"@
    }

    $launchStartTime = [DateTime]::UtcNow
    $processId = [FluentHub.CI.ApplicationActivation]::Activate($applicationUserModelId)
    $deadline = [DateTime]::UtcNow.AddSeconds($StartupTimeoutSeconds)
    $windowCreated = $false

    while ([DateTime]::UtcNow -lt $deadline)
    {
        $activatedProcess = Get-Process -Id $processId -ErrorAction SilentlyContinue

        if ($null -eq $activatedProcess -or $activatedProcess.HasExited)
        {
            break
        }

        $activatedProcess.Refresh()

        if ($activatedProcess.MainWindowHandle -ne [IntPtr]::Zero)
        {
            $windowCreated = $true
            break
        }

        Start-Sleep -Seconds 1
    }

    if (-not $windowCreated)
    {
        Start-Sleep -Seconds 2
        Save-StartupDiagnostics `
            -StartTime $launchStartTime `
            -ExecutableName $executableName `
            -InstalledPackage $installedPackage

        if ($null -eq $activatedProcess -or $activatedProcess.HasExited)
        {
            throw "The packaged app exited before its main window was created."
        }

        throw "The packaged app did not create a main window within $StartupTimeoutSeconds seconds."
    }

    $crashEvents = @(
        Get-WinEvent `
            -FilterHashtable @{
                LogName = "Application"
                StartTime = $launchStartTime
            } `
            -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Id -in @(1000, 1001, 1026) -and
                $_.Message -like "*$executableName*"
            }
    )

    if ($crashEvents.Count -gt 0)
    {
        Save-StartupDiagnostics `
            -StartTime $launchStartTime `
            -ExecutableName $executableName `
            -InstalledPackage $installedPackage
        throw "Windows reported a startup crash for '$executableName'."
    }

    Write-Host "Validated packaged app startup: $applicationUserModelId (PID $processId)."
}
catch
{
    if ($null -ne $installedPackage -and
        -not [string]::IsNullOrWhiteSpace($executableName))
    {
        Save-StartupDiagnostics `
            -StartTime $launchStartTime `
            -ExecutableName $executableName `
            -InstalledPackage $installedPackage
    }

    throw
}
finally
{
    if ($null -ne $activatedProcess -and -not $activatedProcess.HasExited)
    {
        Stop-Process -Id $activatedProcess.Id -Force -ErrorAction SilentlyContinue
    }

    if ($null -ne $installedPackage)
    {
        Remove-AppxPackage `
            -Package $installedPackage.PackageFullName `
            -ErrorAction SilentlyContinue
    }

    if (-not [string]::IsNullOrWhiteSpace($certificateThumbprint))
    {
        try
        {
            Remove-Item `
                -LiteralPath "$certificateStore\$certificateThumbprint" `
                -Force `
                -ErrorAction Stop
        }
        catch
        {
            Write-Warning "Could not remove the temporary package certificate: $($_.Exception.Message)"
        }
    }

    if ($null -ne $dumpRegistryPath)
    {
        Remove-Item -LiteralPath $dumpRegistryPath -Force -ErrorAction SilentlyContinue
    }
}
