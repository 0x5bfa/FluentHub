# <img width="64" align="center" src="assets/fluenthub.png" /> <img width="64" align="center" src="assets/fluenthub-dev.png" /> FluentHub

###### A stylish yet powerful GitHub client

<p align="center">
  <a title="Azure Pipeline" target="_blank" href="https://dev.azure.com/fluenthub/FluentHub">
    <img align="left" src="https://dev.azure.com/fluenthub/FluentHub/_apis/build/status/Build%20Pipeline%20(x64)?branchName=main">
  </a>
  <a title="Crowdin" target="_blank" href="https://crowdin.com/project/fluenthub">
    <img align="left" src="https://badges.crowdin.net/fluenthub/localized.svg">
  </a>
  <a title="GitHub Releases" target="_blank" href="https://github.com/fluenthub-community/FluentHub/releases">
    <img align="left" src="https://img.shields.io/github/v/release/fluenthub-community/fluenthub?include_prereleases" alt="Release" />
  </a>
  <a title="Discord" target="_blank" href="https://discord.gg/8KtRkjq2Q4">
    <img align="left" src="https://img.shields.io/discord/935562861701390336?color=blue&label=Discord" alt="Discord" />
  </a>
  <a title="Platform" target="_blank">
    <img align="left" src="https://img.shields.io/badge/Platform-Windows-red" alt="Platform" />
  </a>
</p>

<br/>

---

## ➕ Installation

##### Via Microsoft Store

Release mode is currently unstable and therefore packages shall not be uploaded for new versions.

<!--
This is our preferred method. This allows you to always be on the latest version when we release new builds with automatic updates.

<a title="Microsoft Store" target="_blank" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <img width="128" align="center" src="https://getbadgecdn.azureedge.net/images/English_L.png" />
</a>
-->

##### Via GitHub

Release mode is currently unstable and therefore packages shall not be uploaded for new versions.

<!--
Released builds can be manually downloaded from this [repository's Releases page](https://github.com/FluentHub/FluentHub/releases).

Download the `FluentHub_<versionNumber>.msixbundle` file from the `Assets` section. In order too install the app, you can simply double-click on the .msixbundle file, and the app installer should automatically run. If that fails for any reason, you can try the following command at a PowerShell prompt:

```powershell
# NOTE: If you are using PowerShell 7+, please run
#   Import-Module Appx -UseWindowsPowerShell
# before using Add-AppxPackage.

Add-AppxPackage FluentHub_<versionNumber>.msixbundle
```
-->

##### Building from source
###### ⭐Recommended⭐

This is our preferred method.
See [this section](#building-the-code)

## 📸 Screenshots

*May not always be up-to-date due to constant changes to the user interface*

![image](https://user-images.githubusercontent.com/62196528/183133469-88c60c73-9058-4a23-a781-642cbd3cd318.png)
![image](https://user-images.githubusercontent.com/62196528/183133521-42891ffa-7ada-485d-ac6d-a802e1b883b9.png)

## 🧑‍💻 Contributing

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

## 🦜 Feedback

- [Request a new feature](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- Join [our Discord](https://discord.gg/8KtRkjq2Q4) and let us know what you think

## 🔨 Building the Code

##### 1. Prerequisites

Ensure you have following components:

- Windows 10 2004 (10.0.19041.0) or later with Developer Mode on in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with following individual components:
  - the Windows 11 (10.0.22000.0) SDK
  - UWP Development Kit
  - [.NET SDK](https://dotnet.microsoft.com/en-us/download)

##### 2. Git

Clone the repository:

```git
git clone https://github.com/FluentHub/FluentHub
```

Initialize submodules recursively:

```git
git submodule update --init --recursive
```

##### 3. Prepare OAuth credentials

See [the documentation](docs/credentials.md).

##### 4. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Uwp` in the Solution Explorer
- Build with `DEBUG|x64|FluentHub.Uwp (Universal Windows)`

## 💳 Credit

- Many thanks to Joseph Beattie ([@josephbeattie](https://github.com/josephbeattie)) for creating our current logo.

## 📱 Contact
The easiest way to contact us is to join [our Discord](https://discord.gg/8KtRkjq2Q4).

If you would like to ask a question, please reach out to us via Twitter:

- Tomoyuki Terashita, Lead Developer: [@onein528](https://twitter.com/onein528)
- Jupiter, Developer: [@DeveloperWOW64](https://twitter.com/DeveloperWOW64)
- Gabriel Fontán, Developer: [@BobbyESPGabiles](https://twitter.com/BobbyESPGabiles)
- Luandersonn Airton, Developer: [@luandersonn](https://twitter.com/luandersonn)

## ⚖️ License

Copyright (c) 2022 FluentHub Team

Licensed under the MIT license stated in the [LICENSE](LICENSE).

---

<a href="https://github.com/FluentHub/FluentHub/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=FluentHub/FluentHub" />
</a>
