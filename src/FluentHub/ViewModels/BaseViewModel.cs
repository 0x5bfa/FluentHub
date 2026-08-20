using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels
{
	public abstract class BaseViewModel : ObservableObject
	{
		protected readonly IMessenger _messenger;
		protected readonly ILogger _logger;
		protected readonly INavigationService _navigation;
		protected readonly IFluentHubGitHubClient _gitHub;

		// Provided for v3 API response
		protected int _loadedItemCount = 0;
		protected int _loadedPageCount = 0;
		protected bool _loadedToTheEnd = false;
		protected const int _itemCountPerPage = 30;

		// Provided for v4 API response
		protected PageInfo _lastPageInfo = default!;

		protected string _currentTaskingMethodName = default!;

		protected ITabViewItem SelectedTabViewItem
			=> _navigation.TabView.SelectedItem;

		protected string _login = default!;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		protected string _name = default!;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		protected int _number;
		public int Number { get => _number; set => SetProperty(ref _number, value); }

		private User _user = default!;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private Repository _repository = default!;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel = default!;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private Organization _organization = default!;
		public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

		private OrganizationProfileOverviewViewModel _organizationProfileOverviewViewModel = default!;
		public OrganizationProfileOverviewViewModel OrganizationProfileOverviewViewModel { get => _organizationProfileOverviewViewModel; set => SetProperty(ref _organizationProfileOverviewViewModel, value); }

		private Exception _taskException = default!;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		protected bool _IsTaskFaulted;
		public bool IsTaskFaulted { get => _IsTaskFaulted; set => SetProperty(ref _IsTaskFaulted, value); }

		protected bool _IsTaskLoading;
		public bool IsTaskLoading { get => _IsTaskLoading; set => SetProperty(ref _IsTaskLoading, value); }

		protected bool _IsEmpty;
		public bool IsEmpty { get => _IsEmpty; set => SetProperty(ref _IsEmpty, value); }

		protected BaseViewModel(IFluentHubGitHubClient gitHub)
		{
			_gitHub = gitHub;

			// Dependency Injection
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();

			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText ?? string.Empty;
			Name = parameter.SecondaryText ?? string.Empty;
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
			var queries = _gitHub.Users.Users;
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
			_lastPageInfo = default!;
		}
	}
}
