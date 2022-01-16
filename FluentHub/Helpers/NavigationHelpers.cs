using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helpers
{
    public class NavigationHelpers
    {
        /// <summary>
        /// Get page type through parsing absolute url.<br/>
        /// This method don't any parse flagments or queries
        /// </summary>
        /// <param name="absoluteUrl">starts with "/" like "/username?tab=repositories"</param>
        public void GetPageType(string absoluteUrl)
        {
            var pathItems = absoluteUrl.Split("/");

            if (pathItems.Count() == 2)
            {
                //switch (pathItems[1])
                //{
                //    case "":
                //        HomeNavViewContent.Navigate(typeof(Activities), $"{App.SignedInUserName}");
                //        break;
                //    case "notifications":
                //        HomeNavViewContent.Navigate(typeof(Notifications), $"{App.SignedInUserName}");
                //        break;
                //    case "issues":
                //        HomeNavViewContent.Navigate(typeof(Issues), $"{App.SignedInUserName}");
                //        break;
                //    case "pulls":
                //        HomeNavViewContent.Navigate(typeof(Pulls), $"{App.SignedInUserName}");
                //        break;
                //    //case "discussions":
                //    //HomeNavViewContent.Navigate(typeof(Discussions), $"{App.SignedInUserName}");
                //    //    break;
                //    case "stars":
                //        HomeNavViewContent.Navigate(typeof(Stars), $"{App.SignedInUserName}");
                //        break;
                //    default:
                //        HomeNavViewContent.Navigate(typeof(ProfilePage), $"{App.SignedInUserName}");
                //        break;
                //}
            }
        }

        public enum PageType
        {
            Unknown,
            UserActivitiesPage,
            UserIssuesPage,
            UserNotificationPage,
            UserProfilePage,
            UserPullsPage,
            UserSettingsPage,
            UserStarsPage,
            RepoCodePage,
            RepoCommitsPage,
            RepoIssuesPage,
            RepoPullsPage,
            RepoSettingsPage,
            FindInAllPage,
            FindInRepoPage,
            FindInUserPage
        }
    }
}
