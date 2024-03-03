<p align="center">
  <img width="128" align="center" src="assets/fluenthub.png" />
</p>
<h1 align="center">
  FluentHub
</h1>
<p align="center">
  The stylish yet powerful GitHub client for Windows.
</p>

<p align="center">
  <a title="Pipeline" target="_blank" href="https://dev.azure.com/fluenthub/FluentHub/_build/latest?definitionId=10&amp;branchName=main">
    <img src="https://dev.azure.com/fluenthub/FluentHub/_apis/build/status%2FFluentHub%20CI?branchName=main&amp;jobName=Build%20Debug%20x64" alt="Release" /></a>
  <a title="GitHub Releases" target="_blank" href="https://github.com/fluenthub-community/FluentHub/releases">
    <img src="https://img.shields.io/github/v/release/fluenthub-community/fluenthub?include_prereleases" alt="Release" /></a>
  <a title="Discord" target="_blank" href="https://discord.gg/8KtRkjq2Q4">
    <img src="https://dcbadge.vercel.app/api/server/8KtRkjq2Q4?style=flat" alt="Platform" /></a>
  <a title="Crowdin" target="_blank" href="https://crowdin.com/project/fluenthub">
    <img src="https://badges.crowdin.net/fluenthub/localized.svg" alt="Platform" /></a>
</p>

## Installation and running FluentHub

### Requirements

- Windows 10 or Windows 11 (Build 10.0.19041.0 or newer)

### Via Microsoft Store

This is the preferred installation method. It allows you to always be on the latest version when we release new builds via automatic updates.

<a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <img width="128" align="center" src="https://get.microsoft.com/images/en-us%20dark.svg" />
</a>

### Via GitHub

Released builds can be manually downloaded from this [repository's releases page](https://github.com/FluentHub/FluentHub/releases).

Download the `FluentHub_<versionNumber>.msixbundle` file from the `Assets` section. In order to install the app, you can simply double-click on the .msixbundle file, and the app installer should automatically run. If that fails for any reason, you can try the following command with a PowerShell prompt:

```powershell
# NOTE: If you are using PowerShell 7+, please run
#   Import-Module Appx -UseWindowsPowerShell
# before using Add-AppxPackage.

Add-AppxPackage FluentHub_<versionNumber>.msixbundle
```

## Screenshots

**Home page**
![image](https://github.com/0x5bfa/FluentHub/assets/62196528/a31bdace-8700-4a6a-83e9-1cdc52955c4f)

**PR page**
![image](https://github.com/0x5bfa/FluentHub/assets/62196528/a29c4ef8-1fe5-47c3-be03-6afebe02c55b)

**User profile page**
![image](https://github.com/0x5bfa/FluentHub/assets/62196528/35ffbe36-00d3-4d04-9019-67307febfc95)

## Building the Code

### Requirements

- Windows 10 (Build 10.0.19041.0) or newer with Developer Mode enabled in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/):
  - [Windows App SDK (version 10.0.22621.0)](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/)
  - .NET 7 SDK (check the box named .NET Desktop Development)
  - Windows App SDK

### 1. Clone the repository

```git
git clone https://github.com/FluentHub/FluentHub
```

### 2. Prepare OAuth credentials

See [the documentation](docs/credentials.md).

> [!IMPORTANT]  
> If you skip this step, Visual Studio will give a fatal error that the `AppCredentials.config` file does not exist.

### 3. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Package` in the Solution Explorer.
- Build with `Debug`, `x64`, `FluentHub.Package`.

## Contributing

There are multiple ways to participate in the community:

- [Submit bugs and feature requests](https://github.com/FluentHub/FluentHub/issues/new/choose).
- Review [the documentation](docs/code-style.md) and make pull requests for anything from typos to additional and new idea
- Review source code changes

If you are interested in fixing issues and contributing directly to the code base, please refer to the [documentation](docs/), which covers the following:

- [How to build and run from source](docs/)
- The development workflow, including debugging and running tests
- Coding guidelines
- [Submitting pull requests](https://github.com/FluentHub/FluentHub/pulls)
- [Finding an issue to work on](https://github.com/FluentHub/FluentHub/issues/)
- [Contributing to translations on Crowdin](https://crowdin.com/project/fluenthub)

<a href="https://crowdin.com/project/fluenthub" rel="nofollow">
  <img style="width:140;height:40" src="https://badges.crowdin.net/badge/dark/crowdin-on-light.png" /></a>

## Feedback

- [Request a new feature](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- Join [our Discord](https://discord.gg/8KtRkjq2Q4) and let us know what you think

[![](https://dcbadge.vercel.app/api/server/8KtRkjq2Q4?style=flat)](https://discord.gg/8KtRkjq2Q4)

## Credit

- Some application icons were created by [Icons8](https://github.com/icons8).
- Many thanks to [Joseph Beattie](https://github.com/josephbeattie) for creating our current logo.

![Alt](https://repobeats.axiom.co/api/embed/15ef16427b681d911523e65d60d88a838c9d4fc3.svg "Repobeats analytics image")
