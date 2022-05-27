using FluentHub.Octokit.Settings;
using Serilog;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.System;

namespace FluentHub.Octokit.Authorization
{
    public class OctokitAuthenticator
    {
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }
        private SettingsModel AppSettings { get; set; }

        public async Task<bool> RequestGitHubIdentityAsync()
        {
            try
            {
                await InitializeAsync();

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

                // Success
                Log.Debug($"RequestGitHubIdentityAsync(): Completed successfully: (Login URL: {oauthLoginUrl})");
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
                await InitializeAsync();

                var request = new OctokitOriginal.OauthTokenRequest(ClientId, ClientSecret, code);
                var token = await App.Client.Oauth.CreateAccessToken(request);

                if (token != null)
                {
                    App.Client.Credentials = new OctokitOriginal.Credentials(token.AccessToken);

                    // Store results to app settings container
                    AppSettings.AccessToken = token.AccessToken;
                    Queries.Users.UserQueries queries = new();
                    AppSettings.SignedInUserName = await queries.GetViewerLogin();

                    // Success
                    Log.Debug($"RequestOAuthTokenAsync(): Completed successfully: (Access Token: {AppSettings.AccessToken}), (Signed in: {AppSettings.SignedInUserName})");
                    return true;
                }
                else
                {
                    throw new ArgumentNullException(nameof(token));
                }
            }
            catch (Exception ex)
            {
                Log.Error("RequestOAuthTokenAsync(): {Message}", ex.Message);
                return false;
            }
        }

        private async Task InitializeAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppCredentials.config"));

            var xmlDoc = await XmlDocument.LoadFromFileAsync(file);

            var nodeId = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='id']/@value");
            var nodeSecret = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='secret']/@value");

            ClientId = (string)nodeId.NodeValue;
            ClientSecret = (string)nodeSecret.NodeValue;

            // Create an instance of SettingsModel
            AppSettings = new();
        }
    }
}
