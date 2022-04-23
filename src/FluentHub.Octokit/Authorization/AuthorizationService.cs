using Serilog;
using System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.System;

namespace FluentHub.Octokit.Authorization
{
    public class AuthorizationService
    {
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }

        public async Task<bool> RequestGitHubIdentityAsync()
        {
            try
            {
                await LoadAppCredentialsAsync();

                global::Octokit.OauthLoginRequest request = new(ClientId)
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

                // Success
                Log.Information("RequestGitHubIdentityAsync() completed successfully: [url: {oauthLoginUrl}]", oauthLoginUrl);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("RequestGitHubIdentityAsync(): {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> RequestOAuthTokenAsync(string code)
        {
            try
            {
                await LoadAppCredentialsAsync();

                var request = new global::Octokit.OauthTokenRequest(ClientId, ClientSecret, code);
                var token = await App.Client.Oauth.CreateAccessToken(request);

                if (token != null)
                {
                    App.Client.Credentials = new global::Octokit.Credentials(token.AccessToken);

                    ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                    var accessToken = localSettings.Values["AccessToken"] = token.AccessToken;

                    // Get viewer login name
                    Octokit.Queries.Users.UserQueries queries = new();
                    string login = await queries.GetViewerLogin();

                    var signedInUserName = localSettings.Values["SignedInUserName"] = login;

                    // Success
                    Log.Information("RequestOAuthTokenAsync() completed successfully: [accessToken: {accessToken}](username: {signedInUserName})", accessToken, signedInUserName);
                    return true;
                }
                else
                {
                    throw new ArgumentNullException("AccessToken");
                }
            }
            catch (Exception ex)
            {
                Log.Error("RequestOAuthTokenAsync(): {Message}", ex.Message);
                return false;
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
