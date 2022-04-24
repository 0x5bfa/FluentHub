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

namespace FluentHub.ViewModels.Repositories.Issues
{
    public class IssueViewModel : ObservableObject
    {
        #region constructor
        public IssueViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _eventBlocks = new();
            EventBlocks = new(_eventBlocks);

            RefreshIssuePageCommand = new AsyncRelayCommand<Issue>(RefreshIssuePageAsync);
        }
        #endregion

        #region fields and properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Issue _issueItem;
        public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }

        private IssueEventBlockViewModel _eventBlockViewModel;
        public IssueEventBlockViewModel EventBlockViewModel { get => _eventBlockViewModel; private set => SetProperty(ref _eventBlockViewModel, value); }

        private readonly ObservableCollection<IssueEventBlock> _eventBlocks;
        public ReadOnlyObservableCollection<IssueEventBlock> EventBlocks { get; }

        public IAsyncRelayCommand RefreshIssuePageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshIssuePageAsync(Issue issue)
        {
            try
            {
                IssueItem = issue;
                _eventBlocks.Clear();

                IssueQueries issueQueries = new();

                var bodyComment = await issueQueries.GetBodyAsync(issue.OwnerLogin, issue.Name, issue.Number);

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

                IssueEventQueries queries = new();
                var issueEvents = await queries.GetAllAsync(issue.OwnerLogin, issue.Name, issue.Number);

                foreach (var eventItem in issueEvents)
                {
                    if (eventItem == null) continue;

                    var viewmodel = new IssueEventBlockViewModel()
                    {
                        // FluentHub.Octokit.Models.Events.*
                        EventType = eventItem.GetType().ToString().Split(".")[4],
                        Event = eventItem,
                    };

                    switch (viewmodel.EventType)
                    {
                        case nameof(AddedToProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.AddedToProjectEvent = eventItem as AddedToProjectEvent;
                            viewmodel.Actor = viewmodel.AddedToProjectEvent.Actor;
                            break;
                        case nameof(AssignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.AssignedEvent = eventItem as AssignedEvent;
                            viewmodel.Actor = viewmodel.AssignedEvent.Actor;
                            break;
                        case nameof(ClosedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ClosedEvent = eventItem as ClosedEvent;
                            viewmodel.Actor = viewmodel.ClosedEvent.Actor;
                            break;
                        case nameof(CommentDeletedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CommentDeletedEvent = eventItem as CommentDeletedEvent;
                            viewmodel.Actor = viewmodel.CommentDeletedEvent.Actor;
                            break;
                        case nameof(ConnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConnectedEvent = eventItem as ConnectedEvent;
                            viewmodel.Actor = viewmodel.ConnectedEvent.Actor;
                            break;
                        case nameof(ConvertedNoteToIssueEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConvertedNoteToIssueEvent = eventItem as ConvertedNoteToIssueEvent;
                            viewmodel.Actor = viewmodel.ConvertedNoteToIssueEvent.Actor;
                            break;
                        case nameof(CrossReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CrossReferencedEvent = eventItem as CrossReferencedEvent;
                            viewmodel.Actor = viewmodel.CrossReferencedEvent.Actor;
                            break;
                        case nameof(DemilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DemilestonedEvent = eventItem as DemilestonedEvent;
                            viewmodel.Actor = viewmodel.DemilestonedEvent.Actor;
                            break;
                        case nameof(DisconnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DisconnectedEvent = eventItem as DisconnectedEvent;
                            viewmodel.Actor = viewmodel.DisconnectedEvent.Actor;
                            break;
                        case nameof(IssueComment):
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem as IssueComment };
                            break;
                        case nameof(LabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAA5";
                            viewmodel.LabeledEvent = eventItem as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.LabeledEvent.Label.Name,
                                Color = viewmodel.LabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel.LabeledEvent.Actor;
                            break;
                        case nameof(LockedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#636E7B");
                            viewmodel.TimelineBadgeGlyph = "\uEA05";
                            viewmodel.LockedEvent = eventItem as LockedEvent;
                            viewmodel.Actor = viewmodel.LockedEvent.Actor;
                            break;
                        case nameof(MarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE924";
                            viewmodel.MarkedAsDuplicateEvent = eventItem as MarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel.MarkedAsDuplicateEvent.Actor;
                            break;
                        //case nameof(MentionedEvent):
                        case nameof(MilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA12";
                            viewmodel.MilestonedEvent = eventItem as MilestonedEvent;
                            viewmodel.Actor = viewmodel.MilestonedEvent.Actor;
                            break;
                        case nameof(MovedColumnsInProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.MovedColumnsInProjectEvent = eventItem as MovedColumnsInProjectEvent;
                            viewmodel.Actor = viewmodel.MovedColumnsInProjectEvent.Actor;
                            break;
                        case nameof(PinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.PinnedEvent = eventItem as PinnedEvent;
                            viewmodel.Actor = viewmodel.PinnedEvent.Actor;
                            break;
                        case nameof(ReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ReferencedEvent = eventItem as ReferencedEvent;
                            viewmodel.Actor = viewmodel.ReferencedEvent.Actor;
                            break;
                        case nameof(RemovedFromProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.RemovedFromProjectEvent = eventItem as RemovedFromProjectEvent;
                            viewmodel.Actor = viewmodel.RemovedFromProjectEvent.Actor;
                            break;
                        case nameof(RenamedTitleEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.RenamedTitleEvent = eventItem as RenamedTitleEvent;
                            viewmodel.Actor = viewmodel.RenamedTitleEvent.Actor;
                            break;
                        case nameof(ReopenedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ReopenedEvent = eventItem as ReopenedEvent;
                            viewmodel.Actor = viewmodel.ReopenedEvent.Actor;
                            break;
                        //case nameof(SubscribedEvent):
                        case nameof(UnassignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnassignedEvent = eventItem as UnassignedEvent;
                            viewmodel.Actor = viewmodel.UnassignedEvent.Actor;
                            break;
                        case nameof(UnlabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlabeledEvent = eventItem as UnlabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.UnlabeledEvent.Label.Name,
                                Color = viewmodel.UnlabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel.UnlabeledEvent.Actor;
                            break;
                        case nameof(UnlockedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlockedEvent = eventItem as UnlockedEvent;
                            viewmodel.Actor = viewmodel.UnlockedEvent.Actor;
                            break;
                        case nameof(UnmarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem as UnmarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel.UnmarkedAsDuplicateEvent.Actor;
                            break;
                        case nameof(UnpinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnpinnedEvent = eventItem as UnpinnedEvent;
                            viewmodel.Actor = viewmodel.UnpinnedEvent.Actor;
                            break;
                        //case nameof(UnsubscribedEvent):
                        case nameof(UserBlockedEvent):
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
                _logger?.Error("RefreshIssuePageAsync", ex);
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
