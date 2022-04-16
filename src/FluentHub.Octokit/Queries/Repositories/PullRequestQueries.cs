using FluentHub.Octokit.Models.Events;
using Humanizer;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
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

        public async Task<List<Models.PullRequest>> GetAllAsync(string name, string owner)
        {
            IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    x.Repository.Name,
                    x.Title,

                    x.Closed,
                    x.Merged,
                    x.IsDraft,

                    x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                    {
                        y.Color,
                        y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => new
                    {
                        y.State,
                    })
                    .ToList(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => new
                    {
                        State = y.Commit.StatusCheckRollup.Select(z => new
                        {
                            z.State,
                        })
                        .SingleOrDefault(),
                    })
                    .ToList(),

                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            List<Models.PullRequest> items = new();

            foreach (var res in response)
            {
                Models.PullRequest item = new()
                {
                    Name = res.Name,
                    OwnerAvatarUrl = res.OwnerAvatarUrl,
                    OwnerLogin = res.OwnerLogin,
                    Title = res.Title,

                    Number = res.Number,
                    CommentCount = res.CommentCount,

                    IsClosed = res.Closed,
                    IsMerged = res.Merged,
                    IsDraft = res.IsDraft,

                    UpdatedAt = res.UpdatedAt,
                    UpdatedAtHumanized = res.UpdatedAt.Humanize(),
                };

                if (res.Labels.Count() != 0)
                {
                    foreach (var label in res.Labels)
                    {
                        Models.Label labels = new()
                        {
                            Color = label.Color,
                            Name = label.Name,
                            ColorBrush = Helpers.ColorHelper.HexCodeToSolidColorBrush(label.Color),
                        };

                        item.Labels.Add(labels);
                    }
                }

                if (res.ReviewState.Count() != 0)
                    item.ReviewState = res.ReviewState[0].State.ToString();

                if (res.StatusState.Count() != 0 && res.StatusState[0].State != null)
                    item.StatusState = res.StatusState[0].State.State.ToString();

                items.Add(item);
            }
            #endregion

            return items;
        }

        public async Task<Models.PullRequest> GetAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Select(x => new
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    x.Repository.Name,
                    x.Title,

                    x.Closed,
                    x.Merged,
                    x.IsDraft,

                    x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                    {
                        y.Color,
                        y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => new
                    {
                        y.State,
                    })
                    .ToList(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => new
                    {
                        State = y.Commit.StatusCheckRollup.Select(z => new
                        {
                            z.State,
                        })
                        .SingleOrDefault(),
                    })
                    .ToList(),

                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            #region copying
            Models.PullRequest item = new()
            {
                Name = res.Name,
                OwnerAvatarUrl = res.OwnerAvatarUrl,
                OwnerLogin = res.OwnerLogin,
                Title = res.Title,

                Number = res.Number,
                CommentCount = res.CommentCount,

                IsClosed = res.Closed,
                IsMerged = res.Merged,
                IsDraft = res.IsDraft,

                UpdatedAt = res.UpdatedAt,
                UpdatedAtHumanized = res.UpdatedAt.Humanize(),
            };

            if (res.Labels.Count() != 0)
            {
                foreach (var label in res.Labels)
                {
                    Models.Label labels = new()
                    {
                        Color = label.Color,
                        Name = label.Name,
                        ColorBrush = Helpers.ColorHelper.HexCodeToSolidColorBrush(label.Color),
                    };

                    item.Labels.Add(labels);
                }
            }

            if (res.ReviewState.Count() != 0)
                item.ReviewState = res.ReviewState[0].State.ToString();

            if (res.StatusState.Count() != 0 && res.StatusState[0].State != null)
                item.StatusState = res.StatusState[0].State.State.ToString();
            #endregion

            return item;
        }

        public async Task<CommentedEvent> GetBodyAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Select(x => new
                {
                    Author = x.Author.Select(author => new
                    {
                        author.Login,
                        AvatarUrl = author.AvatarUrl(100),
                    }).Single(),

                    Reactions = x.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new
                    {
                        Reactions = reaction.Select(reactionNode => new {
                            reactionNode.Content,
                            ReactedUserName = reactionNode.User.Login,
                        }).Single(),
                    }).ToList(),

                    x.AuthorAssociation,
                    x.BodyHTML,
                    x.LastEditedAt,
                    x.UpdatedAt,
                    x.ViewerCanReact,
                    x.ViewerCanUpdate,
                    x.ViewerDidAuthor,
                    x.Url,
                    x.CreatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying

            CommentedEvent comment = new()
            {
                AuthorAssociation = response.AuthorAssociation,
                AuthorAvatarUrl = response.Author.AvatarUrl,
                AuthorLogin = response.Author.Login,
                BodyHtml = response.BodyHTML,
                IsEdited = response.LastEditedAt == null ? false : true,
                UpdatedAt = response.UpdatedAt,
                ViewerCanReact = response.ViewerCanReact,
                ViewerCanUpdate = response.ViewerCanUpdate,
                ViewerDidAuthor = response.ViewerDidAuthor,
                Url = response.Url,
                CreatedAt = response.CreatedAt,
            };

            foreach (var reaction in response.Reactions)
            {
                switch (reaction.Reactions.Content)
                {
                    case GraphQLModel.ReactionContent.ThumbsUp:
                        comment.Reactions.ThumbsUpCount++;
                        comment.Reactions.ThumbsUpActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactThumbsUp = true;
                        break;
                    case GraphQLModel.ReactionContent.ThumbsDown:
                        comment.Reactions.ThumbsDownCount++;
                        comment.Reactions.ThumbsDownActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactThumbsDown = true;
                        break;
                    case GraphQLModel.ReactionContent.Laugh:
                        comment.Reactions.LaughCount++;
                        comment.Reactions.LaughActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactLaugh = true;
                        break;
                    case GraphQLModel.ReactionContent.Hooray:
                        comment.Reactions.HoorayCount++;
                        comment.Reactions.HoorayActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactHooray = true;
                        break;
                    case GraphQLModel.ReactionContent.Confused:
                        comment.Reactions.ConfusedCount++;
                        comment.Reactions.ConfusedActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactConfused = true;
                        break;
                    case GraphQLModel.ReactionContent.Heart:
                        comment.Reactions.HeartCount++;
                        comment.Reactions.HeartActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactHeart = true;
                        break;
                    case GraphQLModel.ReactionContent.Rocket:
                        comment.Reactions.RocketCount++;
                        comment.Reactions.RocketActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactRocket = true;
                        break;
                    case GraphQLModel.ReactionContent.Eyes:
                        comment.Reactions.EyesCount++;
                        comment.Reactions.EyesActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactEyes = true;
                        break;
                }
            }

            #endregion

            return comment;
        }
    }
}
