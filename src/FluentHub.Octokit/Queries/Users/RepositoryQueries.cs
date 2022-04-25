using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Queries.Users
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<List<Repository>> GetAllAsync(string login)
        {
            GraphQLCore.Arg<IEnumerable<GraphQLModel.IssueState>> issueState = new(new GraphQLModel.IssueState[] { GraphQLModel.IssueState.Open });
            GraphQLCore.Arg<IEnumerable<GraphQLModel.PullRequestState>> pullRequestState = new(new GraphQLModel.PullRequestState[] { GraphQLModel.PullRequestState.Open });

            #region query
            var query = new Query()
                    .User(login)
                    .Repositories(first: 30)
                    .Nodes
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

            var result = await App.Connection.Run(query);

            #region copying
            List<Repository> items = new();

            foreach (var res in result)
            {
                Repository item = new()
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
