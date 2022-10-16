using CommunityToolkit.WinUI.UI;
using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Home
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
                Text = "Issues".GetLocalizedResource(),
                Tag = "issues",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/PullRequests.png")),
                Text = "PullRequests".GetLocalizedResource(),
                Tag = "pullrequests",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Discussions.png")),
                Text = "Discussions".GetLocalizedResource(),
                Tag = "discussions",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Repositories.png")),
                Text = "Repositories".GetLocalizedResource(),
                Tag = "repositories",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Organizations.png")),
                Text = "Organizations".GetLocalizedResource(),
                Tag = "organizations",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Starred.png")),
                Text = "Stars".GetLocalizedResource(),
                Tag = "stars",
            });
        }

        private async Task LoadUserHomePageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserHomePageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadHomeContentsAsync);
                await LoadHomeContentsAsync();
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

        private async Task LoadHomeContentsAsync()
        {
            RepositoryQueries repositoryQueries = new();
            var repositoryResponse = await repositoryQueries.GetAllAsync(App.AppSettings.SignedInUserName);

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
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Home.png"))
            };
        }
    }
}
