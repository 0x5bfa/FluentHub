using FluentHub.OctokitEx.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries.User
{
    public class UserQueries
    {
        public UserQueries() => new App();

        public async Task<UserOverviewItem> GetOverview(string login)
        {
            try
            {
                #region queries
                var query = new Query()
                    .User(login)
                    .Select(x => new
                    {
                        AvatarUrl = x.AvatarUrl(100),
                        x.Bio,
                        x.Company,
                        x.Email,
                        x.IsCampusExpert,
                        x.IsBountyHunter,
                        x.IsDeveloperProgramMember,
                        x.IsEmployee,
                        x.IsGitHubStar,
                        x.IsViewer,
                        x.Location,
                        x.Login,
                        x.Name,
                        x.TwitterUsername,
                        x.ViewerIsFollowing,
                        x.WebsiteUrl,

                        FollowersTotalCount = x.Followers(null, null, null, null).TotalCount,
                        FollowingTotalCount = x.Following(null, null, null, null).TotalCount,
                    })
                    .Compile();
                #endregion

                var response = await App.Connection.Run(query);

                UserOverviewItem item = new();

                #region copying
                item.AvatarUrl = response.AvatarUrl;
                item.Bio = response.Bio;
                item.Company = response.Company;
                item.Email = response.Email;
                item.IsBountyHunter = response.IsBountyHunter;
                item.IsCampusExpert = response.IsCampusExpert;
                item.IsDeveloperProgramMember = response.IsDeveloperProgramMember;
                item.IsEmployee = response.IsGitHubStar;
                item.IsGitHubStar = response.IsGitHubStar;
                item.IsViewer = response.IsViewer;
                item.Location = response.Location;
                item.Login = response.Login;
                item.Name = response.Name;
                item.TwitterUsername = response.TwitterUsername;
                item.ViewerIsFollowing = response.ViewerIsFollowing;
                item.WebsiteUrl = response.WebsiteUrl;

                item.FollowersTotalCount = response.FollowersTotalCount;
                item.FollowingTotalCount = response.FollowingTotalCount;
                #endregion

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
