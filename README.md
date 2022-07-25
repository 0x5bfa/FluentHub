<p align="center">
  <img width="128" align="center" src="assets/fluenthub.png" />
  <img width="128" align="center" src="assets/fluenthub-canary.png" />
  <img width="128" align="center" src="assets/fluenthub-dev.png" />
  <img width="128" align="center" src="assets/fluenthub-beta.png" />
</p>
<h1 align="center">
  FluentHub
</h1>
<p align="center">
  A stylish yet wonderfully powerful GitHub client
</p>

<p align="center">
  <a title="Azure Pipeline" target="_blank" href="https://dev.azure.com/fluenthub/FluentHub">
    <img src="https://dev.azure.com/fluenthub/FluentHub/_apis/build/status/Build%20Pipeline%20(x64)?branchName=main">
  </a>
  <a title="Crowdin" target="_blank" href="https://crowdin.com/project/fluenthub">
    <img src="https://badges.crowdin.net/fluenthub/localized.svg">
  </a>
  <a title="GitHub Releases" target="_blank" href="https://github.com/fluenthub-community/FluentHub/releases">
    <img src="https://img.shields.io/github/v/release/fluenthub-community/fluenthub?include_prereleases" alt="Release" />
  </a>
  <a title="Discord" target="_blank" href="https://discord.gg/8KtRkjq2Q4">
    <img src="https://img.shields.io/discord/935562861701390336?color=blue&label=Discord" alt="Discord" />
  </a>
  <a title="Platform" target="_blank">
    <img src="https://img.shields.io/badge/Platform-Windows-red" alt="Platform" />
  </a>
</p>

---

## Installation

### Via Microsoft Store

This is our preferred method. This allows you to always be on the latest version when we release new builds with automatic updates.

<a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <img width="128" align="center" src="https://getbadgecdn.azureedge.net/images/English_L.png" />
</a>

### Via GitHub

Released builds can be manually downloaded from this repository's Releases page.

Download the `FluentHub_<versionNumber>.msixbundle` file from the `Assets` section. In order too install the app, you can simply double-click on the .msixbundle file, and the app installer should automatically run. If that fails for any reason, you can try the following command at a PowerShell prompt:

```powershell
# NOTE: If you are using PowerShell 7+, please run
#   Import-Module Appx -UseWindowsPowerShell
# before using Add-AppxPackage.

Add-AppxPackage FluentHub_<versionNumber>.msixbundle
```

### Building from source

See [the section](#building-the-code)

## Screenshots

*May not always be up-to-date due to massive changes to the user interface*

![image](https://user-images.githubusercontent.com/62196528/180275377-c35e5348-2e8c-44e5-ba57-ab4199547589.png)
![image](https://user-images.githubusercontent.com/62196528/180275385-4534c2cd-e309-47e7-bc73-d25298957445.png)

## Feedback

- [Request a new feature](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- Join [our Discord](https://discord.gg/8KtRkjq2Q4) and let us know what you think

## Contributing

There are many ways where you participate in this community:

- [Submit bugs and feature requests](https://github.com/FluentHub/FluentHub/issues/new/choose).
- Review [the documentation](docs/code-style.md) and make pull requests for anything from typos to additional and new idea
- Review source code changes

If you are interested in fixing issues and contributing directly to the code base, please see the document [How to Contribute](docs/), which covers the following:

- [How to build and run from source](docs/)
- The development workflow, including debugging and running tests
- Coding guidelines
- [Submitting pull requests](https://github.com/FluentHub/FluentHub/pulls)
- [Finding an issue to work on](https://github.com/FluentHub/FluentHub/issues/new/choose)
- [Contributing to translations on Crowdin](https://crowdin.com/project/fluenthub)

## Contributors

<a href="https://github.com/FluentHub/FluentHub/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=FluentHub/FluentHub" />
</a>

### Special thanks

- Joseph Beattie([@josephbeattie](https://github.com/josephbeattie)) created our logo!

## Contact Us

The easiest way to contact us is to join [our Discord channel](https://discord.gg/8KtRkjq2Q4).

If you would like to ask a question, please reach out to us via Twitter:

- Tomoyuki Terashita, Lead developer: [@onein528](https://twitter.com/onein528)
- Jupiter, Developer: [@DeveloperWOW64](https://twitter.com/DeveloperWOW64)
- Gabriel Fontán, Developer: [@BobbyESPGabiles](https://twitter.com/BobbyESPGabiles)
- Luandersonn Airton, Developer: [@luandersonn](https://twitter.com/luandersonn)

## Building the Code

### 1. Prerequisites

Ensure you have following components:

- Windows 10 2004 (10.0.19041.0) or later with enabled Developer Mode in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with following individual components:
  - the Windows 11 (10.0.22000.0) SDK
  - UWP Development Kit
  - [.NET SDK](https://dotnet.microsoft.com/en-us/download)

### 2. Git

Clone the repository:

```git
git clone https://github.com/FluentHub/FluentHub
```

Initialize submodules recursively:

```git
git submodule update --init --recursive
```

### 3. Prepare OAuth credentials

See [the documentation](docs/building-from-source.md).

### 4. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Uwp` in the Solution Explorer
- Build with `DEBUG|x64|FluentHub.Uwp (Universal Windows)`

## License

Copyright (c) 2022 FluentHub Team

Licensed under the MIT license stated in [LICENSE](LICENSE).
