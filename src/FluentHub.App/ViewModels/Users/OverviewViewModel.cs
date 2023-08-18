// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Models;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Users
{
	public class OverviewViewModel : BaseViewModel
	{
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		private UserProfileOverviewViewModel _userProfileOverviewViewModel;
		public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnedRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnedRepositories { get; }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnableRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnableRepositories { get; }

		private RepoContextViewModel _contextViewModel;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		public IAsyncRelayCommand LoadUserOverviewCommand { get; }
		public IAsyncRelayCommand ShowPinnedRepositoriesEditorDialogCommand { get; }

		public OverviewViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;

			_pinnedRepositories = new();
			PinnedRepositories = new(_pinnedRepositories);

			_pinnableRepositories = new();
			PinnableRepositories = new(_pinnableRepositories);

			LoadUserOverviewCommand = new AsyncRelayCommand(LoadUserOverviewAsync);
			ShowPinnedRepositoriesEditorDialogCommand = new AsyncRelayCommand(ShowPinnedRepositoriesEditorDialogAsync);
		}

		private async Task LoadUserOverviewAsync()
		{
			SetTabInformation("Overview", "Overview", "Profile");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			_currentTaskingMethodName = nameof(LoadUserOverviewAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserPinnableAndPinnedRepositoriesAsync);
				await LoadUserPinnableAndPinnedRepositoriesAsync(Login);

				SetTabInformation("Overview", "Overview", "Profile");
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

		private async Task LoadUserPinnableAndPinnedRepositoriesAsync(string login)
		{
			_pinnableRepositories.Clear();
			_pinnedRepositories.Clear();

			PinnedItemQueries queries = new();
			var pinnedItemsRes = await queries.GetAllAsync(login);
			if (pinnedItemsRes == null) return;

			if (pinnedItemsRes.Count == 0)
			{
				var pinnableItemsRes = await queries.GetAllPinnableItems(login);
				if (pinnableItemsRes == null) return;

				foreach (var item in pinnableItemsRes)
				{
					RepoBlockButtonViewModel viewModel = new()
					{
						Repository = item,
						DisplayDetails = false,
						DisplayStarButton = false,
					};

					_pinnableRepositories.Add(viewModel);
				}
			}
			else
			{
				foreach (var item in pinnedItemsRes)
				{
					RepoBlockButtonViewModel viewModel = new()
					{
						Repository = item,
						DisplayDetails = false,
						DisplayStarButton = false,
					};

					_pinnedRepositories.Add(viewModel);
				}
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

		private async Task ShowPinnedRepositoriesEditorDialogAsync()
		{
			var dialogs = new global::FluentHub.App.Dialogs.EditPinnedRepositoriesDialog(Login);
			_ = await dialogs.ShowAsync();
		}
	}
}
