<p align="center">
  <img alt="Logo" src="https://avatars.githubusercontent.com/u/98096883?s=400&u=d9d8b793aee2e6e6a9646c8482e3a8d5a5e181c3&v=4" width="100px" />
  <h1 align="center">FluentHub</h1>
</p>

[![Build Status](https://dev.azure.com/fluenthub-uwp/FluentHub/_apis/build/status/Build%20Pipeline?branchName=main)](https://dev.azure.com/fluenthub-uwp/FluentHub/_build/latest?definitionId=1&branchName=main)
[![CodeFactor](https://www.codefactor.io/repository/github/fluenthub-uwp/fluenthub/badge)](https://www.codefactor.io/repository/github/fluenthub-uwp/fluenthub)
![License](https://img.shields.io/github/license/fluenthub-uwp/FluentHub)
![RepoSize](https://img.shields.io/github/repo-size/fluenthub-uwp/FluentHub)

## What is FluentHub?

`FluentHub` is the GitHub Desktop which conform to [Fluent Design System](https://www.microsoft.com/design/fluent) using GitHub API [v3(Rest API)](https://developer.github.com/v3/) and [v4(GraphQL API)](https://developer.github.com/v4/) on Windows.

> **Warning**⚠️<br> This project is in dev stage. Expect regular breaking changes.

>**Need any help**🔧<br/>Feel free to open a [issue](https://github.com/onein528/FluentHub/issues/new)/[PR](https://github.com/onein528/FluentHub/compare).<br>If you want to contribute to this repository in earnest, please [request to become a member](https://github.com/onein528/FluentHub/issues/new).

## Builds

If you are building a project, you need to look at [this documentation](building-from-source.md).

## Our goals

* Multi-tab support
  * Go back and forward on the page(now disabled)
  * Jump to the page with the URL of GitHub
  * Share button (URL copy) for current page
* App settings
  * AppTheme
  * Default new page
* Octokit.NET authorization
  * Make up for missing API requests with HttpRequest class and GraphQL.NET.
* Support for rendered markdown displaying using WebView

## Translation

FluentHub is different from traditional GitHub in that it needs to be made available in a variety of languages.
But we are currently in the Dev stage, so the scope of translation is limited.

See [this documentation](translations.md)

## Screenshot

* User profile page not yet developed:

  ![image](https://user-images.githubusercontent.com/62196528/152377145-9dcc3adc-6bfc-4244-bf7d-77eb9f5f547c.png)

* Repository page not yet developed:

  ![image](https://user-images.githubusercontent.com/62196528/152377118-ae0488a1-95a4-4198-abae-2a60a4cb25ca.png)
