// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Authorization
{
	public class AuthorizationService
	{
		private static OctokitSecrets Secrets { get; set; }

		public string RequestGitHubIdentityAsync(OctokitSecrets secrets)
		{
			Secrets = secrets;

			OctokitV3.OauthLoginRequest request = new(Secrets.ClientId)
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

		public async Task<string> RequestOAuthTokenAsync(string code, OctokitSecrets secrets)
		{
			Secrets = secrets;

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
