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
    [string]$ReportPath
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$resolvedPackagePath = [IO.Path]::GetFullPath($PackagePath)
$resolvedCertificatePath = [IO.Path]::GetFullPath($CertificatePath)
$resolvedReportPath = [IO.Path]::GetFullPath($ReportPath)
$certificationKitPath = Join-Path `
    ${env:ProgramFiles(x86)} `
    "Windows Kits\10\App Certification Kit\appcert.exe"

foreach ($requiredFile in @(
    $resolvedPackagePath,
    $resolvedCertificatePath,
    $certificationKitPath
))
{
    if (-not [IO.File]::Exists($requiredFile))
    {
        throw "A required certification input does not exist: $requiredFile"
    }
}

$reportDirectory = [IO.Path]::GetDirectoryName($resolvedReportPath)

if (-not [string]::IsNullOrWhiteSpace($reportDirectory))
{
    [IO.Directory]::CreateDirectory($reportDirectory) | Out-Null
}

$certificateThumbprint = $null
$certificateStore = "Cert:\LocalMachine\TrustedPeople"

try
{
    $certificate = Import-Certificate `
        -FilePath $resolvedCertificatePath `
        -CertStoreLocation $certificateStore

    $certificateThumbprint = $certificate.Thumbprint

    $resetProcess = Start-Process `
        -FilePath $certificationKitPath `
        -ArgumentList "reset" `
        -Wait `
        -PassThru `
        -NoNewWindow

    if ($resetProcess.ExitCode -ne 0)
    {
        throw "Windows App Certification Kit reset failed with exit code $($resetProcess.ExitCode)."
    }

    $testArguments = @(
        "test",
        "-appxpackagepath",
        "`"$resolvedPackagePath`"",
        "-reportoutputpath",
        "`"$resolvedReportPath`""
    )
    $testProcess = Start-Process `
        -FilePath $certificationKitPath `
        -ArgumentList $testArguments `
        -Wait `
        -PassThru `
        -NoNewWindow

    if ($testProcess.ExitCode -ne 0)
    {
        throw "Windows App Certification Kit failed with exit code $($testProcess.ExitCode)."
    }

    if (-not [IO.File]::Exists($resolvedReportPath))
    {
        throw "Windows App Certification Kit did not create '$resolvedReportPath'."
    }
}
finally
{
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
}
