<p align="center">
  <img alt="Logo" src="https://avatars.githubusercontent.com/u/98096883?s=400&u=d9d8b793aee2e6e6a9646c8482e3a8d5a5e181c3&v=4" width="100px" />
  <h1 align="center">FluentHub</h1>
</p>

![DownloadsCount](https://img.shields.io/github/downloads/fluenthub-uwp/FluentHub/total)
![License](https://img.shields.io/github/license/fluenthub-uwp/FluentHub)
![RepoSize](https://img.shields.io/github/repo-size/fluenthub-uwp/FluentHub)
[![CodeFactor](https://www.codefactor.io/repository/github/fluenthub-uwp/fluenthub/badge)](https://www.codefactor.io/repository/github/fluenthub-uwp/fluenthub)

## What is FluentHub?

`FluentHub` is the GitHub Desktop which conform to [Fluent Design System](https://www.microsoft.com/design/fluent) using GitHub API [v3(Rest API)](https://developer.github.com/v3/) and [v4(GraphQL API)](https://developer.github.com/v4/) on Windows.

> **Warning**⚠️<br> This project is in dev stage. Expect regular breaking changes.

>**Need help**🔧<br/>Any trivial suggestions or corrections are fine. Feel free to open a [issue](https://github.com/onein528/FluentHub/issues/new)/[PR](https://github.com/onein528/FluentHub/compare).<br>**If you want to contribute to this repository in earnest, please [request to become a member](https://github.com/onein528/FluentHub/issues/new).**

## Screenshot
* User profile page which have not been developed yet with light mode

  ![image](https://user-images.githubusercontent.com/62196528/152377145-9dcc3adc-6bfc-4244-bf7d-77eb9f5f547c.png)

* Repository page which have not been developed yet with light mode

  ![image](https://user-images.githubusercontent.com/62196528/152377118-ae0488a1-95a4-4198-abae-2a60a4cb25ca.png)

## Build from source

### 1. Preparation

- Visual Studio 2022 with the Windows SDK.

### 2. Clone the repo

Click "Open with Visual Studio" on this repo page

### 3. Add app credentials

1. After cloning this repository to your local machine, create a file named `AppCredentials.config` in the root of your FluentHub repo with Solution Explorer and update it as follows:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <client>
        <type key="id" value="YOUR-APP-ID"/>
        <type key="secret" value="YOUR-APP-SECRET"/>
    </client>
</configuration>
```

2. Set and get the value on [this site](https://github.com/settings/applications/new) as follows:

Name|Value
---|---
Application name|`FluentHub`<br/>
Homepage URL|`https://github.com/fluenthub-uwp/FluentHub`<br/>
Application description|*Optional*<br/>
Authorization callback URL|`fluenthub://`<br/>
Application logo|Use `/Assets/AppTiles/DefaultLogo.png`<br/>
Badge background color|`#2D333B` or your favorite color<br/>


## Our goals

* Multi-tab support
  * Go back and forward on the page(now disabled)
  * Jump to the page with the URL of GitHub
  * Share button (URL copy) for current page
* App settings
  * AppTheme
  * Default new page
* Octokit.NET authorization
  * Make up for missing API requests with HttpRequest class and [GraphQL.NET](https://graphql-dotnet.github.io/).
* Support for markdown converted to Html using WebView(NavigateToString not available in WebView2😥)
* Design following pages:

Features|Expected URL|Priority|Dev|More Info
---|---|:---:|:---:|---
**User Profile Page**||Must|🟢|
User's contribution graph|`/{username}`|Must|🔵|
User's README.md on profile page|`/{username}`|Must|🟢|
User's star list|`/{username}?tab=stars`, `/stars`|Must|🟢|Sorting and searching not yet coded
User's pinned items(v4)|`/{username}`|Must|🟢
User's repository list|`/{username}?tab=repositories`|Must|🟢|Sorting and searching not yet coded
User's issue list|`/issues`|Could|🔵|
User's Pull list|`/pulls`|Could|🔵|
User's dicussion list|`/discussions`|Could|🔴|
User settings page|`/settings/profile`|Must|🔴|
**Organization Profile Page**||Must|🔵|
Org's contribution graph|`/{username}`|Must|🔴|
Org's pinned items(v4)|`/{username}`|Must|🟢|
Org's repository list|`/{username}?tab=repositories`|Must|🟢|
Org settings page|`/settings/profile`|Must|🔴|
**Repository page**||Must|🟢|
Repo's code page(ListView)|`/{user(org)name}/{reponame}`|Must|🟢|
Repo's commit page|`/{user(org)name}/{reponame}/commits/{branch}`|Must|🔴|
Repo's issue list|`/{user(org)name}/{reponame}/issues`|Must|🟢|Sorting and searching not yed coded
Repo's Pull list|`/{user(org)name}/{reponame}/pulls`|Must|🟢|Sorting and searching not yed coded
Repo setings page|`/organizations/{user(org)name}/settings/profile`|Must|🔴|

> **Dev status🚩**<br/>🔴 To do<br/>🔵 In progress<br/>🟢 Done
