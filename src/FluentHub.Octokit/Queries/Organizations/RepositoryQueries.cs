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

namespace FluentHub.Octokit.Queries.Organizations
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string org)
        {
            GraphQLCore.Arg<IEnumerable<GraphQLModel.IssueState>> issueState = new(new GraphQLModel.IssueState[] { GraphQLModel.IssueState.Open });
            GraphQLCore.Arg<IEnumerable<GraphQLModel.PullRequestState>> pullRequestState = new(new GraphQLModel.PullRequestState[] { GraphQLModel.PullRequestState.Open });

            #region query
            var query = new Query()
                    .Organization(org)
                    .Repositories(first: 30)
                    .Nodes
                    .Select(x => new Repository
                    {
                        Name = x.Name,
                        Description = x.Description,
                        LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),

                        StargazerCount = x.StargazerCount,
                        ForkCount = x.ForkCount,
                        OpenIssueCount = x.Issues(null, null, null, null, null, null, null, issueState).TotalCount,
                        OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).TotalCount,

                        IsFork = x.IsFork,
                        IsInOrganization = x.IsInOrganization,
                        ViewerHasStarred = x.ViewerHasStarred,

                        UpdatedAt = x.UpdatedAt,
                        UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                        Owner = x.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),

                        PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
                        {
                            Name = y.Name,
                            Color = y.Color,
                        })
                        .SingleOrDefault(),
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
