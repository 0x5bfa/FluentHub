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

        #region properties
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
                        EventType = eventItem.Item1,
                    };

                    switch (eventItem.Item1)
                    {
                        case "AddedToProjectEvent":
                            viewmodel.AddedToProjectEvent = eventItem.Item2 as AddedToProjectEvent;
                            break;
                        case "AssignedEvent":
                            viewmodel.AssignedEvent = eventItem.Item2 as AssignedEvent;
                            break;
                        case "ClosedEvent":
                            viewmodel.ClosedEvent = eventItem.Item2 as ClosedEvent;
                            break;
                        case "CommentDeletedEvent":
                            viewmodel.CommentDeletedEvent = eventItem.Item2 as CommentDeletedEvent;
                            break;
                        case "ConnectedEvent":
                            viewmodel.ConnectedEvent = eventItem.Item2 as ConnectedEvent;
                            break;
                        case "ConvertedNoteToIssueEvent":
                            viewmodel.ConvertedNoteToIssueEvent = eventItem.Item2 as ConvertedNoteToIssueEvent;
                            break;
                        case "CrossReferencedEvent":
                            viewmodel.CrossReferencedEvent = eventItem.Item2 as CrossReferencedEvent;
                            break;
                        case "DemilestonedEvent":
                            viewmodel.DemilestonedEvent = eventItem.Item2 as DemilestonedEvent;
                            break;
                        case "DisconnectedEvent":
                            viewmodel.DisconnectedEvent = eventItem.Item2 as DisconnectedEvent;
                            break;
                        default: case "IssueComment":
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem.Item2 as IssueComment };
                            break;
                        case "LabeledEvent":
                            viewmodel.LabeledEvent = eventItem.Item2 as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel.LabeledEvent.LabeledLabel.Name,
                                BackgroundColorBrush = viewmodel.LabeledEvent.LabeledLabel.ColorBrush,
                            };
                            break;
                        case "LockedEvent":
                            viewmodel.LockedEvent = eventItem.Item2 as LockedEvent;
                            break;
                        case "MarkedAsDuplicateEvent":
                            viewmodel.MarkedAsDuplicateEvent = eventItem.Item2 as MarkedAsDuplicateEvent;
                            break;
                        case "MentionedEvent":
                            viewmodel.MentionedEvent = eventItem.Item2 as MentionedEvent;
                            break;
                        case "MilestonedEvent":
                            viewmodel.MilestonedEvent = eventItem.Item2 as MilestonedEvent;
                            break;
                        case "MovedColumnsInProjectEvent":
                            viewmodel.MovedColumnsInProjectEvent = eventItem.Item2 as MovedColumnsInProjectEvent;
                            break;
                        case "PinnedEvent":
                            viewmodel.PinnedEvent = eventItem.Item2 as PinnedEvent;
                            break;
                        case "ReferencedEvent":
                            viewmodel.ReferencedEvent = eventItem.Item2 as ReferencedEvent;
                            break;
                        case "RemovedFromProjectEvent":
                            viewmodel.RemovedFromProjectEvent = eventItem.Item2 as RemovedFromProjectEvent;
                            break;
                        case "RenamedTitleEvent":
                            viewmodel.RenamedTitleEvent = eventItem.Item2 as RenamedTitleEvent;
                            break;
                        case "ReopenedEvent":
                            viewmodel.ReopenedEvent = eventItem.Item2 as ReopenedEvent;
                            break;
                        case "SubscribedEvent":
                            viewmodel.SubscribedEvent = eventItem.Item2 as SubscribedEvent;
                            break;
                        case "UnassignedEvent":
                            viewmodel.UnassignedEvent = eventItem.Item2 as UnassignedEvent;
                            break;
                        case "UnlabeledEvent":
                            viewmodel.UnlabeledEvent = eventItem.Item2 as UnlabeledEvent;
                            break;
                        case "UnlockedEvent":
                            viewmodel.UnlockedEvent = eventItem.Item2 as UnlockedEvent;
                            break;
                        case "UnmarkedAsDuplicateEvent":
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem.Item2 as UnmarkedAsDuplicateEvent;
                            break;
                        case "UnpinnedEvent":
                            viewmodel.UnpinnedEvent = eventItem.Item2 as UnpinnedEvent;
                            break;
                        case "UnsubscribedEvent":
                            viewmodel.UnsubscribedEvent = eventItem.Item2 as UnsubscribedEvent;
                            break;
                        case "UserBlockedEvent":
                            viewmodel.UserBlockedEvent = eventItem.Item2 as UserBlockedEvent;
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
