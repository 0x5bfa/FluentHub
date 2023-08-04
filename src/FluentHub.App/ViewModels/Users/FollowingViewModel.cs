using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Users
{
    public class FollowingViewModel : ObservableObject
    {
        public FollowingViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _followingItems = new();
            FollowingItems = new(_followingItems);

            LoadUserFollowingPageCommand = new AsyncRelayCommand(LoadUserFollowingPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<UserBlockButtonViewModel> _followingItems;
        public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowingItems { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserFollowingPageCommand { get; }
        #endregion

        private async Task LoadUserFollowingPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserFollowingPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadUserAsync);
                await LoadUserAsync(Login);

                _currentTaskingMethodName = nameof(LoadUserFollowingAsync);
                await LoadUserFollowingAsync(Login);
            }
            catch (Exception ex)
            {
                TaskException = ex;
                faulted = true;

                _logger?.Error(_currentTaskingMethodName, ex);
                throw;
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadUserFollowingAsync(string login)
        {
            FollowingQueries queries = new();
            var response = await queries.GetAllAsync(login);

            _followingItems.Clear();
            foreach (var item in response)
            {
                UserBlockButtonViewModel viewModel = new()
                {
                    User = item,
                };

                _followingItems.Add(viewModel);
            }
        }

        public async Task LoadUserAsync(string login)
        {
            UserQueries queries = new();
            var response = await queries.GetAsync(login);

            User = response ?? new();

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

        private void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Following";
            currentItem.Description = $"People {User?.Login} is following";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };
        }
    }
}
