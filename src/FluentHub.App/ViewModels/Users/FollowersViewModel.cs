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
	public class FollowersViewModel : BaseViewModel
	{
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private bool _displayTitle;
		public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _followersItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowersItems { get; }

		public IAsyncRelayCommand LoadUserFollowersPageCommand { get; }

		public FollowersViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				DisplayTitle = true;
			}

			_followersItems = new();
			FollowersItems = new(_followersItems);

			LoadUserFollowersPageCommand = new AsyncRelayCommand(LoadUserFollowersPageAsync);
		}

		private async Task LoadUserFollowersPageAsync()
		{
			SetTabInformation("Followers", "Followers", "Accounts");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadUserFollowersPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserFollowersAsync);
				await LoadUserFollowersAsync(Login);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				SetTabInformation("Followers", "Followers", "Accounts");

				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadUserFollowersAsync(string login)
		{
			FollowersQueries queries = new();
			var response = await queries.GetAllAsync(login);

			_followersItems.Clear();
			foreach (var item in response)
			{
				UserBlockButtonViewModel viewModel = new()
				{
					User = item,
				};

				_followersItems.Add(viewModel);
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
				SelectedTag = "followers",
			};

			if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
			{
				UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
			}
		}
	}
}
