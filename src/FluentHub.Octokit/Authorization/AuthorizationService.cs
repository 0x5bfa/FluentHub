// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Net.Http;
using System.Text.Json.Serialization;

namespace FluentHub.Octokit.Authorization
{
	public class AuthorizationService
	{
		private const string DeviceCodeEndpoint = "https://github.com/login/device/code";
		private const string AccessTokenEndpoint = "https://github.com/login/oauth/access_token";
		private const string DeviceCodeGrantType = "urn:ietf:params:oauth:grant-type:device_code";
		private static readonly string[] Scopes =
		[
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
			"admin:gpg_key",
		];

		private static readonly HttpClient HttpClient = new();

		public async Task<DeviceAuthorizationResponse> RequestDeviceAuthorizationAsync(
			OctokitSecrets secrets,
			CancellationToken cancellationToken = default)
		{
			var clientId = secrets.ClientId ?? throw new InvalidOperationException("GitHub OAuth client id is not configured.");

			using FormUrlEncodedContent content = new(new Dictionary<string, string>
			{
				["client_id"] = clientId,
				["scope"] = string.Join(" ", Scopes),
			});

			using HttpRequestMessage request = new(HttpMethod.Post, DeviceCodeEndpoint)
			{
				Content = content,
			};
			request.Headers.Accept.ParseAdd("application/json");

			using HttpResponseMessage response = await HttpClient.SendAsync(request, cancellationToken);
			string json = await response.Content.ReadAsStringAsync(cancellationToken);
			var deviceAuthorization = System.Text.Json.JsonSerializer.Deserialize(
				json,
				GitHubDeviceFlowJsonContext.Default.DeviceAuthorizationResponse);

			if (deviceAuthorization is null)
				throw new InvalidOperationException("GitHub device authorization response was empty.");

			if (!string.IsNullOrEmpty(deviceAuthorization.Error))
				throw CreateDeviceFlowException(deviceAuthorization);

			return deviceAuthorization;
		}

		public async Task<string> RequestDeviceAccessTokenAsync(
			string deviceCode,
			OctokitSecrets secrets,
			CancellationToken cancellationToken = default)
		{
			var clientId = secrets.ClientId ?? throw new InvalidOperationException("GitHub OAuth client id is not configured.");

			using FormUrlEncodedContent content = new(new Dictionary<string, string>
			{
				["client_id"] = clientId,
				["device_code"] = deviceCode,
				["grant_type"] = DeviceCodeGrantType,
			});

			using HttpRequestMessage request = new(HttpMethod.Post, AccessTokenEndpoint)
			{
				Content = content,
			};
			request.Headers.Accept.ParseAdd("application/json");

			using HttpResponseMessage response = await HttpClient.SendAsync(request, cancellationToken);
			string json = await response.Content.ReadAsStringAsync(cancellationToken);
			var token = System.Text.Json.JsonSerializer.Deserialize(
				json,
				GitHubDeviceFlowJsonContext.Default.DeviceAccessTokenResponse);

			if (token is null)
				throw new InvalidOperationException("GitHub access token response was empty.");

			if (!string.IsNullOrEmpty(token.Error))
				throw CreateDeviceFlowException(token);

			if (string.IsNullOrEmpty(token.AccessToken))
				throw new ArgumentNullException("token");

			return token.AccessToken;
		}

		private static Exception CreateDeviceFlowException(DeviceFlowResponse response)
			=> response.Error switch
			{
				"authorization_pending" => new DeviceAuthorizationPendingException(),
				"slow_down" => new DeviceAuthorizationSlowDownException(response.Interval),
				"expired_token" or "token_expired" => new TimeoutException("The GitHub device authorization code has expired."),
				"access_denied" => new UnauthorizedAccessException("GitHub device authorization was cancelled."),
				"device_flow_disabled" => new InvalidOperationException("Device flow is disabled for this OAuth app. Enable it in the GitHub OAuth app settings."),
				_ => new InvalidOperationException(response.ErrorDescription ?? response.Error),
			};
	}

	public class DeviceFlowResponse
	{
		[JsonPropertyName("error")]
		public string Error { get; set; } = string.Empty;

		[JsonPropertyName("error_description")]
		public string ErrorDescription { get; set; } = string.Empty;

		[JsonPropertyName("interval")]
		public int? Interval { get; set; }
	}

	public class DeviceAuthorizationResponse : DeviceFlowResponse
	{
		[JsonPropertyName("device_code")]
		public string DeviceCode { get; set; } = string.Empty;

		[JsonPropertyName("user_code")]
		public string UserCode { get; set; } = string.Empty;

		[JsonPropertyName("verification_uri")]
		public string VerificationUri { get; set; } = string.Empty;

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }

	}

	public class DeviceAccessTokenResponse : DeviceFlowResponse
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonPropertyName("token_type")]
		public string TokenType { get; set; } = string.Empty;

		[JsonPropertyName("scope")]
		public string Scope { get; set; } = string.Empty;
	}

	[JsonSerializable(typeof(DeviceAuthorizationResponse))]
	[JsonSerializable(typeof(DeviceAccessTokenResponse))]
	internal partial class GitHubDeviceFlowJsonContext : JsonSerializerContext
	{
	}

	public class DeviceAuthorizationPendingException : Exception
	{
		public DeviceAuthorizationPendingException()
			: base("GitHub device authorization is still pending.")
		{
		}
	}

	public class DeviceAuthorizationSlowDownException : Exception
	{
		public DeviceAuthorizationSlowDownException(int? interval)
			: base("GitHub requested a slower device authorization polling interval.")
		{
			Interval = interval;
		}

		public int? Interval { get; }
	}
}
