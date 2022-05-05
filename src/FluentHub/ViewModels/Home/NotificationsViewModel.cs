using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Octokit;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Home
{
    public class NotificationsViewModel : ObservableObject
    {
        #region constructor
        public NotificationsViewModel(ToastService toastService,
                                      IMessenger messenger = null,
                                      ILogger logger = null)
        {
            _toastService = toastService;
            _messenger = messenger;
            _logger = logger;
            _notifications = new();
            _unreadCount = 0;
            NotificationItems = new(_notifications);

            RefreshNotificationsCommand = new AsyncRelayCommand(RefreshNotificationsAsync);
        }
        #endregion

        #region properties
        private readonly ToastService _toastService;
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private int _unreadCount;
        public int UnreadCount { get => _unreadCount; set => SetProperty(ref _unreadCount, value); }

        private readonly ObservableCollection<NotificationButtonBlockViewModel> _notifications;
        public ReadOnlyObservableCollection<NotificationButtonBlockViewModel> NotificationItems { get; }

        public IAsyncRelayCommand RefreshNotificationsCommand { get; }
        #endregion

        #region methods
        private async Task RefreshNotificationsAsync(CancellationToken token)
        {
            try
            {
                NotificationQueries queries = new();
                var items = await queries.GetAllAsync();

                _notifications.Clear();
                foreach (var item in items)
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
                _logger?.Error("RefreshNotificationsAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
            }
        }
        #endregion
    }
}
