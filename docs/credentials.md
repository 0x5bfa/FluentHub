# Prepare OAuth credentials

This application uses Oauth, so you must independently get a client ID and secret in your account. This is because it uses something similar to a password and cannot be disclosed.

 1.  Set some values on [your OAuth app settings](https://github.com/settings/developers):

		![image](https://user-images.githubusercontent.com/62196528/161755644-1de8e2ec-ddea-4b47-ae14-bc3c326a33f8.png)

		|Name|Value|Required|
		|-|-|-|
		|Application name|`FluentHub`|True|
		|Homepage URL|`https://github.com/FluentHub/FluentHub`|True|
		|Application description|A fluent GitHub app for Windows|False|
		|Authorization callback URL|`fluenthub://auth`|True|
		|Application logo|Use [this](https://user-images.githubusercontent.com/62196528/181265200-0f331fd0-e0b3-4896-8c6c-8468c8fd714f.png)|True|
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
