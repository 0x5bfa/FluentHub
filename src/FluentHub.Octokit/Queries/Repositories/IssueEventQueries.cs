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
    public class IssueEventQueries
    {
        public IssueEventQueries() => new App();

        public async Task<List<Tuple<string, object>>> GetAllAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .TimelineItems(null, null, 10, null, null, null, null)
                .Nodes
                .Select(node => node.Switch<object>(when =>
                    when.AddedToProjectEvent(addedtoprojectevent => new
                    {
                        Actor = addedtoprojectevent.Actor.Select(actor => new
                        {
                            AvatarUrl = actor.AvatarUrl(100),
                            actor.Login,
                        })
                        .Single(),

                        addedtoprojectevent.CreatedAt,
                    }))
                )
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return null;
        }
    }
}
