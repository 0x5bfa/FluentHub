using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories
{
    public class IssueViewModel : ObservableObject
    {
        #region constructor
        public IssueViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            RefreshIssuePageCommand = new AsyncRelayCommand<Issue>(RefreshIssuePageAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private Issue issueItem;
        #endregion

        #region properties
        public Issue IssueItem { get => issueItem; private set => SetProperty(ref issueItem, value); }
        public IAsyncRelayCommand RefreshIssuePageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshIssuePageAsync(Issue issue)
        {
            try
            {
                // Load comments/events
                IssueItem = issue;

                IssueEventQueries queries = new();
                var eventsGroup = await queries.GetAllAsync(issue.OwnerLogin, issue.Name, issue.Number);
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
