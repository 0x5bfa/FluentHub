using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels
{
	public abstract class BaseViewModel : ObservableObject
	{
		protected readonly IMessenger _messenger;
		protected readonly ILogger _logger;
		protected readonly INavigationService _navigation;

		// Provided for v3 API response
		protected int _loadedItemCount = 0;
		protected int _loadedPageCount = 0;
		protected bool _loadedToTheEnd = false;
		protected const int _itemCountPerPage = 30;

		// Provided for v4 API response
		protected PageInfo _lastPageInfo;

		protected string _currentTaskingMethodName;

		protected ITabViewItem SelectedTabViewItem
			=> _navigation.TabView.SelectedItem;

		protected string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		protected string _name;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		protected int _number;
		public int Number { get => _number; set => SetProperty(ref _number, value); }

		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private Organization _organization;
		public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

		private OrganizationProfileOverviewViewModel _organizationProfileOverviewViewModel;
		public OrganizationProfileOverviewViewModel OrganizationProfileOverviewViewModel { get => _organizationProfileOverviewViewModel; set => SetProperty(ref _organizationProfileOverviewViewModel, value); }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		protected bool _IsTaskFaulted;
		public bool IsTaskFaulted { get => _IsTaskFaulted; set => SetProperty(ref _IsTaskFaulted, value); }

		protected bool _IsTaskLoading;
		public bool IsTaskLoading { get => _IsTaskLoading; set => SetProperty(ref _IsTaskLoading, value); }

		protected bool _IsEmpty;
		public bool IsEmpty { get => _IsEmpty; set => SetProperty(ref _IsEmpty, value); }

		protected BaseViewModel()
		{
			// Dependency Injection
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();

			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;
			Number = parameter.Number;
		}

		protected void SetTabInformation(
			string? header = null,
			string? description = null,
			string? imageIconSourceSimplified = null)
		{
			var currentItem = _navigation.TabView.SelectedItem.NavigationHistory.CurrentItem;
			if (currentItem is null)
				return;

			if (!string.IsNullOrEmpty(header))
				currentItem.Header = header;

			if (!string.IsNullOrEmpty(description))
				currentItem.Description = description;

			if (!string.IsNullOrEmpty(imageIconSourceSimplified))
			{
				currentItem.Icon = new ImageIconSource()
				{
					ImageSource = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{imageIconSourceSimplified}.png"))
				};
			}
		}

		protected void SetLoadingProgress(bool isStarted)
		{
			if (isStarted)
			{
				IsTaskFaulted = false;
				IsTaskLoading = true;
				_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
				_navigation.TabView.SelectedItem.NavigationHistory.CanReload = false;
			}
			else
			{
				IsTaskLoading = false;
				_navigation.TabView.SelectedItem.NavigationHistory.CanReload = true;

				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));

				if (IsTaskFaulted)
				{
					_logger?.Error(_currentTaskingMethodName, TaskException);
				}
			}
		}

		protected async Task LoadUserAsync(string login)
		{
			UserQueries queries = new();
			var response = await queries.GetAsync(login);

			User = response ?? new();

			var userProfileOverviewViewModel = new UserProfileOverviewViewModel()
			{
				User = User,
				SelectedTag = "discussions"
			};

			if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
			{
				userProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
			}

			UserProfileOverviewViewModel = userProfileOverviewViewModel;
		}

		protected void InitializeNodePagingInfo()
		{
			_loadedItemCount = 0;
			_loadedPageCount = 0;
			_loadedToTheEnd = false;
			_lastPageInfo = null;
		}
	}
}
