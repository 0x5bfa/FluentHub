using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Organizations
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string org)
        {
            #region query
            var query = new Query()
                    .Organization(org)
                    .Repositories(first: 30)
                    .Nodes
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

            var result = await App.Connection.Run(query);

            #region copying
            List<Models.Repository> items = new();

            foreach (var res in result)
            {
                Models.Repository item = new();

                item.Name = res.Name;
                item.Owner = res.OwnerLoginName;
                item.OwnerAvatarUrl = res.OwnerAvatarUrl;
                item.Description = res.Description;
                item.StargazerCount = res.StargazerCount;

                item.PrimaryLangName = res.PrimaryLanguage?.Name;
                item.PrimaryLangColor = res.PrimaryLanguage?.Color;

                item.LicenseName = res.LicenseName;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.PullCount = res.PullCount;
                item.UpdatedAt = res.UpdatedAt;
                item.WatcherCount = res.WatcherCount;
                item.ViewerHasStarred = res.ViewerHasStarred;
                item.DefaultBranchName = res.DefaultBranchName;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
