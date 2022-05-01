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
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class PullRequestsViewModel : ObservableObject
    {
        #region constructor
        public PullRequestsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsPageCommand = new AsyncRelayCommand<string>(RefreshPullRequestsPageAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }
        public IAsyncRelayCommand RefreshPullRequestsPageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshPullRequestsPageAsync(string login, CancellationToken token)
        {
            try
            {
                PullRequestQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _pullRequests.Clear();
                foreach (var item in items)
                {
                    PullButtonBlockViewModel viewModel = new()
                    {
                        PullItem = item,
                    };

                    _pullRequests.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
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
        #endregion
    }
}