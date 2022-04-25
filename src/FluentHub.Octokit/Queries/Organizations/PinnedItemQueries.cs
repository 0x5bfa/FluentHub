using Humanizer;
using global::Octokit.GraphQL.Core;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Organizations
{
    public class PinnedItemQueries
    {
        public PinnedItemQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string org)
        {
            Arg<IEnumerable<IssueState>> issueState = new(new IssueState[] { IssueState.Open });
            Arg<IEnumerable<PullRequestState>> pullRequestState = new(new PullRequestState[] { PullRequestState.Open });

            #region query
            var usersQuery = new Query()
                    .Organization(org)
                    .PinnedItems(first: 6)
                    .Nodes
                    .OfType<Repository>()
                    .Select(x => new
                    {
                        x.Name,
                        OwnerAvatarUrl = x.Owner.AvatarUrl(100),
                        OwnerLoginName = x.Owner.Login,
                        x.Description,
                        LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),

                        PrimaryLanguage = x.PrimaryLanguage.Select(y => new
                        {
                            y.Name,
                            y.Color,
                        })
                        .SingleOrDefault(),

                        x.StargazerCount,
                        x.ForkCount,
                        OpenIssueCount = x.Issues(null, null, null, null, null, null, null, issueState).TotalCount,
                        OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).TotalCount,

                        x.IsFork,
                        x.IsInOrganization,
                        x.ViewerHasStarred,

                        x.UpdatedAt,
                    })
                    .Compile();
            #endregion

            var result = await App.Connection.Run(usersQuery);

            #region copying
            List<Models.Repository> items = new();

            foreach (var res in result)
            {
                Models.Repository item = new()
                {
                    Name = res.Name,
                    Owner = res.OwnerLoginName,
                    OwnerAvatarUrl = res.OwnerAvatarUrl,
                    OwnerIsOrganization = res.IsInOrganization,
                    Description = res.Description,

                    PrimaryLangName = res.PrimaryLanguage?.Name,
                    PrimaryLangColor = res.PrimaryLanguage?.Color,
                    LicenseName = res.LicenseName,

                    StargazerCount = res.StargazerCount,
                    ForkCount = res.ForkCount,
                    OpenIssueCount = res.OpenIssueCount,
                    OpenPullCount = res.OpenPullCount,

                    IsFork = res.IsFork,
                    ViewerHasStarred = res.ViewerHasStarred,

                    UpdatedAt = res.UpdatedAt,
                    UpdatedAtHumanized = res.UpdatedAt.Humanize(),
                };

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
