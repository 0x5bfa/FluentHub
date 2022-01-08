using FluentHub.ViewModels;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.System;

namespace FluentHub.Services.Auth
{
    public class RequestAuthorization : SettingsManager
    {
        static private string clientId = "";
        static private string clientSecret = "";

        public async Task<bool> RequestGitHubIdentity()
        {
            try
            {
                AppCredentials appCredentials = new AppCredentials();
                await appCredentials.GetAppCredentials();
                clientId = appCredentials.clientId;
                clientSecret = appCredentials.clientSecret;

                OauthLoginRequest request = new OauthLoginRequest(clientId)
                {
                    Scopes = { "user", "repo" },
                };

                Uri oauthLoginUrl = App.Client.Oauth.GetGitHubLoginUrl(request);

                await Launcher.LaunchUriAsync(oauthLoginUrl);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RequestOAuthToken(string code)
        {
            try
            {
                var request = new OauthTokenRequest(clientId, clientSecret, code);
                var token = await App.Client.Oauth.CreateAccessToken(request);

                if (token != null)
                {
                    App.Client.Credentials = new Credentials(token.AccessToken);

                    // store token (in the future, this token will be stored credential locker)
                    // https://docs.microsoft.com/en-us/windows/uwp/security/credential-locker
                    Set("accessToken", token.AccessToken);
                    App.settings.SetupCompleted = true;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
