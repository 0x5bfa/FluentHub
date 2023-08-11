// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Viewers
{
	public class NotificationsViewModel : BaseViewModel
	{
		private readonly ToastService _toastService;

		private int _loadedItemCount = 0;
		private int _loadedPageCount = 0;
		private bool _loadedToTheEnd = false;
		private const int _itemCountPerPage = 30;

		private int _unreadCount;
		public int UnreadCount { get => _unreadCount; set => SetProperty(ref _unreadCount, value); }

		private readonly ObservableCollection<NotificationBlockButtonViewModel> _notifications;
		public ReadOnlyObservableCollection<NotificationBlockButtonViewModel> NotificationItems { get; }

		public IAsyncRelayCommand LoadUserNotificationsPageCommand { get; }
		public IAsyncRelayCommand LoadFurtherUserNotificationsPageCommand { get; }

		public NotificationsViewModel()
		{
			_toastService = Ioc.Default.GetRequiredService<ToastService>();

			_notifications = new();
			NotificationItems = new(_notifications);

			_unreadCount = 0;

			LoadUserNotificationsPageCommand = new AsyncRelayCommand(LoadUserNotificationsPageAsync);
			LoadFurtherUserNotificationsPageCommand = new AsyncRelayCommand(LoadFurtherNotificationsAsync);
		}

		private async Task LoadUserNotificationsPageAsync()
		{
			SetTabInformation("Notifications", "Notifications", "Notifications");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadUserNotificationsPageAsync);

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

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				_toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
				if (_messenger != null)
				{
					UserNotificationMessage notification = new("NotificationCount", UnreadCount.ToString(), UserNotificationType.Info);
					_messenger.Send(notification);
				}

				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
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
	}
}
