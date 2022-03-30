# Build from source

## Preparation

Ensure you have these packages:

- Visual Studio 2022 with the Windows SDK.
- UWP Development kits

## Clone the repo

- Click `Open with Visual Studio` on this repo page

## Add app credentials

1. Set some values on [this site](https://github.com/settings/applications/new):

Name|Value
---|---
Application name|`FluentHub`<br/>
Homepage URL|`https://github.com/fluenthub-community/FluentHub`<br/>
Application description|*Optional*<br/>
Authorization callback URL|`fluenthub://`<br/>
Application logo|Use [this](https://github.com/fluenthub-community/FluentHub/blob/main/src/FluentHub/Assets/AppTiles/StoreLogo.scale-400.png)<br/>
Badge background color|`#FFFFFF` or your favorite color<br/>

**WARNING!**. For the security of our users and app, we are probably changing this method soon by retrieving these values directly from an own-made [API](https://github.com/fluenthub-community/FluentHub_CredentialsAPI) that's going to be open sourced but by the moment, it's a private template. For now, we're keeping this method until the API fully tested and functional for production.

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
