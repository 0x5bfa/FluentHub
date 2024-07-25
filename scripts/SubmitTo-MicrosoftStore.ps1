# Copyright (c) 2024 0x5BFA
# Licensed under the MIT License. See the LICENSE.

param(
    [string]$SubmissionDirPath = "",
    [string]$StoreBrokerConfigPath = "",
    [string]$AppxPackagePath = "",
    [string]$PartnerCenterClientId = ""
    [string]$PartnerCenterClientSecret = ""
    [string]$PartnerCenterStoreId = ""
    [string]$PartnerCenterTenantId = ""
)

Set-ExecutionPolicy RemoteSigned -Force
Set-PSRepository -Name "PSGallery" -InstallationPolicy Trusted
Install-Module -Name StoreBroker

# Authenticate StoreBroker
$username = $env:PartnerCenterClientId
$password = ConvertTo-SecureString $env:PartnerCenterClientSecret -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential ($username, $password)
Set-StoreBrokerAuthentication -TenantId $PartnerCenterTenantId -Credential $cred

# Prepare the submission package
New-SubmissionPackage -ConfigPath $StoreBrokerConfigPath -AppxPath $appxUploadFilePath -OutPath $SubmissionDirPath -OutName 'submission'
$SubmissionDataPath = Join-Path -Path $SubmissionDirPath -ChildPath 'submission.json'
$SubmissionPackagePath = Join-Path -Path $SubmissionDirPath -ChildPath 'submission.zip'

# Upload the package
Update-ApplicationSubmission -Verbose -ReplacePackages -AppId $PartnerCenterStoreId -SubmissionDataPath $SubmissionDataPath -PackagePath $SubmissionPackagePath -AutoCommit -Force -NoStatus
