using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class FollowersViewModel : ObservableObject
    {
        public FollowersViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _followersItems = new();
            FollowersItems = new(_followersItems);

            RefreshFollowersCommand = new AsyncRelayCommand(LoadUserFollowersAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<UserButtonBlockViewModel> _followersItems;
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowersItems { get; }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        public IAsyncRelayCommand RefreshFollowersCommand { get; }
        #endregion

        private async Task LoadUserFollowersAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                FollowersQueries queries = new();
                var response = await queries.GetAllAsync(Login);

                _followersItems.Clear();
                foreach (var item in response)
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
                _logger?.Error(nameof(LoadUserFollowersAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new LoadingMessaging(false));
            }
        }

        public async Task LoadUserAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(login);

                User = response ?? new();

                // View model
                UserProfileOverviewViewModel = new()
                {
                    User = User,
                    SelectedTag = "followers",
                };

                if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
                {
                    UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserAsync), ex);
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
