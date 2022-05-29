namespace FluentHub.Octokit.Authorization
{
    public class OctokitAuthenticator
    {
        private static OctokitSecret Secrets { get; set; }

        public string RequestGitHubIdentityAsync(OctokitSecret secrets)
        {
            Secrets = secrets;

            OctokitOriginal.OauthLoginRequest request = new(secrets.ClientId)
            {
                // All scopes
                Scopes = {
                        "repo",
                        "workflow",
                        "write:packages",
                        "delete:packages",
                        "admin:org",
                        "admin:public_key",
                        "admin:repo_hook",
                        "admin:org_hook",
                        "gist",
                        "notifications",
                        "user",
                        "delete_repo",
                        "write:discussion",
                        "admin:enterprise",
                        "admin:gpg_key"
                    },
            };

            Uri oauthLoginUrl = App.Client.Oauth.GetGitHubLoginUrl(request);

            return oauthLoginUrl.ToString();
        }

        public async Task<string> RequestOAuthTokenAsync(string code)
        {
            var request = new OctokitOriginal.OauthTokenRequest(Secrets.ClientId, Secrets.ClientSecret, code);
            var token = await App.Client.Oauth.CreateAccessToken(request);

            if (token != null)
            {
                App.AccessToken = token.AccessToken;
                App.Client.Credentials = new OctokitOriginal.Credentials(token.AccessToken);
                App.Connection = new(App.ProductInformation, token.AccessToken);
                Queries.Users.UserQueries userQueries = new();
                var login = await userQueries.GetViewerLogin();
                App.SignedInUserName = login;
                return token.AccessToken;
            }
            else
            {
                throw new ArgumentNullException(nameof(token));
            }
        }
    }
}
