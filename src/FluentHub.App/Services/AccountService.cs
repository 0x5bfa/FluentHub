namespace FluentHub.App.Services
{
	/// <summary>
	/// This class is used to reflect new data in the app configuration and does not retrieve any GitHub data.
	/// </summary>
	public static class AccountService
	{
		public static void AddAccount(string login)
		{
			// Avoid duplication
			RemoveAccount(login);

			var logins = App.AppSettings.SignedInUserLogins;

			if (string.IsNullOrEmpty(logins)) logins += $"{login}"; // comma-divided
			else logins += $",{login}"; // comma-divided

			App.AppSettings.SignedInUserLogins = logins;
		}

		public static void RemoveAccount(string login)
		{
			var loginsDividedWithComma = App.AppSettings.SignedInUserLogins.Split(",").ToList();
			loginsDividedWithComma.Remove(login);
			var joinedNewValues = string.Join(",", loginsDividedWithComma);

			App.AppSettings.SignedInUserLogins = joinedNewValues;
		}

		public static void RemoveAllAccount()
		{
			// Anticipate using only when deleting all accounts and re-signing in.
			App.AppSettings.SignedInUserLogins = "";
		}

		public static bool IsAlreadySignedIn(string login)
		{
			var loginsDividedWithComma = App.AppSettings.SignedInUserLogins.Split(",").ToList();
			var isContain = loginsDividedWithComma.Contains(login);

			return isContain;
		}
	}
}
