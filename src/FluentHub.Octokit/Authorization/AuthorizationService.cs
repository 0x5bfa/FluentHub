namespace FluentHub.Octokit.Authorization
{
    public class AuthorizationService
    {
        private static OctokitSecrets Secrets { get; set; }

        public string RequestGitHubIdentityAsync(OctokitSecrets secrets)
        {
            Secrets = secrets;

            OctokitOriginal.OauthLoginRequest request = new(Secrets.ClientId)
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

        public async Task RequestOAuthTokenAsync(string code)
        {
            if (Secrets == null) return;

            var request = new OctokitOriginal.OauthTokenRequest(Secrets.ClientId, Secrets.ClientSecret, code);
            var token = await App.Client.Oauth.CreateAccessToken(request);

            if (token != null)
            {
                App.Client.Credentials = new OctokitOriginal.Credentials(token.AccessToken);
                App.Connection = new(App.ProductInformation, token.AccessToken);

                App.AccessToken = token.AccessToken;

                Queries.Users.UserQueries queries = new();
                App.SignedInUserName = await queries.GetViewerLogin();
            }
            else
            {
                throw new ArgumentNullException("token.AccessToken");
            }
        }
    }
}
