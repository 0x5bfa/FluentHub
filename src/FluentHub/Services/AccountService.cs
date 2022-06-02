using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services
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

            var logins = App.Settings.SignedInUserLogins;

            if (string.IsNullOrEmpty(logins)) logins += $"{login}"; // comma-divided
            else logins += $",{login}"; // comma-divided

            App.Settings.SignedInUserLogins = logins;
        }

        public static void RemoveAccount(string login)
        {
            var loginsDividedWithComma = App.Settings.SignedInUserLogins.Split(",").ToList();
            loginsDividedWithComma.Remove(login);
            var joinedNewValues = string.Join(",", loginsDividedWithComma);

            App.Settings.SignedInUserLogins = joinedNewValues;
        }

        public static void RemoveAllAccount()
        {
            // Anticipate using only when deleting all accounts and re-signing in.
            App.Settings.SignedInUserLogins = "";
        }

        public static bool IsAlreadySignedIn(string login)
        {
            var loginsDividedWithComma = App.Settings.SignedInUserLogins.Split(",").ToList();
            var isContain = loginsDividedWithComma.Contains(login);

            return isContain;
        }
    }
}
