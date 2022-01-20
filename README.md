<h1 align="center">
  <img alt="Logo" src="FluentHub/Assets/AppTiles/StoreLogo.scale-400.png" width="72" />
  <br/>
  FluentHub
</h1>

## What's this?

`FluentHub` is the GitHub Desktop which conform to [Fluent Design System](https://www.microsoft.com/design/fluent) using GitHub API [v3(Rest API)](https://developer.github.com/v3/) and [v4(GraphQL API)](https://developer.github.com/v4/) on Windows.

> **Warning**⚠️<br> This project is in beta stage. Expect regular breaking changes.

>**Need help**🔧<br/>Any trivial suggestions or corrections are fine. Feel free to open a [issue](https://github.com/onein528/FluentHub/issues/new)/[PR](https://github.com/onein528/FluentHub/compare).<br>*If you want to contribute to this repository in earnest, please [request a this project member](https://github.com/onein528/FluentHub/issues/new).*

## Build from source

### 1. Preparation

- Visual Studio 2022 with the Windows SDK.

### 2. Clone the repo

Click "Open Visual Studio" on this repo page

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

2. Set and get the value on [this site](https://github.com/settings/applications/new) as follows (Repository members get from [here](https://github.com/organizations/fluenthub-uwp/settings/applications/)):

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

Features|Expected URL|Priority|Dev
---|---|:---:|:---:
**User Profile Page**||Must|🔵
User's contribution graph|`/{username}`|Must|🔵
User's README.md on profile page|`/{username}`|Must|🔵
User's star list|`/{username}?tab=stars`, `/stars`|Must|🔵
User's pinned items(v4)|`/{username}`|Must|🔵
User's repository list|`/{username}?tab=repositories`|Must|🔵
User's issue list|`/issues`|Could|🔴
User's Pull list|`/pulls`|Could|🔴
User's dicussion list|`/discussions`|Could|🔴
User settings page|`/settings/profile`|Must|🔴
**Organization Profile Page**||Must|🔴
Org's contribution graph|`/{username}`|Must|🔴
Org's pinned items(v4)|`/{username}`|Must|🔴
Org's repository list|`/{username}?tab=repositories`|Must|🔴
Org settings page|`/settings/profile`|Must|🔴
**Perository page**||Must|🔴
Repo's code page(ListView)|`/{user(org)name}/{reponame}`|Must|🔴
Repo's commit page|`/{user(org)name}/{reponame}/commits/{branch}`|Must|🔴
Repo's issue list|`/{user(org)name}/{reponame}/issues`|Must|🔴
Repo's Pull list|`/{user(org)name}/{reponame}/pulls`|Must|🔴
Repo setings page|`/organizations/{user(org)name}/settings/profile`|Must|🔴

> **Dev status🚩**<br/>🔴 To do<br/>🔵 In progress<br/>🟢 Done


## Screenshot
> User profile page which have not been developed yet

![image](https://user-images.githubusercontent.com/62196528/150352810-a00db82e-daf4-4e2c-88c7-f492150e1a8c.png)
