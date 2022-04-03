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

                        LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),
                        DefaultBranchName = x.DefaultBranchRef.Name,
                        x.HomepageUrl,

                        x.StargazerCount,
                        x.ForkCount,
                        IssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                        OpenIssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                        PullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                        OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                        WatcherCount = x.Watchers(null, null, null, null).TotalCount,
                        HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, null).TotalCount,
                        ReleaseCount = x.Releases(null, null, null, null, null).TotalCount,

                        x.ForkingAllowed,
                        x.HasIssuesEnabled,
                        x.HasProjectsEnabled,
                        x.IsArchived,
                        x.IsFork,
                        x.IsInOrganization,
                        x.IsPrivate,
                        x.IsTemplate,
                        x.ViewerHasStarred,

                        x.ViewerSubscription,

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

                item.PrimaryLangName = res.PrimaryLanguage?.Name;
                item.PrimaryLangColor = res.PrimaryLanguage?.Color;
                item.LicenseName = res.LicenseName;
                item.DefaultBranchName = res.DefaultBranchName;
                item.HomepageUrl = res.HomepageUrl;

                item.StargazerCount = res.StargazerCount;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.OpenIssueCount = res.OpenIssueCount;
                item.PullCount = res.PullCount;
                item.OpenPullCount = res.OpenPullCount;
                item.WatcherCount = res.WatcherCount;
                item.HeadRefsCount = res.HeadRefsCount;
                item.ReleaseCount = res.ReleaseCount;

                item.ViewerHasStarred = res.ViewerHasStarred;
                item.HasIssuesEnabled = res.HasIssuesEnabled;
                item.HasProjectsEnabled = res.HasProjectsEnabled;
                item.IsArchived = res.IsArchived;
                item.IsFork = res.IsFork;
                item.IsInOrganization = res.IsInOrganization;
                item.IsPrivate = res.IsPrivate;
                item.IsTemplate = res.IsTemplate;

                item.ViewerSubscription = res.ViewerSubscription;

                item.UpdatedAt = res.UpdatedAt;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
