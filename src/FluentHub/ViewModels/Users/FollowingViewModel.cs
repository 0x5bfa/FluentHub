using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
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
    public class FollowingViewModel : ObservableObject
    {
        #region constructor
        public FollowingViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _followingItems = new();
            FollowingItems = new(_followingItems);

            RefreshFollowingCommand = new AsyncRelayCommand<string>(RefreshFollowing);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<UserButtonBlockViewModel> _followingItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowingItems { get; }

        public IAsyncRelayCommand RefreshFollowingCommand { get; }
        #endregion

        #region methods
        private async Task RefreshFollowing(string login, CancellationToken token)
        {
            try
            {
                FollowingQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _followingItems.Clear();
                foreach (var item in items)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = item,
                    };

                    _followingItems.Add(viewModel);
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
        #endregion
    }
}
