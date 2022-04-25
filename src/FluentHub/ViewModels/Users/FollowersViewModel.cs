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
    public class FollowersViewModel : ObservableObject
    {
        #region constructor
        public FollowersViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _followersItems = new();
            FollowersItems = new(_followersItems);

            RefreshFollowersCommand = new AsyncRelayCommand<string>(RefreshFollowers);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<UserButtonBlockViewModel> _followersItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowersItems { get; }

        public IAsyncRelayCommand RefreshFollowersCommand { get; }
        #endregion

        #region methods
        private async Task RefreshFollowers(string login, CancellationToken token)
        {
            try
            {
                FollowersQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _followersItems.Clear();
                foreach (var item in items)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = item,
                    };

                    _followersItems.Add(viewModel);
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
