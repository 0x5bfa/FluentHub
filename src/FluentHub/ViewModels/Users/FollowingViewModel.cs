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
    public class FollowingViewModel : ObservableObject
    {
        #region constructor
        public FollowingViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _followingItems = new();
            FollowingItems = new(_followingItems);
            RefreshFollowingCommand = new AsyncRelayCommand<string>(RefreshFollowingAsync, CanRefreshFollowing);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<UserButtonBlockViewModel> _followingItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowingItems { get; }
        public IAsyncRelayCommand RefreshFollowingCommand { get; }
        #endregion

        #region methods
        private bool CanRefreshFollowing(string username) => !string.IsNullOrEmpty(username);
        private async Task RefreshFollowingAsync(string username, CancellationToken token)
        {
            try
            {
                FollowingQueries client = new();
                var following = await client.GetAllAsync(username);

                foreach (var user in following)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = user
                    };
                    _followingItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshFollowingAsync", ex);
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
