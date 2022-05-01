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

namespace FluentHub.Octokit.Queries.Repositories
{
    public class DiscussionQueries
    {
        public async Task<List<Discussion>> GetAllAsync(string owner, string name)
        {
            var query = new Query()
                .Repository(owner: owner, name: name)
                .Discussions(first: 30)
                .Nodes
                .Select(x => new Discussion
                {
                    AnswerChosenAt = x.AnswerChosenAt,

                    Category = x.Category.Select(category => new DiscussionCategory
                    {
                        Emoji = category.Emoji,
                        Id = category.Id.ToString(),
                    })
                    .Single(),

                    Id = x.Id.ToString(),
                    Locked = x.Locked,
                    Number = x.Number,

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

                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,
                    UpvoteCount = x.UpvoteCount,
                    Url = x.Url,
                    ViewerCanDelete = x.ViewerCanDelete,
                    ViewerDidAuthor = x.ViewerDidAuthor,
                    ViewerHasUpvoted = x.ViewerHasUpvoted,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<Discussion> GetAllAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(owner: owner, name: name)
                .Discussion(number)
                .Select(x => new Discussion
                {
                    ActiveLockReason = x.ActiveLockReason,
                    AnswerChosenAt = x.AnswerChosenAt,
                    AuthorAssociation = x.AuthorAssociation,
                    BodyHTML = x.BodyHTML,

                    Category = x.Category.Select(category => new DiscussionCategory
                    {
                        CreatedAt = category.CreatedAt,
                        Description = category.Description,
                        Emoji = category.Emoji,
                        Id = category.Id.ToString(),
                        Name = category.Name,
                        UpdatedAt = category.UpdatedAt,
                    })
                    .Single(),

                    CreatedAt = x.CreatedAt,
                    Id = x.Id.ToString(),
                    IncludesCreatedEdit = x.IncludesCreatedEdit,
                    LastEditedAt = x.LastEditedAt,
                    Locked = x.Locked,
                    Number = x.Number,

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

                    PublishedAt = x.PublishedAt,
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,
                    UpvoteCount = x.UpvoteCount,
                    Url = x.Url,
                    ViewerCanDelete = x.ViewerCanDelete,
                    ViewerCanReact = x.ViewerCanReact,
                    ViewerCanSubscribe = x.ViewerCanSubscribe,
                    ViewerCanUpdate = x.ViewerCanUpdate,
                    ViewerCanUpvote = x.ViewerCanUpvote,
                    ViewerDidAuthor = x.ViewerDidAuthor,
                    ViewerHasUpvoted = x.ViewerHasUpvoted,
                    ViewerSubscription = x.ViewerSubscription,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
