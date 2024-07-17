<p align="center">
  <img width="128" align="center" src="assets/fluenthub.png" />
</p>
<h1 align="center">
  FluentHub
</h1>
<p align="center">
  <a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
    <img width="220" align="center" src="https://get.microsoft.com/images/en-us%20dark.svg" />
  </a>
</p>

**A stylish yet powerful GitHub client for Windows**, which empowers development experience and follows the Microsoft Design Language.

#### **FluentUI** - designed with FluentUI and built on WinAppSdk/WinUI3
#### **Multitasking** - with FluentHub you can multi-task with ease with tab support built-in to the app
#### **Page navigation** - easily navigate through pages without losing history or progress
#### **Just like GitHub** - perform all of your everyday tasks on GitHub such as creating issues and pull requests with built-in API mutation

## 🎁 Getting started with FluentHub

You need Windows 10 or 11 to run FluentHub.

### Microsoft Store (⭐ Recommended ⭐)

<a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <img width="175" src="https://get.microsoft.com/images/en-us%20dark.svg" />
</a>

This is the preferred installation method. It allows you to always be on the latest version when we release new builds via automatic updates.

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
```

### 🔨 Building from source

### Prerequistes

- Windows 10 (Build 10.0.19041.0) or newer with Developer Mode enabled in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/):
  - [Windows App SDK (version 10.0.22621.0)](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/)
  - .NET 7 SDK (check the box named .NET Desktop Development in Visual Studio Installer)
  - Windows App SDK

### 1. Close the repo

```git
git clone https://github.com/0x5BFA/FluentHub
```

### 2. Prepare credentials

See [the documentation](docs/credentials.md).

> [!IMPORTANT]  
> If you skip this step, Visual Studio will fail to build reporting that the `AppCredentials.config` file does not exist.

### 3. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Package` in the Solution Explorer.
- Build with `Debug`, `x64`, `FluentHub.Package`.

## 📸 Screenshots

**Home page**
![Homepage screenshot](https://github.com/0x5bfa/FluentHub/assets/62196528/a31bdace-8700-4a6a-83e9-1cdc52955c4f)

**PR page**
![Pull request page screenshot](https://github.com/0x5bfa/FluentHub/assets/62196528/a29c4ef8-1fe5-47c3-be03-6afebe02c55b)

**User profile page**
![Profile screenshot](https://github.com/0x5bfa/FluentHub/assets/62196528/35ffbe36-00d3-4d04-9019-67307febfc95)

## 🙋 Contributing & Feedback

There are multiple ways to participate in the community:

- [Submit bugs and feature requests](https://github.com/0x5BFA/FluentHub/issues/new/choose).
- Review [the documentation](docs/code-style.md) and make pull requests for anything from typos to additional and new idea
- Review source code changes

If you are interested in fixing issues and contributing directly to the code base, please refer to the [documentation](docs/), which covers the following:

- [How to build and run from source](docs/)
- The development workflow, including debugging and running tests
- Coding guidelines
- [Submitting pull requests](https://github.com/0x5BFA/FluentHub/pulls)
- [Finding an issue to work on](https://github.com/0x5BFA/FluentHub/issues/)
- [Contributing to translations on Crowdin](https://crowdin.com/project/fluenthub)

<a href="https://crowdin.com/project/fluenthub" rel="nofollow">
  <img style="width:140;height:40" src="https://badges.crowdin.net/badge/dark/crowdin-on-light.png" /></a>

### 🦜 Feedback

- [Request a new feature](https://github.com/0x5BFA/FluentHub/pulls)
- Upvote popular feature requests
- [File an issue](https://github.com/0x5BFA/FluentHub/issues/new/choose)
- Join [our Discord](https://discord.gg/8KtRkjq2Q4) and let us know what you think

[![](https://dcbadge.vercel.app/api/server/8KtRkjq2Q4?style=flat)](https://discord.gg/8KtRkjq2Q4)

### 📄 Credit

- This app is subject to the [Riverside Valley Global Disclaimer](https://github.com/RiversideValley/.github/blob/main/DISCLAIMER.md).
- Many thanks to [Joseph Beattie](https://github.com/josephbeattie) for creating our current logo.
