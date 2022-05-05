using FluentHub.Octokit.Models;
using FluentHub.Octokit.Models.Events;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class PullRequestQueries
    {
        public PullRequestQueries() => new App();

        public async Task<List<PullRequest>> GetAllAsync(string name, string owner)
        {
            GraphQLModel.IssueOrder order = new() { Direction = GraphQLModel.OrderDirection.Desc, Field = GraphQLModel.IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new PullRequest
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    Name = x.Repository.Name,
                    Title = x.Title,

                    BaseRefName = x.BaseRefName,
                    HeadRefName = x.HeadRefName,
                    HeadRefOwnerLogin = x.HeadRepositoryOwner.Login,

                    Closed = x.Closed,
                    Merged = x.Merged,
                    IsDraft = x.IsDraft,

                    Number = x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                    {
                        Color = y.Color,
                        Description = y.Description,
                        Name = y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => y.State)
                    .ToList().FirstOrDefault(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => y.Commit.StatusCheckRollup.Select(z => new StatusCheckRollup
                    {
                        Status = z.State,
                    })
                    .SingleOrDefault())
                    .ToList().FirstOrDefault(),

                    UpdatedAt = x.UpdatedAt,
                })
                .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            return res.ToList();
        }

        public async Task<PullRequest> GetAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Select(x => new PullRequest
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    Name = x.Repository.Name,
                    Title = x.Title,

                    BaseRefName = x.BaseRefName,
                    HeadRefName = x.HeadRefName,
                    HeadRefOwnerLogin = x.HeadRepositoryOwner.Login,

                    Closed = x.Closed,
                    Merged = x.Merged,
                    IsDraft = x.IsDraft,

                    Number = x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                    {
                        Color = y.Color,
                        Description = y.Description,
                        Name = y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => y.State)
                    .ToList().FirstOrDefault(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => y.Commit.StatusCheckRollup.Select(z => new StatusCheckRollup
                    {
                        Status = z.State,
                    })
                    .SingleOrDefault())
                    .ToList().FirstOrDefault(),

                    UpdatedAt = x.UpdatedAt,
                })
                .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            return res;
        }

        public async Task<IssueComment> GetBodyAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Select(x => new IssueComment
                {
                    Author = x.Author.Select(author => new Actor
                    {
                        Login = author.Login,
                        AvatarUrl = author.AvatarUrl(100),
                    })
                    .Single(),

                    Reactions = x.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new Reaction
                    {
                        Content = reaction.Content,
                        ReactorLogin = reaction.User.Login,
                    })
                    .ToList(),

                    AuthorAssociation = x.AuthorAssociation,
                    BodyHTML = x.BodyHTML,
                    LastEditedAt = x.LastEditedAt,
                    UpdatedAt = x.UpdatedAt,
                    ViewerCanReact = x.ViewerCanReact,
                    ViewerCanUpdate = x.ViewerCanUpdate,
                    ViewerDidAuthor = x.ViewerDidAuthor,
                    Url = x.Url,
                    CreatedAt = x.CreatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
