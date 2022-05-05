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
    public class ProfilePageViewModel : ObservableObject
    {
        #region constructor
        public ProfilePageViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            RefreshUserCommand = new AsyncRelayCommand<string>(LoadUserAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private User _userItem;
        private Uri _builtWebsiteUrl;
        #endregion

        #region properties
        public User UserItem { get => _userItem; private set => SetProperty(ref _userItem, value); }
        public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }
        public IAsyncRelayCommand RefreshUserCommand { get; }
        #endregion

        #region methods
        private async Task LoadUserAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                var item = await queries.GetAsync(login);
                if (item == null) return;

                UserItem = item;

                if (string.IsNullOrEmpty(UserItem?.WebsiteUrl) is false)
                {
                    BuiltWebsiteUrl = new UriBuilder(UserItem?.WebsiteUrl).Uri;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("Failed to load user.", ex);
                UserItem = new User();
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