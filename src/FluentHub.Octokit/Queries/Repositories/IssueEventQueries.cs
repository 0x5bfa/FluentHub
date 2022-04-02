using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueEventQueries
    {
        public IssueEventQueries() => new App();

        public async Task<List<Models.Issue>> GetAllAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    Comments = x.TimelineItems(10, null, null, null, null, null, null).Nodes.OfType<IssueComment>().Select(y => new
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
                        //...
                    }).ToList(),
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            //response.Comments[0].Reactions.Reactions[0].Content;
            #endregion

            return null;
        }
    }
}
