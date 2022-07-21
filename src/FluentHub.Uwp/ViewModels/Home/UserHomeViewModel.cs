using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.Uwp.ViewModels.Home
{
    public class UserHomeViewModel : ObservableObject
    {
        #region constructor
        public UserHomeViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _userRepositories = new();
            UserRepositories = new(_userRepositories);

            _userNotifications = new();
            UserNotifications = new(_userNotifications);

            _folderCardItems = new();
            FolderCardItems = new(_folderCardItems);

            InitializeFolderCardItems();

            LoadHomeContentsCommand = new AsyncRelayCommand(LoadHomeContentsAsync);
        }
        #endregion

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<Repository> _userRepositories;
        public ReadOnlyObservableCollection<Repository> UserRepositories { get; }

        private readonly ObservableCollection<Notification> _userNotifications;
        public ReadOnlyObservableCollection<Notification> UserNotifications { get; }

        private readonly ObservableCollection<FolderCardItem> _folderCardItems;
        public ReadOnlyObservableCollection<FolderCardItem> FolderCardItems { get; }

        public IAsyncRelayCommand LoadHomeContentsCommand { get; }
        #endregion

        private void InitializeFolderCardItems()
        {
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Issues.png")),
                Text = "Issues",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/PullRequests.png")),
                Text = "Pull Requests",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Discussions.png")),
                Text = "Discussions",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Repositories.png")),
                Text = "Repositories",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Organizations.png")),
                Text = "Organizations",
            });
            _folderCardItems.Add(new()
            {
                Thumbnail = new(new Uri("ms-appx:///Assets/Icons/Starred.png")),
                Text = "Starred",
            });
        }

        private async Task LoadHomeContentsAsync(CancellationToken token)
        {
            try
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
                        PageSize = 3,
                        StartPage = 1,
                    });

                foreach (var item in notificationResponse)
                    _userNotifications.Add(item);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadHomeContentsAsync), ex);
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
