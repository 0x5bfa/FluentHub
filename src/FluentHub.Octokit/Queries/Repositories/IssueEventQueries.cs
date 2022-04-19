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
                .Select(node => node.Switch<object>(when => when
                .AddedToProjectEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .AssignedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .ClosedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .CommentDeletedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .ConnectedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .ConvertedNoteToIssueEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .CrossReferencedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .DemilestonedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .DisconnectedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .IssueComment(y => new
                {
                    Actor = y.Author.Select(author => new
                    {
                        AvatarUrl = author.AvatarUrl(100),
                        author.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .LabeledEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .LockedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .MarkedAsDuplicateEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .MentionedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .MilestonedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .MovedColumnsInProjectEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .PinnedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .ReferencedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .RemovedFromProjectEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .RenamedTitleEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .ReopenedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .SubscribedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .TransferredEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnassignedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnlabeledEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnlockedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnmarkedAsDuplicateEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnpinnedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UnsubscribedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                .UserBlockedEvent(y => new
                {
                    Actor = y.Actor.Select(actor => new
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        actor.Login,
                    })
                    .Single(),

                    y.CreatedAt,
                })
                ))
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return null;
        }
    }
}
