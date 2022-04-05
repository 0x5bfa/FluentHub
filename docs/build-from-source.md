# Build from source

## Preparation

Make sure these packages are local:

- Visual Studio 2022 and Windows SDK.
- UWP Development Kit
- Source code for this project

## Add app credentials

This application uses a method called Oauth, so you must independently get a client ID and secret in your account. This is because it uses something similar to a password and cannot be disclosed.

> **Warning**</br>Since this method is cumbersome for open source app, we plan to remove this process in the future by exposing our own API, which is currently private.

 1.  Set some values on [your OAuth app settings](https://github.com/settings/developers):

		![image](https://user-images.githubusercontent.com/62196528/161755644-1de8e2ec-ddea-4b47-ae14-bc3c326a33f8.png)

		|Name|Value|Requred|
		|-|-|-|
		|Application name|`FluentHub`|True|
		|Homepage URL|`https://github.com/fluenthub-community/FluentHub`|True|
		|Application description|A fluent GitHub app for Windows|False|
		|Authorization callback URL|`fluenthub://auth`|True|
		|Application logo|Use [this](https://github.com/fluenthub-community/FluentHub/blob/main/src/FluentHub/Assets/AppTiles/StoreLogo.scale-400.png)|True|
		|Badge background color|`#FFFFFF`|True (whatever)|

 2.  Create a file named `AppCredentials.config` in the root of your FluentHub repo with Solution Explorer
 3.  Change `id` and `secret` node value

		```xml
		<?xml version="1.0" encoding="utf-8" ?>
		<configuration>
		    <client>
		        <type key="id" value="YOUR-APP-ID"/>
		        <type key="secret" value="YOUR-APP-SECRET"/>
		    </client>
		</configuration>
		```

		![image](https://user-images.githubusercontent.com/62196528/161758514-350c2d44-8ffc-402a-b67e-4ccc48c706df.png)

		![image](https://user-images.githubusercontent.com/62196528/161756202-8c269cc3-a955-402e-a40e-f143b6b36fc6.png)	
