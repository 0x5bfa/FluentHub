using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class UserQueries
    {
        public UserQueries() => new App();

        public async Task<User> GetAsync(string login)
        {
            #region queries
            var query = new Query()
                .User(login)
                .Select(x => new User
                {
                    AvatarUrl = x.AvatarUrl(100),
                    Bio = x.Bio,
                    Company = x.Company,
                    Email = x.Email,
                    IsCampusExpert = x.IsCampusExpert,
                    IsBountyHunter = x.IsBountyHunter,
                    IsDeveloperProgramMember = x.IsDeveloperProgramMember,
                    IsEmployee = x.IsEmployee,
                    IsGitHubStar = x.IsGitHubStar,
                    IsViewer = x.IsViewer,
                    Location = x.Location,
                    Login = x.Login,
                    Name = x.Name,
                    TwitterUsername = x.TwitterUsername,
                    ViewerIsFollowing = x.ViewerIsFollowing,
                    WebsiteUrl = x.WebsiteUrl,

                    FollowersTotalCount = x.Followers(null, null, null, null).TotalCount,
                    FollowingTotalCount = x.Following(null, null, null, null).TotalCount,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response;
        }

        public async Task<string> GetViewerLogin()
        {
            #region query
            var query = new Query()
                .Viewer
                .Select(x => new
                {
                    x.Login,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.Login;
        }
    }
}
