namespace FluentHub.Octokit.Authorization
{
    public class AuthorizationService
    {
        private static OctokitSecrets Secrets { get; set; }

        public string RequestGitHubIdentityAsync(OctokitSecrets secrets)
        {
            Secrets = secrets;

            OctokitV3.OauthLoginRequest request = new(Secrets.ClientId);

            Uri oauthLoginUrl = App.Client.Oauth.GetGitHubLoginUrl(request);

            return oauthLoginUrl.ToString();
        }

        public async Task<string> RequestOAuthTokenAsync(string code)
        {
            if (Secrets == null) return null; 

            var request = new OctokitV3.OauthTokenRequest(Secrets.ClientId, Secrets.ClientSecret, code);
            var token = await App.Client.Oauth.CreateAccessToken(request);

            if (token != null)
            {
                // Initialize octokit.net and octokit.graphql.net
                InitializeOctokit.InitializeApiConnections(token.AccessToken);

                return token.AccessToken;
            }
            else
            {
                throw new ArgumentNullException("token");
            }
        }
    }
}
