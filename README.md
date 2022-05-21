<p align="center">
  <img width="128" align="center" src="src/FluentHub/Assets/AppTilesAlpha/StoreLogo.scale-400.png" />
</p>
<h1 align="center">
  FluentHub Alpha
</h1>
<p align="center">
  A fluent yet powerful GitHub Oauth client app made with Fluent Design and WinUI 2.7
</p>

<p align="center">
  <a style="text-decoration:none" href="https://github.com/fluenthub-community/FluentHub/releases">
    <img src="https://img.shields.io/github/v/release/fluenthub-community/fluenthub?include_prereleases&style=flat-square" alt="Release" />
  </a>
  <a style="text-decoration:none" href="https://discord.gg/8KtRkjq2Q4">
    <img src="https://img.shields.io/discord/935562861701390336?color=blue&label=Discord&style=flat-square" alt="Discord" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/badge/Platform-Windows-red?style=flat-square" alt="Platform" />
  </a>
</p>

## Download and Build

<a style="text-decoration:none" href="https://apps.microsoft.com/store/detail/fluenthub/9nkb9hx8rjz3">
  <img width="128" src="https://getbadgecdn.azureedge.net/images/English_L.png" alt="GetItFromMicrosoft" />
</a>

> **Attention**</br>The app from ms store has issues during authentication that do not occur in DEBUG configured FluentHub. We are tracking on [#138](https://github.com/fluenthub-community/FluentHub/issues/138). Due to this issue, we are not publishing packages to GitHub Releases and Microsoft Store because the RELEASE configured FluentHub does not work properly. So need to build from source. Please take a look at [build-from-source](https://github.com/fluenthub-community/FluentHub/blob/main/docs/build-from-source.md). 
## Screenshots

![image](https://user-images.githubusercontent.com/62196528/167259072-adedd9c3-c979-48a5-96f9-f37ddc87b662.png)
![image](https://user-images.githubusercontent.com/62196528/167259003-500c79b0-f301-4bd7-82b6-5d9ad7473118.png)
![image](https://user-images.githubusercontent.com/62196528/167263705-7068e9d9-8086-4bde-a445-84a8e9c09136.png)

## Notices

 1. HttpsClient in Octokit.GraohQL.NET does not work properly (occur unknown exception) for users using Win11 Dev/Beta version with TLS and SSL settings disabled.</br>`Win+R` > `inetcpl.cpl` > `Advanced` tab > enable all TLS and SSL versions > restart

	![image](https://user-images.githubusercontent.com/99880210/164863685-27770148-4c68-4920-bf87-8c0dd2b0272f.png)

## FAQ

Any questions? Ask on our [Discord server](https://discord.gg/8KtRkjq2Q4).

## Contributing

- [How to contribute](docs/CONTRIBUTING.md)
- Have any concepts/bugs? let us know in GitHub [issue](https://github.com/fluenthub-community/FluentHub/issues)/[PR](https://github.com/fluenthub-community/FluentHub/pulls) or Discord.

## Privacy

We may use the App Center to track the settings used, find bugs and fix crashes. The information sent to the App Center is anonymous and does not contain any user or contextual data.
