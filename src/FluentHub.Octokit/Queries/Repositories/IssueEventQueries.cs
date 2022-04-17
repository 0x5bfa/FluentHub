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
                .Select(x => new
                {
                    TimelineItems = x.TimelineItems(100, null, null, null, null, null, null).Select(x => new
                    {
                        #region AddedToProjectEvent
                        AddedToProjectEvent = x.Nodes.OfType<GraphQLModel.AddedToProjectEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                AvatarUrl = actor.AvatarUrl(100),
                                actor.Login,
                            })
                            .Single()
                        })
                        .ToList(),
                        #endregion

                        #region AssignedEvent
                        AssignedEvent = x.Nodes.OfType<GraphQLModel.AssignedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                AvatarUrl = actor.AvatarUrl(100),
                                actor.Login,
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region ClosedEvent
                        ClosedEvent = x.Nodes.OfType<GraphQLModel.ClosedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region CommentDeletedEvent
                        CommentDeletedEvent = x.Nodes.OfType<GraphQLModel.CommentDeletedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region ConnectedEvent
                        ConnectedEvent = x.Nodes.OfType<GraphQLModel.ConnectedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region ConvertedNoteToIssueEvent
                        ConvertedNoteToIssueEvent = x.Nodes.OfType<GraphQLModel.ConvertedNoteToIssueEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        //#region ConvertedToDiscussionEvent
                        //ConvertedToDiscussionEvent = x.Nodes.OfType<GraphQLModel.ConvertedToDiscussionEvent>().Select(y => new
                        //{
                        //    Actor = y.Actor.Select(actor => new
                        //    {
                        //        actor.Login,
                        //        AvatarUrl = actor.AvatarUrl(100),
                        //    })
                        //    .Single(),
                        //    y.CreatedAt,
                        //})
                        //.ToList(),
                        //#endregion

                        #region CrossReferencedEvent
                        CrossReferencedEvent = x.Nodes.OfType<GraphQLModel.CrossReferencedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region DemilestonedEvent
                        DemilestonedEvent = x.Nodes.OfType<GraphQLModel.DemilestonedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region DisconnectedEvent
                        DisconnectedEvent = x.Nodes.OfType<GraphQLModel.DisconnectedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region IssueComment
                        IssueComment = x.Nodes.OfType<GraphQLModel.IssueComment>().Select(y => new
                        {
                            Author = y.Author.Select(author => new
                            {
                                author.Login,
                                AvatarUrl = author.AvatarUrl(100),
                            })
                            .Single(),
                            y.AuthorAssociation,
                            y.BodyHTML,
                            Reactions = y.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new
                            {
                                Reactions = reaction.Select(reactionNode => new {
                                    reactionNode.Content,
                                    ReactedUserName = reactionNode.User.Login,
                                })
                                .Single(),
                            })
                            .ToList(),
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
                        })
                        .ToList(),
                        #endregion

                        #region LabeledEvent
                        LabeledEvent = x.Nodes.OfType<GraphQLModel.LabeledEvent>().Select(y => new
                        {
                            y.Label.Color,
                            y.Label.Name,
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region LockedEvent
                        LockedEvent = x.Nodes.OfType<GraphQLModel.LockedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region MarkedAsDuplicateEvent
                        MarkedAsDuplicateEvent = x.Nodes.OfType<GraphQLModel.MarkedAsDuplicateEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region MentionedEvent
                        MentionedEvent = x.Nodes.OfType<GraphQLModel.MentionedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region MilestonedEvent
                        MilestonedEvent = x.Nodes.OfType<GraphQLModel.MilestonedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region MovedColumnsInProjectEvent
                        MovedColumnsInProjectEvent = x.Nodes.OfType<GraphQLModel.MovedColumnsInProjectEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region PinnedEvent
                        PinnedEvent = x.Nodes.OfType<GraphQLModel.PinnedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region ReferencedEvent
                        ReferencedEvent = x.Nodes.OfType<GraphQLModel.ReferencedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region RemovedFromProjectEvent
                        RemovedFromProjectEvent = x.Nodes.OfType<GraphQLModel.RemovedFromProjectEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region RenamedTitleEvent
                        RenamedTitleEvent = x.Nodes.OfType<GraphQLModel.RenamedTitleEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region ReopenedEvent
                        ReopenedEvent = x.Nodes.OfType<GraphQLModel.ReopenedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region SubscribedEvent
                        SubscribedEvent = x.Nodes.OfType<GraphQLModel.SubscribedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnassignedEvent
                        UnassignedEvent = x.Nodes.OfType<GraphQLModel.UnassignedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnlabeledEvent
                        UnlabeledEvent = x.Nodes.OfType<GraphQLModel.UnlabeledEvent>().Select(y => new
                        {
                            y.Label.Color,
                            y.Label.Name,
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnlockedEvent
                        UnlockedEvent = x.Nodes.OfType<GraphQLModel.UnlockedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnmarkedAsDuplicateEvent
                        UnmarkedAsDuplicateEvent = x.Nodes.OfType<GraphQLModel.UnmarkedAsDuplicateEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnpinnedEvent
                        UnpinnedEvent = x.Nodes.OfType<GraphQLModel.UnpinnedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UnsubscribedEvent
                        UnsubscribedEvent = x.Nodes.OfType<GraphQLModel.UnsubscribedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion

                        #region UserBlockedEvent
                        UserBlockedEvent = x.Nodes.OfType<GraphQLModel.UserBlockedEvent>().Select(y => new
                        {
                            Actor = y.Actor.Select(actor => new
                            {
                                actor.Login,
                                AvatarUrl = actor.AvatarUrl(100),
                            })
                            .Single(),
                            y.CreatedAt,
                        })
                        .ToList(),
                        #endregion
                    })
                    .SingleOrDefault(),
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            List<Tuple<string, object>> allEvents = new();
            List<Tuple<string, int, DateTimeOffset>> allEventCreatedDates = new();

            #region copying
            List<CommentedEvent> commentedEvent = new();
            foreach (var item in response.TimelineItems.IssueComment)
            {
                CommentedEvent comment = new()
                {
                    AuthorAvatarUrl = item.Author.AvatarUrl,
                    AuthorLogin = item.Author.Login,
                    BodyHtml = item.BodyHTML,
                    MinimizedReason = item.MinimizedReason,
                    Url = item.Url,

                    IsEdited = item.LastEditedAt == null ? false : true,
                    IsMinimized = item.IsMinimized,
                    ViewerCanDelete = item.ViewerCanDelete,
                    ViewerCanMinimize = item.ViewerCanMinimize,
                    ViewerCanReact = item.ViewerCanReact,
                    ViewerCanUpdate = item.ViewerCanUpdate,
                    ViewerDidAuthor = item.ViewerDidAuthor,

                    AuthorAssociation = item.AuthorAssociation,

                    UpdatedAt = item.UpdatedAt,
                    CreatedAt = item.CreatedAt,
                };

                foreach(var reaction in item.Reactions)
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

                commentedEvent.Add(comment);
                allEventCreatedDates.Add(Tuple.Create("CommentedEvent", commentedEvent.Count() - 1, comment.CreatedAt));
            }

            List<LabeledEvent> labeledEvents = new();
            foreach (var item in response.TimelineItems.LabeledEvent)
            {
                LabeledEvent label = new()
                {
                    CreatedAt = item.CreatedAt,
                    CreatedAtHumanized = item.CreatedAt.Humanize(),
                    LabeledLabel = new()
                    {
                        Color = item.Color,
                        ColorBrush = Helpers.ColorHelper.HexCodeToSolidColorBrush(item.Color),
                        Name = item.Name,
                    },
                };

                labeledEvents.Add(label);
                allEventCreatedDates.Add(Tuple.Create("LabeledEvent", labeledEvents.Count() - 1, label.CreatedAt));
            }

            List<UnlabeledEvent> unlabeledEvents = new();
            foreach (var item in response.TimelineItems.UnlabeledEvent)
            {
                UnlabeledEvent unlabeledItem = new()
                {
                    CreatedAt = item.CreatedAt,
                    CreatedAtHumanized = item.CreatedAt.Humanize(),
                    UnlabeledLabel = new()
                    {
                        Color = item.Color,
                        ColorBrush = Helpers.ColorHelper.HexCodeToSolidColorBrush(item.Color),
                        Name = item.Name,
                    },
                };

                unlabeledEvents.Add(unlabeledItem);
                allEventCreatedDates.Add(Tuple.Create("UnlabeledEvent", unlabeledEvents.Count() - 1, unlabeledItem.CreatedAt));
            }
            #endregion

            // Sort by dates
            foreach (var item in allEventCreatedDates.OrderBy(x => x.Item3).ToList())
            {
                switch (item.Item1)
                {
                    case "CommentedEvent":
                        allEvents.Add(Tuple.Create(item.Item1, commentedEvent[item.Item2] as object));
                        break;
                    case "LabeledEvent":
                        allEvents.Add(Tuple.Create(item.Item1, labeledEvents[item.Item2] as object));
                        break;
                    case "UnlabeledEvent":
                        allEvents.Add(Tuple.Create(item.Item1, unlabeledEvents[item.Item2] as object));
                        break;
                }
            }

            return allEvents;
        }
    }
}
