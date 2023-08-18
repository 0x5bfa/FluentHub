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
	public class PullRequestsViewModel : BaseViewModel
	{
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private bool _displayTitle;
		public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public IAsyncRelayCommand LoadUserPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadFurtherUserPullRequestsPageCommand { get; }

		public PullRequestsViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				DisplayTitle = true;
			}

			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadUserPullRequestsPageCommand = new AsyncRelayCommand(LoadUserPullRequestsPageAsync);
			LoadFurtherUserPullRequestsPageCommand = new AsyncRelayCommand(LoadFurtherUserPullRequestsPageAsync);
		}

		private async Task LoadUserPullRequestsPageAsync()
		{
			SetTabInformation("Pull Requests", "Pull Requests", "PullRequests");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			_currentTaskingMethodName = nameof(LoadUserPullRequestsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserPullRequestsAsync);
				await LoadUserPullRequestsAsync(Login);

				SetTabInformation("Pull Requests", "Pull Requests", "PullRequests");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadUserPullRequestsAsync(string login)
		{
			PullRequestQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<PullRequest>)result.Response;

			_pullRequests.Clear();
			foreach (var item in items)
			{
				PullBlockButtonViewModel viewModel = new()
				{
					PullItem = item,
				};

				_pullRequests.Add(viewModel);
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
			};

			if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
			{
				UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
			}
		}

		private async Task LoadFurtherUserPullRequestsPageAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			try
			{
				PullRequestQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<PullRequest>)result.Response;

				foreach (var item in items)
				{
					PullBlockButtonViewModel viewModel = new()
					{
						PullItem = item,
					};

					_pullRequests.Add(viewModel);
				}
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(nameof(LoadFurtherUserPullRequestsPageAsync), ex);
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}
	}
}
