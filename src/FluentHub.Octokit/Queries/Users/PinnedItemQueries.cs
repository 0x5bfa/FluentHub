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
    public class PinnedItemQueries
    {
        public PinnedItemQueries() => new App();

        public async Task<List<Repository>> GetAllAsync(string login)
        {
            GraphQLCore.Arg<IEnumerable<GraphQLModel.IssueState>> issueState = new(new GraphQLModel.IssueState[] { GraphQLModel.IssueState.Open });
            GraphQLCore.Arg<IEnumerable<GraphQLModel.PullRequestState>> pullRequestState = new(new GraphQLModel.PullRequestState[] { GraphQLModel.PullRequestState.Open });

            #region query
            var query = new Query()
                    .User(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .OfType<GraphQLModel.Repository>()
                    .Select(x => new Repository
                    {
                        Name = x.Name,
                        Description = x.Description,

                        StargazerCount = x.StargazerCount,
                        ViewerHasStarred = x.ViewerHasStarred,

                        PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
                        {
                            Name = y.Name,
                            Color = y.Color,
                        })
                        .SingleOrDefault(),

                        Owner = x.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
