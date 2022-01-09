using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helper
{
    public class GitHubUrlParser
    {
        public bool VerifyUrl(string url)
        {
            var item = url.Split("/").ToList();

            // For example: https://github.com/onein528 is correct
            if ((item[0] == "https:" || item[0] == "http:") && item[1] == "" && item[2] == "github.com")
            {
                // Remove domain
                item.RemoveRange(0, 3);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string WhereShouldINavigateTo(string url)
        {
            if (VerifyUrl(url) != true)
            {
                return null;
            }

            var item = url.Split("/").ToList();

            // Remove domain
            item.RemoveRange(0, 3);

            // authed user home page
            if (item[0] == App.AuthedUserName && item.Count() == 1)
            {
                return "AuthedUserHomePage";
            }
            // user profile page
            else if (item.Count() == 1)
            {
                return "UserHomePage";
            }
            else
            {
                return "AuthedUserHomePage";
            }
        }
    }
}
