using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphqlmodel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueEventQueries
    {
        public IssueEventQueries() => new App();

        public async Task<List<IssueComment>> GetAllAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    #region comments
                    Comments = x.TimelineItems(100, null, null, null, null, null, null).Nodes.OfType<graphqlmodel.IssueComment>().Select(y => new
                    {
                        Author = y.Author.Select(author => new
                        {
                            author.Login,
                            AvatarUrl = author.AvatarUrl(100),
                        }).Single(),
                        y.AuthorAssociation,
                        y.BodyHTML,
                        Reactions = y.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new
                        {
                            Reactions = reaction.Select(reactionNode => new {
                                reactionNode.Content,
                                ReactedUserName = reactionNode.User.Login,
                            }).Single(),
                        }).ToList(),
                        y.LastEditedAt,
                        y.MinimizedReason,
                        y.IsMinimized,
                        y.UpdatedAt,
                        y.ViewerCanDelete,
                        y.ViewerCanMinimize,
                        y.ViewerCanReact,
                        y.ViewerCanUpdate,
                        y.ViewerDidAuthor,
                        y.Url,
                        y.CreatedAt,
                    }).ToList(),
                    #endregion
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            // Now only available issue comment events
            #region copying
            List<IssueComment> issueComments = new();

            foreach(var item in response.Comments)
            {
                IssueComment comment = new();

                comment.AuthorAssociation = item.AuthorAssociation;
                comment.AuthorAvatarUrl = item.Author.AvatarUrl;
                comment.AuthorLogin = item.Author.Login;
                comment.BodyHtml = item.BodyHTML;
                comment.IsEdited = item.LastEditedAt == null ? false : true;
                comment.IsMinimized = item.IsMinimized;
                comment.MinimizedReason = item.MinimizedReason;

                comment.Reactions = new();
                foreach(var reaction in item.Reactions)
                {
                    switch (reaction.Reactions.Content)
                    {
                        case graphqlmodel.ReactionContent.ThumbsUp:
                            comment.Reactions.ThumbsUpCount++;
                            comment.Reactions.ThumbsUpActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactThumbsUp = true;
                            break;
                        case graphqlmodel.ReactionContent.ThumbsDown:
                            comment.Reactions.ThumbsDownCount++;
                            comment.Reactions.ThumbsDownActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactThumbsDown = true;
                            break;
                        case graphqlmodel.ReactionContent.Laugh:
                            comment.Reactions.LaughCount++;
                            comment.Reactions.LaughActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactLaugh = true;
                            break;
                        case graphqlmodel.ReactionContent.Hooray:
                            comment.Reactions.HoorayCount++;
                            comment.Reactions.HoorayActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactHooray = true;
                            break;
                        case graphqlmodel.ReactionContent.Confused:
                            comment.Reactions.ConfusedCount++;
                            comment.Reactions.ConfusedActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactConfused = true;
                            break;
                        case graphqlmodel.ReactionContent.Heart:
                            comment.Reactions.HeartCount++;
                            comment.Reactions.HeartActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactHeart = true;
                            break;
                        case graphqlmodel.ReactionContent.Rocket:
                            comment.Reactions.RocketCount++;
                            comment.Reactions.RocketActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactRocket = true;
                            break;
                        case graphqlmodel.ReactionContent.Eyes:
                            comment.Reactions.EyesCount++;
                            comment.Reactions.EyesActors.Add(reaction.Reactions.ReactedUserName);
                            if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                                comment.Reactions.ViewerReactEyes = true;
                            break;
                    }
                }

                comment.UpdatedAt = item.UpdatedAt;
                comment.ViewerCanDelete = item.ViewerCanDelete;
                comment.ViewerCanMinimize = item.ViewerCanMinimize;
                comment.ViewerCanReact = item.ViewerCanReact;
                comment.ViewerCanUpdate = item.ViewerCanUpdate;
                comment.ViewerDidAuthor = item.ViewerDidAuthor;
                comment.Url = item.Url;
                comment.CreatedAt = item.CreatedAt;

                issueComments.Add(comment);
            }

            #endregion

            return issueComments;
        }

        public async Task<IssueComment> GetBodyAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
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

            IssueComment comment = new();

            comment.Reactions = new();
            foreach (var reaction in response.Reactions)
            {
                switch (reaction.Reactions.Content)
                {
                    case graphqlmodel.ReactionContent.ThumbsUp:
                        comment.Reactions.ThumbsUpCount++;
                        comment.Reactions.ThumbsUpActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactThumbsUp = true;
                        break;
                    case graphqlmodel.ReactionContent.ThumbsDown:
                        comment.Reactions.ThumbsDownCount++;
                        comment.Reactions.ThumbsDownActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactThumbsDown = true;
                        break;
                    case graphqlmodel.ReactionContent.Laugh:
                        comment.Reactions.LaughCount++;
                        comment.Reactions.LaughActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactLaugh = true;
                        break;
                    case graphqlmodel.ReactionContent.Hooray:
                        comment.Reactions.HoorayCount++;
                        comment.Reactions.HoorayActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactHooray = true;
                        break;
                    case graphqlmodel.ReactionContent.Confused:
                        comment.Reactions.ConfusedCount++;
                        comment.Reactions.ConfusedActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactConfused = true;
                        break;
                    case graphqlmodel.ReactionContent.Heart:
                        comment.Reactions.HeartCount++;
                        comment.Reactions.HeartActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactHeart = true;
                        break;
                    case graphqlmodel.ReactionContent.Rocket:
                        comment.Reactions.RocketCount++;
                        comment.Reactions.RocketActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactRocket = true;
                        break;
                    case graphqlmodel.ReactionContent.Eyes:
                        comment.Reactions.EyesCount++;
                        comment.Reactions.EyesActors.Add(reaction.Reactions.ReactedUserName);
                        if (reaction.Reactions.ReactedUserName == App.SignedInUserName)
                            comment.Reactions.ViewerReactEyes = true;
                        break;
                }
            }

            comment.AuthorAssociation = response.AuthorAssociation;
            comment.AuthorAvatarUrl = response.Author.AvatarUrl;
            comment.AuthorLogin = response.Author.Login;
            comment.BodyHtml = response.BodyHTML;
            comment.IsEdited = response.LastEditedAt == null ? false : true;
            comment.UpdatedAt = response.UpdatedAt;
            comment.ViewerCanReact = response.ViewerCanReact;
            comment.ViewerCanUpdate = response.ViewerCanUpdate;
            comment.ViewerDidAuthor = response.ViewerDidAuthor;
            comment.Url = response.Url;
            comment.CreatedAt = response.CreatedAt;

            #endregion

            return comment;
        }
    }
}
