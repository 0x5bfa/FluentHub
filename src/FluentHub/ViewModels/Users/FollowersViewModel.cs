using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class FollowersViewModel : ObservableObject
    {
        #region constructor
        public FollowersViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _followerItems = new();
            FollowerItems = new(_followerItems);
            RefreshFollowersCommand = new AsyncRelayCommand<string>(RefreshFollowersAsync, CanRefreshFollowers);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<UserButtonBlockViewModel> _followerItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowerItems { get; }
        public IAsyncRelayCommand RefreshFollowersCommand { get; }
        #endregion

        #region methods
        private bool CanRefreshFollowers(string username) => !string.IsNullOrEmpty(username);
        private async Task RefreshFollowersAsync(string username, CancellationToken token)
        {
            try
            {
                FollowersQueries client = new();
                var followers = await client.GetAllAsync(username);

                foreach (var user in followers)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = user
                    };
                    _followerItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshFollowersAsync", ex);
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
