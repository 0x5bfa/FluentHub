using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<Models.Repository> Get(string name, string owner)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    OwnerAvatarUrl = x.Owner.AvatarUrl(100),
                    OwnerLoginName = x.Owner.Login,

                    PrimaryLanguage = x.PrimaryLanguage.Select(y => new
                    {
                        y.Name,
                        y.Color,
                    }).SingleOrDefault(),

                    x.StargazerCount,
                    x.ForkCount,
                    IssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    OpenIssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    PullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                    OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                    WatcherCount = x.Watchers(null, null, null, null).TotalCount,

                    LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),
                    x.ViewerHasStarred,
                    DefaultBranchName = x.DefaultBranchRef.Name,
                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            Models.Repository item = new();

            item.Name = response.Name;
            item.Owner = response.OwnerLoginName;
            item.OwnerAvatarUrl = response.OwnerAvatarUrl;
            item.Description = response.Description;
            item.StargazerCount = response.StargazerCount;

            item.PrimaryLangName = response.PrimaryLanguage?.Name;
            item.PrimaryLangColor = response.PrimaryLanguage?.Color;

            item.LicenseName = response.LicenseName;
            item.ForkCount = response.ForkCount;
            item.IssueCount = response.IssueCount;
            item.PullCount = response.PullCount;
            item.UpdatedAt = response.UpdatedAt;
            item.WatcherCount = response.WatcherCount;
            item.ViewerHasStarred = response.ViewerHasStarred;
            item.DefaultBranchName = response.DefaultBranchName;
            #endregion

            return item;
        }

        // Details such as releases, contributors
        public void GetDetails()
        {
        }
    }
}
