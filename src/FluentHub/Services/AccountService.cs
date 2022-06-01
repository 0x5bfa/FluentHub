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
    public class AccountService
    {
        public void AddAccount(string login)
        {
            // Avoid duplication
            RemoveAccount(login);

            var logins = App.Settings.SignedInUserLogins;
            logins += $",{login}"; // comma-divided

            App.Settings.SignedInUserLogins = logins;
        }

        public void RemoveAccount(string login)
        {
            var loginsDividedWithComma = App.Settings.SignedInUserLogins.Split(",").ToList();
            loginsDividedWithComma.Remove(login);
            var joinedNewValues = string.Join(",", loginsDividedWithComma);

            App.Settings.SignedInUserLogins = joinedNewValues;
        }

        public void RemoveAllAccount()
        {
            // Anticipate using only when deleting all accounts and re-signing in.
            App.Settings.SignedInUserLogins = "";
        }

        public bool IsAlreadySignedIn(string login)
        {
            var loginsDividedWithComma = App.Settings.SignedInUserLogins.Split(",").ToList();
            var isContain = loginsDividedWithComma.Contains(login);

            return isContain;
        }
    }
}
