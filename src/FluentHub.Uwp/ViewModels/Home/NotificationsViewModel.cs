using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

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
            _unreadCount = 0;
            NotificationItems = new(_notifications);

            RefreshNotificationsCommand = new AsyncRelayCommand(LoadNotificationsAsync);
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

        private readonly ObservableCollection<NotificationButtonBlockViewModel> _notifications;
        public ReadOnlyObservableCollection<NotificationButtonBlockViewModel> NotificationItems { get; }

        public IAsyncRelayCommand RefreshNotificationsCommand { get; }
        #endregion

        private async Task LoadNotificationsAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

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
                    NotificationButtonBlockViewModel viewmodel = new()
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
                _logger?.Error(nameof(LoadNotificationsAsync), ex);
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
                _toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("NotificationCount", UnreadCount.ToString(), UserNotificationType.Info);
                    _messenger.Send(notification);
                }
            }
        }

        public async Task LoadFurtherNotificationsAsync()
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

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
                    NotificationButtonBlockViewModel viewmodel = new()
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
                _logger?.Error(nameof(LoadFurtherNotificationsAsync), ex);
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
    }
}
