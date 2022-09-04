using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Home
{
    public class UserHomeViewModel : ObservableObject
    {
        public UserHomeViewModel(IMessenger messenger = null, ILogger logger = null, INavigationService navigation = null)
        {
            _messenger = messenger;
            _logger = logger;
            _navigation = navigation;

            _userRepositories = new();
            UserRepositories = new(_userRepositories);

            _userNotifications = new();
            UserNotifications = new(_userNotifications);

            _folderCardItems = new();
            FolderCardItems = new(_folderCardItems);

            InitializeFolderCardItems();

            LoadUserHomePageCommand = new AsyncRelayCommand(LoadUserHomePageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly INavigationService _navigation;

        private readonly ObservableCollection<Repository> _userRepositories;
        public ReadOnlyObservableCollection<Repository> UserRepositories { get; }

        private readonly ObservableCollection<FolderCardItem> _folderCardItems;
        public ReadOnlyObservableCollection<FolderCardItem> FolderCardItems { get; }

        private readonly ObservableCollection<Notification> _userNotifications;
        public ReadOnlyObservableCollection<Notification> UserNotifications { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserHomePageCommand { get; }
        //public IAsyncRelayCommand ClickHomeRepositoriesListItemCommand { get; }
        //public IAsyncRelayCommand ClickFolderCardCommand { get; }
        //public IAsyncRelayCommand ClickSeeFeedsButtonCommand { get; }
        //public IAsyncRelayCommand ClickRecentUserNotificationListViewItemCommand { get; }
        #endregion

        private void InitializeFolderCardItems()
        {
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Issues.png")),
                Text = "Issues",
                Tag = "issues",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/PullRequests.png")),
                Text = "Pull Requests",
                Tag = "pullrequests",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Discussions.png")),
                Text = "Discussions",
                Tag = "discussions",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Repositories.png")),
                Text = "Repositories",
                Tag = "repositories",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Organizations.png")),
                Text = "Organizations",
                Tag = "organizations",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Starred.png")),
                Text = "Stars",
                Tag = "stars",
            });
        }

        private async Task LoadUserHomePageAsync()
        {
            _messenger?.Send(new LoadingMessaging(true));
            string _currentTaskingMethodName = nameof(LoadUserHomePageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadHomeContentsAsync);
                await LoadHomeContentsAsync();
            }
            catch (Exception ex)
            {
                TaskException = ex;

                _logger?.Error(_currentTaskingMethodName, ex);
                throw;
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new LoadingMessaging(false));
            }
        }

        private async Task LoadHomeContentsAsync()
        {
            RepositoryQueries repositoryQueries = new();
            var repositoryResponse = await repositoryQueries.GetAllAsync(App.Settings.SignedInUserName);

            foreach (var item in repositoryResponse)
                _userRepositories.Add(item);

            NotificationQueries notificationQueries = new();
            var notificationResponse = await notificationQueries.GetAllAsync(
                new() { All = true },
                new()
                {
                    PageCount = 1, // Constant
                    PageSize = 10,
                    StartPage = 1,
                });

            foreach (var item in notificationResponse)
                _userNotifications.Add(item);
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Home";
            currentItem.Description = $"Home";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Home.png"))
            };
        }
    }
}
