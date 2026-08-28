using CommunityToolkit.WinUI;
using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Helpers;
using FluentHub.Features.Repositories.ViewModels;
using FluentHub.Shared.Controls.ViewModels.Overview;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using FluentHub.Services;
using FluentHub.Models;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;
using Windows.System;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shell.ViewModels
{
	public class MainPageViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		private readonly Microsoft.UI.Dispatching.DispatcherQueue _dispatcher;
		private readonly INavigationService _navigationService;
		private readonly IMessenger? _messenger = default!;
		private readonly ToastService? _toastService;
		private readonly ILogger? _logger;

		private UserNotificationMessage _lastNotification = default!;
		public UserNotificationMessage LastNotification { get => _lastNotification; private set => SetProperty(ref _lastNotification, value); }

		private FluentHub.Core.Application.Models.User _signedInUser = default!;
		public FluentHub.Core.Application.Models.User SignedInUser { get => _signedInUser; private set => SetProperty(ref _signedInUser, value); }

		private bool _taskIsInProgress;
		public bool TaskIsInProgress { get => _taskIsInProgress; private set => SetProperty(ref _taskIsInProgress, value); }

		private bool _taskIsCompletedSuccessfully;
		public bool TaskIsCompletedSuccessfully { get => _taskIsCompletedSuccessfully; private set => SetProperty(ref _taskIsCompletedSuccessfully, value); }

		private int _unreadCount;
		public int UnreadCount { get => _unreadCount; private set => SetProperty(ref _unreadCount, value); }

		private string _searchTerm = default!;
		public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

		public bool FailedToLoadUserAvatar { get; private set; }

		private readonly ObservableCollection<SearchQueryModel> _autoSuggestionItems;
		public ReadOnlyObservableCollection<SearchQueryModel> AutoSuggestionItems;

		private readonly ObservableCollection<Repository> _repositories;
		public ReadOnlyObservableCollection<Repository> Repositories { get; }

		public ICommand AddNewTabAcceleratorCommand { get; }
		public ICommand CloseTabAcceleratorCommand { get; }
		public ICommand GoToNextTabAcceleratorCommand { get; }
		public ICommand GoToPreviousTabAcceleratorCommand { get; }

		private readonly RelayCommand _goBackCommand;
		private readonly RelayCommand _goForwardCommand;
		private readonly RelayCommand _reloadCommand;
		public ICommand GoBackCommand => _goBackCommand;
		public ICommand GoForwardCommand => _goForwardCommand;
		public ICommand ReloadCommand => _reloadCommand;

		public ICommand GoHomeCommand { get; private set; } = default!;
		public ICommand GoNotificationsCommand { get; private set; } = default!;
		public ICommand GoActivitiesCommand { get; private set; } = default!;
		public ICommand GoExplorerCommand { get; private set; } = default!;
		public ICommand GoMarketplaceCommand { get; private set; } = default!;
		public ICommand GoProfileCommand { get; private set; } = default!;

		public IAsyncRelayCommand LoadSignedInUserCommand { get; }

		public MainPageViewModel(IFluentHubGitHubClient gitHub, INavigationService navigationService, IMessenger? notificationMessenger = null, ToastService? toastService = null, ILogger? logger = null)
		{
			_gitHub = gitHub;
			// To Access the UI thread later.
			_dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

			_navigationService = navigationService;
			_messenger = notificationMessenger;
			_toastService = toastService;
			_logger = logger;

			if (_messenger != null)
			{
				_messenger.Register<UserNotificationMessage>(this, OnNewNotificationReceived);
				_messenger.Register<TaskStateMessaging>(this, OnIfContentIsLoadingRecieved);
			}

			_autoSuggestionItems = new();
			AutoSuggestionItems = new(_autoSuggestionItems);

			_repositories = new();
			Repositories = new(_repositories);

			AddNewTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabAccelerator);
			CloseTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabAccelerator);
			GoToNextTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToNextTabAccelerator);
			GoToPreviousTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToPreviousTabAccelerator);
			_goBackCommand = new RelayCommand(GoBack, () => _navigationService.CanGoBack);
			_goForwardCommand = new RelayCommand(GoForward, () => _navigationService.CanGoForward);
			_reloadCommand = new RelayCommand(Reload, () => _navigationService.CanReload);
			_navigationService.NavigationStateChanged += OnNavigationStateChanged;

			LoadSignedInUserCommand = new AsyncRelayCommand(LoadSignedInUserAsync);
		}

		#region Command methods
		private void AddNewTabAccelerator(KeyboardAcceleratorInvokedEventArgs? e)
		{
			if (e is null)
				return;

			_ = _navigationService.OpenTabAsync(new DashboardRoute());
			e.Handled = true;
		}

		private void CloseTabAccelerator(KeyboardAcceleratorInvokedEventArgs? e)
		{
			if (e is null)
				return;

			if (_navigationService.TabView.SelectedItem is { } selectedTab)
				_ = _navigationService.CloseTabAsync(selectedTab.Id);
			e.Handled = true;
		}

		private void GoToNextTabAccelerator(KeyboardAcceleratorInvokedEventArgs? e)
		{
			if (e is null)
				return;

			var tabCount = _navigationService.TabView.TabItems.Count;
			if (tabCount == 0)
				return;

			_navigationService.TabView.SelectedIndex =
				(_navigationService.TabView.SelectedIndex + 1) % tabCount;

			e.Handled = true;
		}

		private void GoToPreviousTabAccelerator(KeyboardAcceleratorInvokedEventArgs? e)
		{
			if (e is null)
				return;

			var tabCount = _navigationService.TabView.TabItems.Count;
			if (tabCount == 0)
				return;

			var selectedIndex = _navigationService.TabView.SelectedIndex;
			_navigationService.TabView.SelectedIndex = selectedIndex <= 0 ? tabCount - 1 : selectedIndex - 1;

			e.Handled = true;
		}

		private void GoBack()
		{
			_ = _navigationService.GoBackAsync();
		}

		private void GoForward()
		{
			_ = _navigationService.GoForwardAsync();
		}

		private void Reload()
		{
			_ = _navigationService.ReloadAsync();
		}

		private void OnNavigationStateChanged(object? sender, System.EventArgs args)
		{
			_goBackCommand.NotifyCanExecuteChanged();
			_goForwardCommand.NotifyCanExecuteChanged();
			_reloadCommand.NotifyCanExecuteChanged();
		}

		private async Task LoadSignedInUserAsync()
		{
			string _currentTaskingMethodName = nameof(LoadSignedInUserAsync);

			try
			{
				var userTask = _gitHub.Users.Users.GetAsync(App.AppSettings.SignedInUserName);
				var unreadCountTask = _gitHub.Users.Notifications.GetUnreadCountAsync();
				await Task.WhenAll(userTask, unreadCountTask);

				var user = await userTask;
				SignedInUser = user ?? new();

				UnreadCount = await unreadCountTask;
				_toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);

				FailedToLoadUserAvatar = false;
			}
			catch (Exception ex)
			{
				_logger?.Error(_currentTaskingMethodName, ex);
				FailedToLoadUserAvatar = true;
			}
			finally
			{
			}
		}

		private async Task LoadUserRepositoriesAsync()
		{
			var queries = _gitHub.Users.Repositories;

			var result = await queries.GetPageAsync(App.AppSettings.SignedInUserName, PageRequest.Forward(20));

			var items = result.Items;

			_repositories.Clear();
			foreach (var item in items)
				_repositories.Add(item);
		}
		#endregion

		public void ClearSearchQueryModelItems()
			=> _autoSuggestionItems.Clear();

		public void AddNewSearchQueryModel(string query, string label)
			=> _autoSuggestionItems.Add(new(query, label));

		private void OnNewNotificationReceived(object recipient, UserNotificationMessage message)
		{
			// Check if the message method contains the InApp value (multivalue enum)
			if (message.Method.HasFlag(UserNotificationMethod.InApp))
			{
				// Thrown by Home.NotificationsViewModel
				if (message.Title == "NotificationCount")
				{
					UnreadCount = Convert.ToInt32(message.Message);
					return;
				}

				// [Obsolete]
				//// Show the message in the UI
				//// using the dispatcher to access the UI thread
				//_dispatcher.TryEnqueue(() => LastNotification = message);

				//// Show the message in the app
				//_logger?.Info("InApp notification received: {0}", message);
			}

			// Check if the message method contains the Toast value (multivalue enum)
			if (message.Method.HasFlag(UserNotificationMethod.Toast))
			{
				_toastService?.ShowToastNotification(message.Title, message.Message);
				// Show the message in the toast
				_logger?.Info("Toast notification received: {0}", message);
			}
		}

		private void OnIfContentIsLoadingRecieved(object recipient, TaskStateMessaging message)
		{
			switch (message.StatusType)
			{
				case TaskStatusType.IsStarted:
					TaskIsInProgress = true;
					break;

				case TaskStatusType.IsCompleted:
					TaskIsInProgress = false;
					break;

				case TaskStatusType.IsCompletedSuccessfully:
					TaskIsCompletedSuccessfully = true;
					TaskIsInProgress = false;
					break;

				case TaskStatusType.IsFaulted:
					TaskIsCompletedSuccessfully = false;
					TaskIsInProgress = false;
					break;
			}
		}
	}
}
