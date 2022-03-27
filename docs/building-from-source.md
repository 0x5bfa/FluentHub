# Build from source

## Preparation

Ensure you have these packages:

- Visual Studio 2022 with the Windows SDK.
- UWP Development kits

## Clone the repo

- Click `Open with Visual Studio` on this repo page

## Add app credentials

1. Set some values on [this site](https://github.com/settings/applications/new):

Name|Value|Requred
---|---|---
Application name|`FluentHub`|True
Homepage URL|`https://github.com/fluenthub-community/FluentHub`|True
Application description|A fluent GitHub app for Windows|False
Authorization callback URL|`fluenthub://auth`|True
Application logo|Use [this](https://github.com/fluenthub-community/FluentHub/blob/main/src/FluentHub/Assets/AppTiles/StoreLogo.scale-400.png)|True
Badge background color|`#FFFFFF`|True (but whatever)

2. Create a file named `AppCredentials.config` in the root of your FluentHub repo with Solution Explorer
3. change `id` and `secret` node value.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <client>
        <type key="id" value="YOUR-APP-ID"/>
        <type key="secret" value="YOUR-APP-SECRET"/>
    </client>
</configuration>
```
