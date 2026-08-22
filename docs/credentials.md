# Prepare OAuth credentials

This application uses GitHub OAuth Device Flow. You must independently get a client ID from an OAuth app in your account.

 1.  Set some values on [your OAuth app settings](https://github.com/settings/developers):

		![image](https://user-images.githubusercontent.com/62196528/161755644-1de8e2ec-ddea-4b47-ae14-bc3c326a33f8.png)

		|Name|Value|Required|
		|-|-|-|
		|Application name|`FluentHub`|True|
		|Homepage URL|`https://github.com/FluentHub/FluentHub`|True|
		|Application description|A fluent GitHub app for Windows|False|
		|Authorization callback URL|`http://127.0.0.1`|True|
		|Application logo|Use [this](https://user-images.githubusercontent.com/62196528/181265200-0f331fd0-e0b3-4896-8c6c-8468c8fd714f.png)|True|
		|Badge background color|`#FFFFFF`|True (whatever)|

 2.  Enable Device Flow in the OAuth app settings.
 3.  Open `src/FluentHub/AppCredentials.config` and replace the placeholder client ID. The client secret can be empty because Device Flow does not use it.

		```xml
		<?xml version="1.0" encoding="utf-8" ?>
		<configuration>
		    <client>
		        <type key="id" value="YOUR_OAUTH_APP_CLIENT_ID"/>
		        <type key="secret" value="YOUR_OAUTH_APP_CLIENT_SECRET"/>
		    </client>
		</configuration>
		```

 4.  To keep your local credentials out of normal Git status output, mark the tracked file as `skip-worktree`:

		```powershell
		git update-index --skip-worktree src/FluentHub/AppCredentials.config
		```

	To receive future repository updates to the placeholder file, clear the local flag first with `git update-index --no-skip-worktree src/FluentHub/AppCredentials.config`.

		![image](https://user-images.githubusercontent.com/62196528/161758514-350c2d44-8ffc-402a-b67e-4ccc48c706df.png)

		![image](https://user-images.githubusercontent.com/62196528/161756202-8c269cc3-a955-402e-a40e-f143b6b36fc6.png)	
