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
                        EventType = "CommentedEvent",
                        Event = bodyComment,
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
                        Event = eventItem.Item2,
                    };

                    if (eventItem.Item1 == "CommentedEvent")
                    {
                        viewmodel.CommentBlockViewModel = new()
                        {
                            IssueComment = eventItem.Item2 as IssueComment,
                        };
                    }

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
