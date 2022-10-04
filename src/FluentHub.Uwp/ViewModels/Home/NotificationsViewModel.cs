using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Home
{
    public class NotificationsViewModel : ObservableObject
    {
        public NotificationsViewModel(ToastService toastService, IMessenger messenger = null, ILogger logger = null)
        {
            _toastService = toastService;
            _messenger = messenger;
            _logger = logger;

            _notifications = new();
            NotificationItems = new(_notifications);

            _unreadCount = 0;

            LoadUserNotificationsPageCommand = new AsyncRelayCommand(LoadUserNotificationsPageAsync);
            LoadFurtherUserNotificationsPageCommand = new AsyncRelayCommand(LoadFurtherNotificationsAsync);
        }

        #region Fields and Poperties
        private readonly ToastService _toastService;
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private int _unreadCount;
        public int UnreadCount { get => _unreadCount; set => SetProperty(ref _unreadCount, value); }

        private int _loadedItemCount = 0;
        private int _loadedPageCount = 0;
        private bool _loadedToTheEnd = false;
        private const int _itemCountPerPage = 30;

        private readonly ObservableCollection<NotificationBlockButtonViewModel> _notifications;
        public ReadOnlyObservableCollection<NotificationBlockButtonViewModel> NotificationItems { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserNotificationsPageCommand { get; }
        public IAsyncRelayCommand LoadFurtherUserNotificationsPageCommand { get; }
        #endregion

        private async Task LoadUserNotificationsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserNotificationsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadNotificationsAsync);
                await LoadNotificationsAsync();
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

                _toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("NotificationCount", UnreadCount.ToString(), UserNotificationType.Info);
                    _messenger.Send(notification);
                }

                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadNotificationsAsync()
        {
            if (_loadedToTheEnd) return;

            NotificationQueries queries = new();
            var response = await queries.GetAllAsync(
                new() { All = true },
                new()
                {
                    PageCount = 1, // Constant
                    PageSize = _itemCountPerPage,
                    StartPage = _loadedPageCount + 1,
                });

            _loadedItemCount += response.Count();
            _loadedPageCount++;
            if (response.Count() < _itemCountPerPage)
                _loadedToTheEnd = true;

            _notifications.Clear();
            foreach (var item in response)
            {
                NotificationBlockButtonViewModel viewmodel = new()
                {
                    Item = item,
                };

                if (item.Unread) UnreadCount++;
                _notifications.Add(viewmodel);
            }
        }

        public async Task LoadFurtherNotificationsAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            try
            {
                if (_loadedToTheEnd) return;

                NotificationQueries queries = new();
                var response = await queries.GetAllAsync(
                    new() { All = true },
                    new()
                    {
                        PageCount = 1, // Constant
                        PageSize = _itemCountPerPage,
                        StartPage = _loadedPageCount + 1,
                    });

                _loadedItemCount += response.Count();
                _loadedPageCount++;
                if (response.Count() < _itemCountPerPage)
                    _loadedToTheEnd = true;

                foreach (var item in response)
                {
                    NotificationBlockButtonViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    if (item.Unread) UnreadCount++;
                    _notifications.Add(viewmodel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                TaskException = ex;
                faulted = true;

                _logger?.Error(nameof(LoadFurtherNotificationsAsync), ex);
                throw;
            }
            finally
            {
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Notifications";
            currentItem.Description = "Notifications";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Notifications.png"))
            };
        }
    }
}
