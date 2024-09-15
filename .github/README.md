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
  <a title="GitHub" href="https://github.com/0x5BFA/FluentHub/releases/latest">
    <img src="https://user-images.githubusercontent.com/74561130/160255105-5e32f911-574f-4cc4-b90b-8769099086e4.png" width="157" alt="Get it from GitHub" />
  </a>
</p>

FluentHub is the stylish yet powerful GitHub client for Windows, which enpowers development experience and follows Microsoft Design Language.

- **Modern UI:** designed with Fluent Design and built on WinAppSdk/WinUI3
- **Multitasking:** let's do multitasking with tabs, you can switch tabs without losing data
- **Powerful page navigation:** navigation can be performed like browsers without losing navigation history
- **Querying/Mutation:** perform any kind of fetch and modification on GitHub, the app supports as far as GitHub API supports using Octokit.NET and Octokit.GraphQL.NET.

## Contributing to FluentHub

- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- [Submit your change](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- Join [our Discord](https://dsc.gg/fluenthub) and let us know what you think
- [Trabslate on Crowdin](https://crowdin.com/project/fluenthub)

## Screenshots

Home page|PR page|User profile page
---|---|---
![image](https://github.com/0x5bfa/FluentHub/assets/62196528/a31bdace-8700-4a6a-83e9-1cdc52955c4f)|![image](https://github.com/0x5bfa/FluentHub/assets/62196528/a29c4ef8-1fe5-47c3-be03-6afebe02c55b)|![image](https://github.com/0x5bfa/FluentHub/assets/62196528/35ffbe36-00d3-4d04-9019-67307febfc95)

## Building the Code

### Requirements

- Windows 10 (Build 10.0.19041.0) or newer with Developer Mode enabled in the Windows Settings
- [Git](https://git-scm.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/):
  - [Windows App SDK (version 10.0.22621.0)](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/)
  - .NET 8 SDK (.NET Desktop Development workload)
  - Windows App SDK

### 1. Clone the repository

```git
git clone https://github.com/FluentHub/FluentHub
```

### 2. Prepare OAuth credentials

See [the documentation](docs/credentials.md).

> [!WARNING]  
> If you skip this step, Visual Studio will give a fatal error that the `AppCredentials.config` file does not exist.

### 3. Build the project

- Open `FluentHub.sln`.
- Hit 'Set as Startup item' on `FluentHub.Package` in the Solution Explorer.
- Build with `Debug`, `x64`, `FluentHub.Package`.

## Credit

- Some application icons were created by [Icons8](https://github.com/icons8).
- Many thanks to [Joseph Beattie](https://github.com/josephbeattie) for creating our current logo.
