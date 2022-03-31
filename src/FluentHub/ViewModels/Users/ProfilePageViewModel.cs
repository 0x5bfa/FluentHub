using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class ProfilePageViewModel : ObservableObject
    {
        public ProfilePageViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;

            LoadUserCommand = new AsyncRelayCommand<string>(LoadUserAsync, CanLoadUser);
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

        public IAsyncRelayCommand LoadUserCommand { get; }

        private bool CanLoadUser(string username) => !string.IsNullOrEmpty(username);

        private async Task LoadUserAsync(string username)
        {
            try
            {
                UserQueries queries = new();
                UserItem = await queries.GetAsync(username);

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