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
                        Reactions = y.Reactions(6, null, null, null, null, null).Select(reaction => new
                        {
                            reaction.ViewerHasReacted,
                            Reactions = reaction.Nodes.Select(reactionNode => new {
                                reactionNode.Content,
                                ReactedUserName = reactionNode.User.Login,
                            }).ToList(),
                        }).Single(),
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
                comment.IsEdited = item.LastEditedAt == null ? true : false;
                comment.IsMinimized = item.IsMinimized;
                comment.MinimizedReason = item.MinimizedReason;

                List<Reaction> reactions = new();
                foreach(var reaction in item.Reactions.Reactions)
                {
                    Reaction reactionItem = new();
                    reactionItem.Content = reaction.Content;
                    reactionItem.ReactorLogin = reaction.ReactedUserName;

                    reactions.Add(reactionItem);
                }
                comment.Reactions = reactions;

                comment.UpdatedAt = item.UpdatedAt;
                comment.ViewerCanDelete = item.ViewerCanDelete;
                comment.ViewerCanMinimize = item.ViewerCanMinimize;
                comment.ViewerCanReact = item.ViewerCanReact;
                comment.ViewerCanUpdate = item.ViewerCanUpdate;
                comment.ViewerDidAuthor = item.ViewerDidAuthor;
                comment.Url = item.Url;

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

                    Reactions = x.Reactions(6, null, null, null, null, null).Select(reaction => new
                    {
                        reaction.ViewerHasReacted,
                        Reactions = reaction.Nodes.Select(reactionNode => new {
                            reactionNode.Content,
                            ReactedUserName = reactionNode.User.Login,
                        }).ToList(),
                    }).Single(),

                    x.AuthorAssociation,
                    x.BodyHTML,
                    x.LastEditedAt,
                    x.UpdatedAt,
                    x.ViewerCanReact,
                    x.ViewerCanUpdate,
                    x.ViewerDidAuthor,
                    x.Url,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying

            IssueComment comment = new();

            List<Reaction> reactions = new();
            foreach (var reaction in response.Reactions.Reactions)
            {
                Reaction reactionItem = new();
                reactionItem.Content = reaction.Content;
                reactionItem.ReactorLogin = reaction.ReactedUserName;

                reactions.Add(reactionItem);
            }
            comment.Reactions = reactions;

            comment.AuthorAssociation = response.AuthorAssociation;
            comment.AuthorAvatarUrl = response.Author.AvatarUrl;
            comment.AuthorLogin = response.Author.Login;
            comment.BodyHtml = response.BodyHTML;
            comment.IsEdited = response.LastEditedAt == null ? true : false;
            comment.UpdatedAt = response.UpdatedAt;
            comment.ViewerCanReact = response.ViewerCanReact;
            comment.ViewerCanUpdate = response.ViewerCanUpdate;
            comment.ViewerDidAuthor = response.ViewerDidAuthor;
            comment.Url = response.Url;

            #endregion

            return comment;
        }
    }
}
