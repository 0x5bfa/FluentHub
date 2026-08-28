# GitHub authentication

FluentHub uses the GitHub OAuth Device Flow. Contributors do not need to create an OAuth app or configure local credentials before building the application.

The OAuth client ID is embedded in `AuthorizationService` because client IDs are public identifiers. Device Flow does not use a client secret, and no client secret should be added to the application or its package.

After GitHub authorizes a user, FluentHub validates the returned access token by resolving the signed-in identity. The token is then stored under that GitHub login in Windows Credential Locker. It is never stored in application settings or source-controlled files.

Existing installs that stored an access token in application settings migrate it to Windows Credential Locker on first launch and remove the plaintext setting after the secure write succeeds.

For more information, see:

- [Authorizing OAuth apps with Device Flow](https://docs.github.com/en/apps/oauth-apps/building-oauth-apps/authorizing-oauth-apps#device-flow)
- [Credential Locker for Windows apps](https://learn.microsoft.com/windows/apps/develop/security/credential-locker)
