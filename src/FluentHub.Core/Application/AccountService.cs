namespace FluentHub.Core.Application
{
	public sealed class AccountService
	{
		private readonly IAccountStore _accountStore;

		public AccountService(IAccountStore accountStore)
			=> _accountStore = accountStore ?? throw new ArgumentNullException(nameof(accountStore));

		public void AddAccount(string login)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			var logins = GetLogins();
			logins.RemoveAll(item => string.Equals(item, login, StringComparison.OrdinalIgnoreCase));
			logins.Add(login);
			SaveLogins(logins);
		}

		public void RemoveAccount(string login)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			var logins = GetLogins();
			logins.RemoveAll(item => string.Equals(item, login, StringComparison.OrdinalIgnoreCase));
			SaveLogins(logins);
		}

		public void RemoveAllAccounts()
			=> _accountStore.SignedInUserLogins = string.Empty;

		public bool IsAlreadySignedIn(string login)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			return GetLogins().Contains(login, StringComparer.OrdinalIgnoreCase);
		}

		private List<string> GetLogins()
			=> _accountStore.SignedInUserLogins
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

		private void SaveLogins(IEnumerable<string> logins)
			=> _accountStore.SignedInUserLogins = string.Join(',', logins);
	}
}
