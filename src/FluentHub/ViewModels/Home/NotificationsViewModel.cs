using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
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
        public NotificationsViewModel(IGitHubClient client!!,
                                      ToastService toastService,
                                      IMessenger messenger = null,
                                      ILogger logger = null)
        {
            _client = client;
            _toastService = toastService;
            _messenger = messenger;
            _logger = logger;
            _notifications = new();
            _unreadCount = 0;
            NotificationItems = new(_notifications);

            RefreshNotificationsCommand = new AsyncRelayCommand(RefreshNotificationsAsync);
        }
        #endregion

        #region fields
        private readonly IGitHubClient _client;
        private readonly ToastService _toastService;
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<NotificationButtonBlockViewModel> _notifications;
        private int _unreadCount;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<NotificationButtonBlockViewModel> NotificationItems { get; }
        public IAsyncRelayCommand RefreshNotificationsCommand { get; }
        public int UnreadCount
        {
            get => _unreadCount;
            set => SetProperty(ref _unreadCount, value);
        }
        #endregion

        #region methods
        private async Task RefreshNotificationsAsync(CancellationToken token)
        {
            try
            {
                NotificationsRequest request = new()
                {
                    All = true
                };
                ApiOptions requestOptions = new()
                {
                    PageCount = 1,
                    PageSize = 50,
                    StartPage = 1
                };

                var requestResult = await _client.Activity.Notifications.GetAllForCurrent(request, requestOptions);

                _notifications.Clear();
                UnreadCount = 0;
                foreach (var item in requestResult)
                {
                    NotificationButtonBlockViewModel viewModel = new()
                    {
                        NotificationItem = item,
                        UpdatedAtHumanized = DateTimeOffset.Parse(item.UpdatedAt).Humanize(),
                        NameWithOwner = item.Repository.Owner.Login + "/" + item.Repository.Name
                    };

                    _notifications.Add(viewModel);
                    if (item.Unread)
                    {
                        UnreadCount++;
                    }
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
                _toastService.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
            }
        }
        #endregion
    }
}
