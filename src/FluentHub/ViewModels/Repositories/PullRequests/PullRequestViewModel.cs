using FluentHub.Backend;
using FluentHub.Octokit.Models.Events;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.UserControls.Blocks;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
    public class PullRequestViewModel : ObservableObject
    {
        #region constructor
        public PullRequestViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _eventBlocks = new();
            EventBlocks = new(_eventBlocks);

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(RefreshPullRequestPageAsync);
        }
        #endregion

        #region properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private IssueEventBlockViewModel _eventBlockViewModel;
        public IssueEventBlockViewModel EventBlockViewModel { get => _eventBlockViewModel; private set => SetProperty(ref _eventBlockViewModel, value); }

        private readonly ObservableCollection<IssueEventBlock> _eventBlocks;
        public ReadOnlyObservableCollection<IssueEventBlock> EventBlocks { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshPullRequestPageAsync(PullRequest pull)
        {
            try
            {
                if (pull != null) PullItem = pull;

                _eventBlocks.Clear();

                PullRequestQueries pullRequestQueries = new();

                var bodyComment = await pullRequestQueries.GetBodyAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                var bodyCommentBlock = new IssueEventBlock()
                {
                    PropertyViewModel = new IssueEventBlockViewModel()
                    {
                        EventType = "IssueComment",
                        IssueComment = bodyComment,
                        CommentBlockViewModel = new()
                        {
                            IssueComment = bodyComment,
                        },
                    },
                };

                _eventBlocks.Add(bodyCommentBlock);

                PullRequestEventQueries queries = new();
                var pullEvents = await queries.GetAllAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                foreach (var eventItem in pullEvents)
                {
                    if (eventItem == null) continue;

                    var viewmodel = new IssueEventBlockViewModel()
                    {
                        // FluentHub.Octokit.Models.Events.*
                        EventType = eventItem.GetType().ToString().Split(".")[4],
                    };

                    switch (viewmodel.EventType)
                    {
                        case "AddedToProjectEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.AddedToProjectEvent = eventItem as AddedToProjectEvent;
                            viewmodel.Actor = viewmodel.AddedToProjectEvent.Actor;
                            break;
                        case "AssignedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.AssignedEvent = eventItem as AssignedEvent;
                            viewmodel.Actor = viewmodel.AssignedEvent.Actor;
                            break;
                        case "ClosedEvent":
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ClosedEvent = eventItem as ClosedEvent;
                            viewmodel.Actor = viewmodel.ClosedEvent.Actor;
                            break;
                        case "CommentDeletedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CommentDeletedEvent = eventItem as CommentDeletedEvent;
                            viewmodel.Actor = viewmodel.CommentDeletedEvent.Actor;
                            break;
                        case "ConnectedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConnectedEvent = eventItem as ConnectedEvent;
                            viewmodel.Actor = viewmodel.ConnectedEvent.Actor;
                            break;
                        case "ConvertedNoteToIssueEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConvertedNoteToIssueEvent = eventItem as ConvertedNoteToIssueEvent;
                            viewmodel.Actor = viewmodel.ConvertedNoteToIssueEvent.Actor;
                            break;
                        case "CrossReferencedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CrossReferencedEvent = eventItem as CrossReferencedEvent;
                            viewmodel.Actor = viewmodel.CrossReferencedEvent.Actor;
                            break;
                        case "DemilestonedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DemilestonedEvent = eventItem as DemilestonedEvent;
                            viewmodel.Actor = viewmodel.DemilestonedEvent.Actor;
                            break;
                        case "DisconnectedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DisconnectedEvent = eventItem as DisconnectedEvent;
                            viewmodel.Actor = viewmodel.DisconnectedEvent.Actor;
                            break;
                        case "IssueComment":
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem as IssueComment };
                            break;
                        case "LabeledEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEAA5";
                            viewmodel.LabeledEvent = eventItem as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.LabeledEvent.Label.Name,
                                Color = viewmodel.LabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel.LabeledEvent.Actor;
                            break;
                        case "LockedEvent":
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#636E7B");
                            viewmodel.TimelineBadgeGlyph = "\uEA05";
                            viewmodel.LockedEvent = eventItem as LockedEvent;
                            viewmodel.Actor = viewmodel.LockedEvent.Actor;
                            break;
                        case "MarkedAsDuplicateEvent":
                            viewmodel.TimelineBadgeGlyph = "\uE924";
                            viewmodel.MarkedAsDuplicateEvent = eventItem as MarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel.MarkedAsDuplicateEvent.Actor;
                            break;
                        //case "MentionedEvent":
                        case "MilestonedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEA12";
                            viewmodel.MilestonedEvent = eventItem as MilestonedEvent;
                            viewmodel.Actor = viewmodel.MilestonedEvent.Actor;
                            break;
                        case "MovedColumnsInProjectEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.MovedColumnsInProjectEvent = eventItem as MovedColumnsInProjectEvent;
                            viewmodel.Actor = viewmodel.MovedColumnsInProjectEvent.Actor;
                            break;
                        case "PinnedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.PinnedEvent = eventItem as PinnedEvent;
                            viewmodel.Actor = viewmodel.PinnedEvent.Actor;
                            break;
                        case "ReferencedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ReferencedEvent = eventItem as ReferencedEvent;
                            viewmodel.Actor = viewmodel.ReferencedEvent.Actor;
                            break;
                        case "RemovedFromProjectEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.RemovedFromProjectEvent = eventItem as RemovedFromProjectEvent;
                            viewmodel.Actor = viewmodel.RemovedFromProjectEvent.Actor;
                            break;
                        case "RenamedTitleEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.RenamedTitleEvent = eventItem as RenamedTitleEvent;
                            viewmodel.Actor = viewmodel.RenamedTitleEvent.Actor;
                            break;
                        case "ReopenedEvent":
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ReopenedEvent = eventItem as ReopenedEvent;
                            viewmodel.Actor = viewmodel.ReopenedEvent.Actor;
                            break;
                        //case "SubscribedEvent":
                        case "UnassignedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnassignedEvent = eventItem as UnassignedEvent;
                            viewmodel.Actor = viewmodel.UnassignedEvent.Actor;
                            break;
                        case "UnlabeledEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlabeledEvent = eventItem as UnlabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.UnlabeledEvent.Label.Name,
                                Color = viewmodel.UnlabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel.UnlabeledEvent.Actor;
                            break;
                        case "UnlockedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlockedEvent = eventItem as UnlockedEvent;
                            viewmodel.Actor = viewmodel.UnlockedEvent.Actor;
                            break;
                        case "UnmarkedAsDuplicateEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem as UnmarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel.UnmarkedAsDuplicateEvent.Actor;
                            break;
                        case "UnpinnedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnpinnedEvent = eventItem as UnpinnedEvent;
                            viewmodel.Actor = viewmodel.UnpinnedEvent.Actor;
                            break;
                        //case "UnsubscribedEvent":
                        case "UserBlockedEvent":
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UserBlockedEvent = eventItem as UserBlockedEvent;
                            viewmodel.Actor = viewmodel.UserBlockedEvent.Actor;
                            break;
                    };

                    var eventBlock = new IssueEventBlock()
                    {
                        PropertyViewModel = viewmodel
                    };

                    _eventBlocks.Add(eventBlock);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshPullRequestPageAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
        #endregion
    }
}
