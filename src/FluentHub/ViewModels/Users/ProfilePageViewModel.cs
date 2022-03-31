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
        public ProfilePageViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            RefreshUserCommand = new AsyncRelayCommand<string>(LoadUserAsync);
        }

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private User _userItem;
        private bool _isNotViewer;
        public User UserItem
        {
            get => _userItem;
            private set => SetProperty(ref _userItem, value);
        }
        public bool IsNotViewer
        {
            get => _isNotViewer;
            set => SetProperty(ref _isNotViewer, value);
        }

        private Uri _builtWebsiteUrl;
        public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

        public IAsyncRelayCommand RefreshUserCommand { get; }

        private async Task LoadUserAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                UserItem = await queries.GetAsync(login);

                if (UserItem == null) return;

                BuiltWebsiteUrl = new UriBuilder(UserItem.WebsiteUrl).Uri;

                if (!UserItem.IsViewer)
                    IsNotViewer = true;
            }
            catch (Exception ex)
            {
                _logger?.Error("Failed to load user.", ex);
                UserItem = new User();
                IsNotViewer = false;
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