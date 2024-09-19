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
  <a style="text-decoration:none" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
    <picture>
      <source media="(prefers-color-scheme: light)" srcset="https://get.microsoft.com/images/en-us%20dark.svg" width="200" />
      <img src="https://get.microsoft.com/images/en-us%20light.svg" width="200" />
    </picture></a>
</p>

FluentHub is the stylish yet powerful GitHub client for Windows, which enpowers development experience and follows Microsoft Design Language.

- **FluentHub UI:** designed with FluentUI and built on WinAppSdk/WinUI3
- **Multitasking:** with FluentHub you can multi-task with ease with tab support built-in to the app
- **Powerful page navigation:** easily navigate through pages without losing history or progress
- **Just like GitHub:** perform all of your everyday tasks on GitHub such as creating issues and pull requests with built-in API mutation

## Contributing to FluentHub

- [File an issue](https://github.com/FluentHub/FluentHub/issues/new/choose)
- [Submit your change](https://github.com/FluentHub/FluentHub/pulls)
- Upvote popular feature requests
- Join [our Discord](https://dsc.gg/fluenthub) and let us know what you think
- [Trabslate on Crowdin](https://crowdin.com/project/fluenthub)

## Screenshots

![Screenshot 2024-09-15 104438](https://github.com/user-attachments/assets/1728729b-0c8f-4cdb-aaf4-fbc7643b0bdf)

Your dashboard|Your repos|User profile page
---|---|---
![Screenshot 2024-09-15 104352](https://github.com/user-attachments/assets/c6e556c8-9fcb-4bfc-822d-08fde80eec2e)|![Screenshot 2024-09-15 104425](https://github.com/user-attachments/assets/3427a168-5bcc-4ac4-a7f2-761698c28eac)|![Screenshot 2024-09-15 104700](https://github.com/user-attachments/assets/d4ee0f1f-7e7b-4751-abf3-1df65ad16a99)




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
