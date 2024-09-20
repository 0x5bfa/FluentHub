using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.Octokit.Queries.Users;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;

namespace FluentHub.App.ViewModels
{
	public class MainPageViewModel : ObservableObject
	{
		private readonly Microsoft.UI.Dispatching.DispatcherQueue _dispatcher;
		private readonly INavigationService _navigationService;
		private readonly IMessenger _messenger;
		private readonly ToastService _toastService;
		private readonly ILogger _logger;

		private UserNotificationMessage _lastNotification;
		public UserNotificationMessage LastNotification { get => _lastNotification; private set => SetProperty(ref _lastNotification, value); }

		private Octokit.Models.v4.User _signedInUser;
		public Octokit.Models.v4.User SignedInUser { get => _signedInUser; private set => SetProperty(ref _signedInUser, value); }

		private bool _taskIsInProgress;
		public bool TaskIsInProgress { get => _taskIsInProgress; private set => SetProperty(ref _taskIsInProgress, value); }

		private bool _taskIsCompletedSuccessfully;
		public bool TaskIsCompletedSuccessfully { get => _taskIsCompletedSuccessfully; private set => SetProperty(ref _taskIsCompletedSuccessfully, value); }

		private int _unreadCount;
		public int UnreadCount { get => _unreadCount; private set => SetProperty(ref _unreadCount, value); }

		private string _searchTerm;
		public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

		public bool FailedToLoadUserAvatar { get; private set; }

		private readonly ObservableCollection<SearchQueryModel> _autoSuggestionItems;
		public ReadOnlyObservableCollection<SearchQueryModel> AutoSuggestionItems;

		private readonly ObservableCollection<SquareNavigationViewItemModel> _navViewItems;
		public ReadOnlyObservableCollection<SquareNavigationViewItemModel> NavViewItems;

		private readonly ObservableCollection<Repository> _repositories;
		public ReadOnlyObservableCollection<Repository> Repositories { get; }

		public ICommand AddNewTabAcceleratorCommand { get; private set; }
		public ICommand CloseTabAcceleratorCommand { get; private set; }
		public ICommand GoToNextTabAcceleratorCommand { get; private set; }
		public ICommand GoToPreviousTabAcceleratorCommand { get; private set; }
		public ICommand AddNewTabWithMouseAcceleratorCommand { get; private set; }
		public ICommand CloseTabWithMouseAcceleratorCommand { get; private set; }

		public ICommand GoBackCommand { get; private set; }
		public ICommand GoForwardCommand { get; private set; }
		public ICommand ReloadCommand { get; private set; }

		public ICommand GoHomeCommand { get; private set; }
		public ICommand GoNotificationsCommand { get; private set; }
		public ICommand GoActivitiesCommand { get; private set; }
		public ICommand GoExplorerCommand { get; private set; }
		public ICommand GoMarketplaceCommand { get; private set; }
		public ICommand GoProfileCommand { get; private set; }

		public IAsyncRelayCommand LoadSignedInUserCommand { get; }

		public MainPageViewModel(INavigationService navigationService, IMessenger notificationMessenger = null, ToastService toastService = null, ILogger logger = null)
		{
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
			AddNewTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabWithMouseAccelerator);
			CloseTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabWithMouseAccelerator);

			GoBackCommand = new RelayCommand(GoBack);
			GoForwardCommand = new RelayCommand(GoForward);
			ReloadCommand = new RelayCommand(Reload);

			LoadSignedInUserCommand = new AsyncRelayCommand(LoadSignedInUserAsync);
		}

		#region Command methods
		private void AddNewTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			_navigationService.OpenTab<Views.Viewers.DashBoardPage>();
			e.Handled = true;
		}

		private void CloseTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			_navigationService.CloseTab(_navigationService.TabView.SelectedItem.Guid);
			e.Handled = true;
		}

		private void GoToNextTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			if (_navigationService.TabView.SelectedIndex == _navigationService.TabView.TabItems.Count - 1)
			{
				_navigationService.TabView.SelectedIndex = 0;
			}
			else
			{
				_navigationService.TabView.SelectedIndex++;
			}

			e.Handled = true;
		}

		private void GoToPreviousTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			if (_navigationService.TabView.SelectedIndex == _navigationService.TabView.TabItems.Count - 1)
			{
				_navigationService.TabView.SelectedIndex = 0;
			}
			else
			{
				_navigationService.TabView.SelectedIndex--;
			}

			e.Handled = true;
		}

		private void AddNewTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			_navigationService.OpenTab<Views.Viewers.DashBoardPage>();
			e.Handled = true;
		}

		private void CloseTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		{
			_navigationService.CloseTab(_navigationService.TabView.SelectedItem.Guid);
			e.Handled = true;
		}

		private void GoBack()
		{
			_navigationService.GoBack();
		}

		private void GoForward()
		{
			_navigationService.GoForward();
		}

		private void Reload()
		{
			_navigationService.Reload();
		}

		private async Task LoadSignedInUserAsync()
		{
			string _currentTaskingMethodName = nameof(LoadSignedInUserAsync);

			try
			{
				UserQueries queries = new();
				var user = await queries.GetAsync(App.AppSettings.SignedInUserName);

				SignedInUser = user ?? new();

				NotificationQueries notificationQueries = new();
				var count = await notificationQueries.GetUnreadCount();

				UnreadCount = count;
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
			RepositoryQueries queries = new();

			var result = await queries.GetAllAsync(App.AppSettings.SignedInUserName, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			var items = (List<Repository>)result.Response;

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
