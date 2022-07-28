using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class FollowingViewModel : ObservableObject
    {
        public FollowingViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _followingItems = new();
            FollowingItems = new(_followingItems);

            RefreshFollowingCommand = new AsyncRelayCommand(LoadUserFollowingAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<UserButtonBlockViewModel> _followingItems;
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowingItems { get; }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        public IAsyncRelayCommand RefreshFollowingCommand { get; }
        #endregion

        private async Task LoadUserFollowingAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                FollowingQueries queries = new();
                var response = await queries.GetAllAsync(Login);

                _followingItems.Clear();
                foreach (var item in response)
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
                _logger?.Error(nameof(LoadUserFollowingAsync), ex);
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
                _messenger?.Send(new LoadingMessaging(true));

                UserQueries queries = new();
                var response = await queries.GetAsync(login);

                User = response ?? new();

                // View model
                UserProfileOverviewViewModel = new()
                {
                    User = User,
                    SelectedTag = "following",
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
