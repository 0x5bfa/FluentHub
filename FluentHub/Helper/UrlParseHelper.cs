using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helper
{
    public class UrlParseHelper
    {
        private bool VerifyUrl(string url)
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

        public PageType WhereShouldINavigateTo(string url)
        {
            if (VerifyUrl(url) != true)
            {
                return PageType.None;
            }

            var item = url.Split("/").ToList();

            // Remove domain
            item.RemoveRange(0, 3);


            if (item.Count() == 1)
            {
                // Delete all queries
                item[0] = item[0].Split("?")[0];

                switch (item[0])
                {
                    case "search":
                        return PageType.SearchInAll;

                    case "issues":
                        return PageType.AuthedUserIssues;

                    case "pulls":
                        return PageType.AuthedUserPRs;

                    case "discussions":
                        return PageType.AuthedUserDiscussions;

                    case "explore":
                        return PageType.Explorer;

                    default:
                        return PageType.UserProfile;
                }
            }
            else
            {
                return PageType.None; // coming soon
            }
        }

        public enum PageType
        {
            None,
            UserProfile,
            AuthedUserIssues,
            AuthedUserPRs,
            AuthedUserDiscussions,
            Explorer,
            Repository,
            SearchInAll
        }
    }
}
