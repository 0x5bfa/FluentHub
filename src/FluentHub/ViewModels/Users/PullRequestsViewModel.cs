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
    public class PullRequestsViewModel : ObservableObject
    {
        public PullRequestsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;

            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsCommand = new AsyncRelayCommand<string>(RefreshPullRequestsAsync, CanRefreshPullRequests);
        }

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }

        public IAsyncRelayCommand RefreshPullRequestsCommand { get; }

        private bool CanRefreshPullRequests(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshPullRequestsAsync(string username)
        {
            try
            {
                PullRequestQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                if (items == null) return;

                _pullRequests.Clear();
                foreach (var item in items)
                {
                    PullButtonBlockViewModel viewModel = new()
                    {
                        PullItem = item,
                        NameWithOwner = $"{item.Owner} / {item.Name} #{item.Number}",
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
                    };

                    _pullRequests.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshPullRequestsAsync", ex);
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