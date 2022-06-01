﻿using Windows.System;

namespace FluentHub.Octokit.Authorization
{
    public class AuthorizationService
    {
        private OctokitSecrets Secrets { get; set; }

        public async Task RequestGitHubIdentityAsync(OctokitSecrets secrets)
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

            await Launcher.LaunchUriAsync(oauthLoginUrl);
        }

        public async Task<string> RequestOAuthTokenAsync(string code)
        {
            if (Secrets == null) return null; 

            var request = new OctokitOriginal.OauthTokenRequest(Secrets.ClientId, Secrets.ClientSecret, code);
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
