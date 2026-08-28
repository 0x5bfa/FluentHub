using Windows.Security.Credentials;
using Windows.Storage;

namespace FluentHub.Services
{
	public sealed class GitHubTokenStore
	{
		private const int ElementNotFoundHResult = unchecked((int)0x80070490);
		private const string LegacyAccessTokenSettingName = "AccessToken";
		private const string ResourceName = "FluentHub.GitHub.OAuth";

		private readonly PasswordVault _vault = new();

		public void SaveToken(string login, string accessToken)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

			_vault.Add(new PasswordCredential(ResourceName, login, accessToken));
			RemoveLegacyToken();
		}

		public string? GetToken(string login)
		{
			if (string.IsNullOrWhiteSpace(login))
				return null;

			var accessToken = RetrieveToken(login);
			if (!string.IsNullOrWhiteSpace(accessToken))
			{
				RemoveLegacyToken();
				return accessToken;
			}

			accessToken = GetLegacyToken();
			if (string.IsNullOrWhiteSpace(accessToken))
			{
				RemoveLegacyToken();
				return null;
			}

			// Migrate existing installs away from plaintext local settings.
			SaveToken(login, accessToken);
			return accessToken;
		}

		public void RemoveToken(string login)
		{
			if (string.IsNullOrWhiteSpace(login))
				return;

			try
			{
				_vault.Remove(_vault.Retrieve(ResourceName, login));
			}
			catch (Exception ex) when (ex.HResult == ElementNotFoundHResult)
			{
			}
		}

		private string? RetrieveToken(string login)
		{
			try
			{
				var credential = _vault.Retrieve(ResourceName, login);
				credential.RetrievePassword();
				return credential.Password;
			}
			catch (Exception ex) when (ex.HResult == ElementNotFoundHResult)
			{
				return null;
			}
		}

		private static string? GetLegacyToken()
			=> ApplicationData.Current.LocalSettings.Values.TryGetValue(
				LegacyAccessTokenSettingName,
				out var value)
				? value as string
				: null;

		private static void RemoveLegacyToken()
			=> ApplicationData.Current.LocalSettings.Values.Remove(LegacyAccessTokenSettingName);
	}
}
