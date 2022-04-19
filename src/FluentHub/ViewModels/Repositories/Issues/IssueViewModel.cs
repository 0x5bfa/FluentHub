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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                    var viewmodel = new IssueEventBlockViewModel()
                    {
                        // FluentHub.Octokit.Models.Events.*
                        EventType = eventItem.GetType().ToString().Split(".")[4],
                    };

                    switch (viewmodel.EventType)
                    {
                        case "AddedToProjectEvent":
                            viewmodel.AddedToProjectEvent = eventItem as AddedToProjectEvent;
                            break;
                        case "AssignedEvent":
                            viewmodel.AssignedEvent = eventItem as AssignedEvent;
                            break;
                        case "ClosedEvent":
                            viewmodel.ClosedEvent = eventItem as ClosedEvent;
                            break;
                        case "CommentDeletedEvent":
                            viewmodel.CommentDeletedEvent = eventItem as CommentDeletedEvent;
                            break;
                        case "ConnectedEvent":
                            viewmodel.ConnectedEvent = eventItem as ConnectedEvent;
                            break;
                        case "ConvertedNoteToIssueEvent":
                            viewmodel.ConvertedNoteToIssueEvent = eventItem as ConvertedNoteToIssueEvent;
                            break;
                        case "CrossReferencedEvent":
                            viewmodel.CrossReferencedEvent = eventItem as CrossReferencedEvent;
                            break;
                        case "DemilestonedEvent":
                            viewmodel.DemilestonedEvent = eventItem as DemilestonedEvent;
                            break;
                        case "DisconnectedEvent":
                            viewmodel.DisconnectedEvent = eventItem as DisconnectedEvent;
                            break;
                        default:
                        case "IssueComment":
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem as IssueComment };
                            break;
                        case "LabeledEvent":
                            viewmodel.LabeledEvent = eventItem as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.LabeledEvent.Label.Name,
                                Color = viewmodel.LabeledEvent.Label.Color,
                            };
                            break;
                        case "LockedEvent":
                            viewmodel.LockedEvent = eventItem as LockedEvent;
                            break;
                        case "MarkedAsDuplicateEvent":
                            viewmodel.MarkedAsDuplicateEvent = eventItem as MarkedAsDuplicateEvent;
                            break;
                        case "MentionedEvent":
                            viewmodel.MentionedEvent = eventItem as MentionedEvent;
                            break;
                        case "MilestonedEvent":
                            viewmodel.MilestonedEvent = eventItem as MilestonedEvent;
                            break;
                        case "MovedColumnsInProjectEvent":
                            viewmodel.MovedColumnsInProjectEvent = eventItem as MovedColumnsInProjectEvent;
                            break;
                        case "PinnedEvent":
                            viewmodel.PinnedEvent = eventItem as PinnedEvent;
                            break;
                        case "ReferencedEvent":
                            viewmodel.ReferencedEvent = eventItem as ReferencedEvent;
                            break;
                        case "RemovedFromProjectEvent":
                            viewmodel.RemovedFromProjectEvent = eventItem as RemovedFromProjectEvent;
                            break;
                        case "RenamedTitleEvent":
                            viewmodel.RenamedTitleEvent = eventItem as RenamedTitleEvent;
                            break;
                        case "ReopenedEvent":
                            viewmodel.ReopenedEvent = eventItem as ReopenedEvent;
                            break;
                        case "SubscribedEvent":
                            viewmodel.SubscribedEvent = eventItem as SubscribedEvent;
                            break;
                        case "UnassignedEvent":
                            viewmodel.UnassignedEvent = eventItem as UnassignedEvent;
                            break;
                        case "UnlabeledEvent":
                            viewmodel.UnlabeledEvent = eventItem as UnlabeledEvent;
                            break;
                        case "UnlockedEvent":
                            viewmodel.UnlockedEvent = eventItem as UnlockedEvent;
                            break;
                        case "UnmarkedAsDuplicateEvent":
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem as UnmarkedAsDuplicateEvent;
                            break;
                        case "UnpinnedEvent":
                            viewmodel.UnpinnedEvent = eventItem as UnpinnedEvent;
                            break;
                        case "UnsubscribedEvent":
                            viewmodel.UnsubscribedEvent = eventItem as UnsubscribedEvent;
                            break;
                        case "UserBlockedEvent":
                            viewmodel.UserBlockedEvent = eventItem as UserBlockedEvent;
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
