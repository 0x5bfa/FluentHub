using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class ProfilePageViewModel : ObservableObject
    {
        public ProfilePageViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            RefreshUserCommand = new AsyncRelayCommand<string>(LoadUserProfileAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private User _userItem;
        public User UserItem { get => _userItem; private set => SetProperty(ref _userItem, value); }

        private Uri _builtWebsiteUrl;
        public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

        public IAsyncRelayCommand RefreshUserCommand { get; }
        #endregion

        private async Task LoadUserProfileAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                var item = await queries.GetAsync(login);
                if (item == null) return;

                UserItem = item;

                if (string.IsNullOrEmpty(UserItem.WebsiteUrl) is false)
                {
                    BuiltWebsiteUrl = new UriBuilder(UserItem.WebsiteUrl).Uri;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserProfileAsync), ex);
                UserItem = new User();
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
