// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Users
{
	public class ProjectsViewModel : ObservableObject
	{
		private readonly IMessenger _messenger;
		private readonly ILogger _logger;
		private readonly INavigationService _navigation;

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private bool _displayTitle;
		public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _projects;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Projects { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadUserProjectsPageCommand { get; }

		public ProjectsViewModel()
		{
			// Dependency Injection
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();

			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				DisplayTitle = true;
			}

			_projects = new();
			Projects = new(_projects);

			LoadUserProjectsPageCommand = new AsyncRelayCommand(LoadUserProjectsPageAsync);
		}

		private async Task LoadUserProjectsPageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadUserProjectsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserProjectsAsync);
				await LoadUserProjectsAsync(Login);
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
				_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadUserProjectsAsync(string login)
		{
			ProjectQueries queries = new();
			var items = await queries.GetAllAsync(login);
			if (items == null) return;

			_projects.Clear();
			foreach (var item in items)
			{
				ProjectBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_projects.Add(viewModel);
			}
		}

		private async Task LoadUserAsync(string login)
		{
			UserQueries queries = new();
			var response = await queries.GetAsync(login);

			User = response ?? new();

			UserProfileOverviewViewModel = new()
			{
				User = User,
				SelectedTag = "projects",
			};

			if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
			{
				UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
			}
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Projects";
			currentItem.Description = "Projects";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
			};
		}
	}
}
