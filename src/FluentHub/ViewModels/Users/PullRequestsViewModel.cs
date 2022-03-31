using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class PullRequestsViewModel : ObservableObject
    {
        public PullRequestsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsCommand = new AsyncRelayCommand<string>(RefreshPullRequestsAsync);
        }

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }

        public IAsyncRelayCommand RefreshPullRequestsCommand { get; }

        private async Task RefreshPullRequestsAsync(string login)
        {
            try
            {
                PullRequestQueries queries = new();
                List<PullRequest> items;

                items = login == null ?
                    await queries.GetAllAsync() :
                    await queries.GetAllAsync(login);

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