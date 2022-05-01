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
    public class DiscussionQueries
    {
        public DiscussionQueries() => new App();

        public async Task<List<Discussion>> GetAllAsync(string login)
        {
            var query = new Query()
                .User(login)
                .RepositoryDiscussions(first: 30)
                .Nodes
                .Select(x => new Discussion
                {
                    Category = x.Category.Select(category => new DiscussionCategory
                    {
                        Emoji = category.Emoji,
                        Id = category.Id.ToString(),
                    })
                    .Single(),

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),

                    Id = x.Id.ToString(),
                    Locked = x.Locked,
                    Number = x.Number,
                    Title = x.Title,
                    UpvoteCount = x.UpvoteCount,
                    Url = x.Url,
                    ViewerCanDelete = x.ViewerCanDelete,
                    ViewerDidAuthor = x.ViewerDidAuthor,
                    ViewerHasUpvoted = x.ViewerHasUpvoted,

                    AnswerChosenAt = x.AnswerChosenAt,
                    UpdatedAt = x.UpdatedAt,
                    AnswerChosenAtHumanized = x.AnswerChosenAt.Humanize(null, null),
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
