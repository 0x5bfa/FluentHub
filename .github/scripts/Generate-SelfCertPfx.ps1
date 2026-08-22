# Copyright (c) 2024 Files Community
# Licensed under the MIT License. See the LICENSE.

# Abstract:
#  This script generates a self-signed certificate for the temporary packaging as a pfx file.

param(
    [Parameter(Mandatory)]
    [string]$Destination,

    [Parameter(Mandatory)]
    [string]$Publisher,

    [string]$FriendlyName = "FluentHub temporary package certificate"
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 3.0

$certificateStoreLocation = "Cert:\CurrentUser\My"

# Generate self signed cert
$cert = New-SelfSignedCertificate `
    -Type Custom `
    -Subject $Publisher `
    -KeyUsage DigitalSignature `
    -FriendlyName $FriendlyName `
    -CertStoreLocation $certificateStoreLocation `
    -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")

try
{
    $destinationPath = [IO.Path]::GetFullPath($Destination)
    $destinationDirectory = [IO.Path]::GetDirectoryName($destinationPath)

    if (-not [string]::IsNullOrWhiteSpace($destinationDirectory))
    {
        [IO.Directory]::CreateDirectory($destinationDirectory) | Out-Null
    }

    # Export the temporary certificate without a password. The file only exists on the ephemeral runner.
    $certificateBytes = $cert.Export(
        [Security.Cryptography.X509Certificates.X509ContentType]::Pkcs12
    )
    [IO.File]::WriteAllBytes($destinationPath, $certificateBytes)

    Write-Host "Generated temporary package certificate for '$Publisher'."
}
finally
{
    Remove-Item -LiteralPath $cert.PSPath -Force -ErrorAction SilentlyContinue
}
