<p align="center">
  <img alt="FluentHub hero image" src="/assets/header.png" />
</p>

<p align="center">
  <a style="text-decoration:none" href="https://github.com/0x5bfa/FluentHub/actions/workflows/ci.yml">
    <img src="https://github.com/0x5bfa/FluentHub/actions/workflows/ci.yml/badge.svg" alt="CI Status" /></a>
  <a style="text-decoration:none" href="https://crowdin.com/project/fluenthub">
    <img src="https://badges.crowdin.net/fluenthub/localized.svg" alt="Localization Status" /></a>
  <a style="text-decoration:none" href="https://dsc.gg/fluenthub">
    <img src="https://img.shields.io/discord/935562861701390336?label=Discord&color=7289da" alt="Discord" /></a>
</p>
<p align="center">
  <a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <a style="text-decoration:none" href="https://apps.microsoft.com/detail/9NGHP3DX8HDX?launch=true&mode=full">
    <picture>
      <source media="(prefers-color-scheme: light)" srcset="https://get.microsoft.com/images/en-us%20dark.svg" width="200" />
      <img src="https://get.microsoft.com/images/en-us%20light.svg" width="200" />
    </picture></a>
</p>

**A stylish yet powerful GitHub client for Windows**, which empowers development experience and follows the Microsoft Design Language.

#### **FluentUI** - designed with FluentUI and built on WinAppSdk/WinUI3
#### **Multitasking** - with FluentHub you can multi-task with ease with tab support built-in to the app
#### **Page navigation** - easily navigate through pages without losing history or progress
#### **Just like GitHub** - perform all of your everyday tasks on GitHub such as creating issues and pull requests with built-in API mutation

<!--## 🎁 Getting started with FluentHub

You need Windows 10 or 11 to run FluentHub.

### GitHub

<a title="GitHub" href='https://github.com/0x5BFA/FluentHub/releases/latest'>
  <img width='175' src='https://user-images.githubusercontent.com/74561130/160255105-5e32f911-574f-4cc4-b90b-8769099086e4.png' alt='Get it from GitHub' />
</a>

Released builds can be manually downloaded from this [repository's releases page](https://github.com/FluentHub/FluentHub/releases).

Download the `FluentHub_<versionNumber>.msixbundle` file from the `Assets` section. In order to install the app, you can simply double-click on the .msixbundle file, and the app installer should automatically run. If that fails for any reason, you can try the following command with a PowerShell prompt:

```powershell
# NOTE: If you are using PowerShell 7+, please run
#   Import-Module Appx -UseWindowsPowerShell
# before using Add-AppxPackage.

Add-AppxPackage FluentHub_<versionNumber>.msixbundle
```-->

## Contributing to FluentHub
- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- [Submit your change](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- Join [our Discord](https://dsc.gg/fluenthub) and let us know what you think
- [Translate on Crowdin](https://crowdin.com/project/fluenthub)

## Screenshots

![Screenshot 2024-09-15 104438](https://github.com/user-attachments/assets/1728729b-0c8f-4cdb-aaf4-fbc7643b0bdf)

Your dashboard|Your repos|User profile page
---|---|---
![Screenshot 2024-09-15 104352](https://github.com/user-attachments/assets/c6e556c8-9fcb-4bfc-822d-08fde80eec2e)|![Screenshot 2024-09-15 104425](https://github.com/user-attachments/assets/3427a168-5bcc-4ac4-a7f2-761698c28eac)|![Screenshot 2024-09-15 104700](https://github.com/user-attachments/assets/d4ee0f1f-7e7b-4751-abf3-1df65ad16a99)




## Building from source

### Prerequistes

- Windows 10 (Build 10.0.19041.0) or newer with Developer Mode enabled in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/):
  - [Windows App SDK (version 10.0.22621.0)](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/)
  - .NET 8 SDK (check the box named .NET Desktop Development in Visual Studio Installer)
  - Windows App SDK

### 1. Close the repo

```git
git clone https://github.com/0x5BFA/FluentHub
```

### 2. Prepare credentials

See [the documentation](docs/credentials.md).

> [!WARNING]  
> If you skip this step, Visual Studio will fail to build reporting that the `AppCredentials.config` file does not exist.

### 3. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Package` in the Solution Explorer.
- Build with `Debug`, `x64`, `FluentHub.Package`.

## Credit

- This app is subject to the [Riverside Valley Global Disclaimer](https://github.com/RiversideValley/.github/blob/main/DISCLAIMER.md).
- Many thanks to [Joseph Beattie](https://github.com/josephbeattie) for creating our current logo.
  
