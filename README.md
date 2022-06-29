<p align="center">
  <img width="128" align="center" src="src/FluentHub/Assets/AppTilesAlpha/StoreLogo.scale-400.png" />
</p>
<h1 align="center">
  FluentHub Alpha
</h1>
<p align="center">
  A stylish yet wonderfully powerful GitHub Oauth client made with Fluent Design and WinUI
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

> **Warning**</br>The app available on the store does not work properly.</br>See the [Linq problem](#2-linq-problem) section

## FluentHub Team

Our team are mostly students (age 17 and under). We have had to suspend the project for several weeks due to our exams. So if this project has no updates for a few days/weeks, do not consider it abandoned. FluentHub's suspension will end (approximately) on the 6th September.

## Screenshots
*May not always be up-to-date due to massive changes to the user interface*

![image](https://user-images.githubusercontent.com/62196528/170248747-1c7458f6-4b22-48e3-9235-0c8561a1759a.png)
![image](https://user-images.githubusercontent.com/62196528/170248759-cbf061b4-6eff-4db9-b61b-12e4e3c413a2.png)
![image](https://user-images.githubusercontent.com/62196528/170248768-cff52abc-fdfc-4b61-bd16-8c89ee9624ce.png)
![image](https://user-images.githubusercontent.com/62196528/170248775-1ef2fe2f-bdf7-4f45-9a01-adbe1bc634fb.png)

## Warnings

#### 1. HttpClient problem

HttpClient used in `Octokit.GraphQL.NET` does not work properly for users using Windows 11 Dev/Beta versions with `TLS` and `SSL` settings disabled. https://github.com/FluentHub/FluentHub/issues/169
</br>Try: `Win+R` > `inetcpl.cpl` > `Advanced` tab > enable all `TLS` and `SSL` versions > restart

![image](https://user-images.githubusercontent.com/99880210/164863685-27770148-4c68-4920-bf87-8c0dd2b0272f.png)

#### 2. Linq problem

`Select() is not supported` exception from Octokit.GraphQL.NET https://github.com/FluentHub/FluentHub/issues/169, https://github.com/FluentHub/FluentHub/issues/138, https://github.com/FluentHub/FluentHub/issues/129, https://github.com/FluentHub/FluentHub/issues/123, https://github.com/FluentHub/FluentHub/issues/116, https://github.com/FluentHub/FluentHub/issues/104

> Upon checking, there was no problem with authentication, and there was a problem with the code that was getting the user's information at the time of authentication. But this wasn't an exception caused by our code, it was caused by [Octokit.GraphQL.](https://github.com/octokit/octokit.graphql.net) So the lead developer [reported it to the GitHub Octokit.GraphQL team](https://github.com/octokit/octokit.graphql.net/issues/262), but the project had seemingly been abandoned as he hadn't received a reply for over a week. We didn't want to support a non-maintainable project, and we came up with a new solution, but the cost of utilising this idea would be huge and it would make the code utterly verbose. As a result, a decision was made: The FluentHub team shall not publish the release mode app package to the Microsoft Store or the release page on GitHub. 

> *- [onein528](https://github.com/onein528) 6/12/2022*

## FAQ

Any questions? Ask on our [Discord server](https://discord.gg/8KtRkjq2Q4).

## Contributing

- [How to contribute](https://hub.codrex.dev/docs/contrib)
- Have any concepts/bugs? let us know in an [issue](https://github.com/fluenthub-community/FluentHub/issues)/[PR](https://github.com/fluenthub-community/FluentHub/pulls) or via Discord.

## Privacy

We may use the App Center to track the settings used, find bugs and fix crashes. The information sent to the App Center is anonymous and does not contain any user or contextual data.
