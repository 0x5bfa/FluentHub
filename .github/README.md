<p align="center">
  <img alt="FluentHub hero image" src="./assets/header.png" />
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

We welcome community contributions. You can [file an issue](https://github.com/FluentHub/FluentHub/issues/new/choos), propose [your changes](https://github.com/FluentHub/FluentHub/pulls), join [our Discord channel](https://dsc.gg/fluenthub) to connect with us. We especially appreciate help with translating the app [on Crowdin](https://crowdin.com/project/fluenthub)—your contributions make a big difference in reaching a wider audience!

Looking for a place to start? Check out [the task board](https://github.com/users/0x5bfa/projects/7/views/2), where you can sort tasks by size and priority.

## Screenshots

![PR page screenshot](./assets/screenshots/page-pr.png)

Your dashboard|Your repos|User profile page
---|---|---
![Dashboard page screenshot](./assets/screenshots/page-dashboard.png)|![Repo page screenshot](./assets/screenshots/page-repo.png)|![User page screenshot](./assets/screenshots/page-user.png)

## Building the Code

### 1. Prerequisites

- Windows 10 (Build 10.0.19041.0) or newer with Developer Mode enabled in the Windows Settings
- [Visual Studio](https://visualstudio.microsoft.com/vs/) with the WinUI application development workload
- .NET 10 SDK

```
git clone https://github.com/FluentHub/FluentHub
```

### 2. Prepare OAuth credentials

See [the documentation](../docs/credentials.md).

The app builds without credentials, but GitHub sign-in remains unavailable until you copy and configure the example file:

```powershell
Copy-Item src\FluentHub.App\AppCredentials.config.example src\FluentHub.App\AppCredentials.config
```

### 3. Build the project

- Open `FluentHub.slnx`.
- Set `FluentHub.App` as the startup project.
- Build with the `Debug` and `x64` configuration.

## Credit

- Some application icons were created by [Icons8](https://github.com/icons8).
- Many thanks to [Joseph Beattie](https://github.com/josephbeattie) for creating our current logo.
- Join [Developer Sanctuary](https://dsc.gg/devsanx) to learn more about our project!
