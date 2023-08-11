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
using Microsoft.UI.Xaml;

namespace FluentHub.App.ViewModels.Users
{
	public class StarredReposViewModel : BaseViewModel
	{
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private bool _displayTitle;
		public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public IAsyncRelayCommand LoadUserStarredRepositoriesPageCommand { get; }

		public StarredReposViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				DisplayTitle = true;
			}

			_repositories = new();
			Repositories = new(_repositories);

			LoadUserStarredRepositoriesPageCommand = new AsyncRelayCommand(LoadUserStarredRepositoriesPageAsync);
		}

		private async Task LoadUserStarredRepositoriesPageAsync()
		{
			SetTabInformation("Stars", "Stars", "Starred");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadUserStarredRepositoriesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserStarredRepositoriesAsync);
				await LoadUserStarredRepositoriesAsync(Login);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				SetTabInformation("Stars", "Stars", "Starred");

				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadUserStarredRepositoriesAsync(string login)
		{
			StarredRepoQueries queries = new();
			var response = await queries.GetAllAsync(login);

			_repositories.Clear();
			foreach (var item in response)
			{
				RepoBlockButtonViewModel viewModel = new()
				{
					Repository = item,
					DisplayDetails = true,
					DisplayStarButton = true,
				};

				_repositories.Add(viewModel);
			}
		}

		public async Task LoadUserAsync(string login)
		{
			UserQueries queries = new();
			var response = await queries.GetAsync(login);

			User = response ?? new();

			UserProfileOverviewViewModel = new()
			{
				User = User,
				SelectedTag = "stars",
			};

			if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
			{
				UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
			}
		}
	}
}
