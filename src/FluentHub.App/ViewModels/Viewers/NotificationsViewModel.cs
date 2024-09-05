// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Viewers
{
	public class NotificationsViewModel : BaseViewModel
	{
		private readonly ToastService _toastService;

		private int _unreadCount;
		public int UnreadCount { get => _unreadCount; set => SetProperty(ref _unreadCount, value); }

		private readonly ObservableCollection<NotificationBlockButtonViewModel> _notifications;
		public ReadOnlyObservableCollection<NotificationBlockButtonViewModel> NotificationItems { get; }

		public IAsyncRelayCommand LoadUserNotificationsPageCommand { get; }
		public IAsyncRelayCommand LoadUserNotificationsFurtherCommand { get; }

		public NotificationsViewModel()
		{
			_toastService = Ioc.Default.GetRequiredService<ToastService>();

			_notifications = new();
			NotificationItems = new(_notifications);

			_unreadCount = 0;

			LoadUserNotificationsPageCommand = new AsyncRelayCommand(LoadUserNotificationsPageAsync);
			LoadUserNotificationsFurtherCommand = new AsyncRelayCommand(LoadFurtherNotificationsAsync);
		}

		private async Task LoadUserNotificationsPageAsync()
		{
			SetTabInformation("Notifications", "Notifications", "Notifications");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserNotificationsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadNotificationsAsync);
				await LoadNotificationsAsync();

				SetTabInformation("Notifications", "Notifications");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				_toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
				if (_messenger != null)
				{
					UserNotificationMessage notification = new("NotificationCount", UnreadCount.ToString(), UserNotificationType.Info);
					_messenger.Send(notification);
				}

				SetLoadingProgress(false);
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

			_loadedItemCount += response.Count;
			_loadedPageCount++;
			if (response.Count < _itemCountPerPage)
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
			SetLoadingProgress(true);

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

				_loadedItemCount += response.Count;
				_loadedPageCount++;
				if (response.Count < _itemCountPerPage)
					_loadedToTheEnd = true;

				foreach (var item in response)
				{
					NotificationBlockButtonViewModel viewmodel = new()
					{
						Item = item,
					};

					if (item.Unread)
						UnreadCount++;

					_notifications.Add(viewmodel);
				}
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}
	}
}
