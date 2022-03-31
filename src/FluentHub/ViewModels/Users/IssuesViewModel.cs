using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class IssuesViewModel : ObservableObject
    {
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;

            _issueItems = new();
            IssueItems = new(_issueItems);

            RefreshIssuesCommand = new AsyncRelayCommand<string>(RefreshIssuesAsync, CanRefreshIssues);
        }

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<IssueButtonBlockViewModel> _issueItems;

        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> IssueItems { get; }

        public IAsyncRelayCommand RefreshIssuesCommand { get; }

        private bool CanRefreshIssues(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshIssuesAsync(string username)
        {
            try
            {
                IssueQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                if (items == null) return;

                _issueItems.Clear();
                foreach (var item in items)
                {
                    IssueButtonBlockViewModel viewModel = new()
                    {
                        IssueItem = item,
                        NameWithOwner = item.Owner + "/" + item.Name + " #" + item.Number,
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
                    };

                    _issueItems.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshIssuesAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}