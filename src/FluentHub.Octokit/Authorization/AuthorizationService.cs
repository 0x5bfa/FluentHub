using Serilog;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.System;

namespace FluentHub.Octokit.Authorization
{
    public class AuthorizationService
    {
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }

        public async Task RequestGitHubIdentityAsync()
        {
            await LoadAppCredentialsAsync();

            OctokitOriginal.OauthLoginRequest request = new(ClientId)
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

            await Launcher.LaunchUriAsync(oauthLoginUrl);
        }

        public async Task RequestOAuthTokenAsync(string code)
        {
            await LoadAppCredentialsAsync();

            var request = new OctokitOriginal.OauthTokenRequest(ClientId, ClientSecret, code);
            var token = await App.Client.Oauth.CreateAccessToken(request);

            if (token != null)
            {
                App.Client.Credentials = new OctokitOriginal.Credentials(token.AccessToken);

                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                var accessToken = localSettings.Values["AccessToken"] = token.AccessToken;

                // Get viewer login name
                Queries.Users.UserQueries queries = new();
                string login = await queries.GetViewerLogin();

                var signedInUserName = localSettings.Values["SignedInUserName"] = login;

                var signedInUserLogins = localSettings.Values["SignedInUserLogins"] as string;
                string signedInLogin;

                if (string.IsNullOrEmpty(signedInUserLogins))
                {
                    signedInLogin = login;
                }
                else
                {
                    var dividedLogins = signedInUserLogins.Split(",").ToList();
                    dividedLogins.Remove(login); // Double checking
                    signedInLogin = string.Join(",", dividedLogins);

                    if (!string.IsNullOrEmpty(signedInLogin))
                    {
                        signedInLogin += ",";
                    }

                    signedInLogin += login;
                }

                localSettings.Values["SignedInUserLogins"] = signedInLogin;
            }
            else
            {
                throw new ArgumentNullException("AccessToken");
            }
        }

        private async Task LoadAppCredentialsAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppCredentials.config"));

            var xmlDoc = await XmlDocument.LoadFromFileAsync(file);

            var nodeId = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='id']/@value");
            var nodeSecret = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='secret']/@value");

            ClientId = (string)nodeId.NodeValue;
            ClientSecret = (string)nodeSecret.NodeValue;
        }
    }
}
