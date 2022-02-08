## Build from source

### 1. Preparation

- Visual Studio 2022 with the Windows SDK.

### 2. Clone the repo

- Click `Open with Visual Studio` on this repo page

### 3. Add app credentials


1. Set some values on [this site](https://github.com/settings/applications/new):

Name|Value
---|---
Application name|`FluentHub`<br/>
Homepage URL|`https://github.com/fluenthub-uwp/FluentHub`<br/>
Application description|*Optional*<br/>
Authorization callback URL|`fluenthub://`<br/>
Application logo|Use `/Assets/AppTiles/DefaultLogo.png`<br/>
Badge background color|`#2D333B` or your favorite color<br/>

2. Create a file named `AppCredentials.config` in the root of your FluentHub repo with Solution Explorer and update it as follows:</br>Set the values got in 1. for `YOUR-APP-ID` and `YOUR-APP-SECRET`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <client>
        <type key="id" value="YOUR-APP-ID"/>
        <type key="secret" value="YOUR-APP-SECRET"/>
    </client>
</configuration>
```

## 4. You're good to go!
