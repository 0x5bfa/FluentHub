// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class FollowersViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _followersItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowersItems { get; }

		public IAsyncRelayCommand LoadUserFollowersPageCommand { get; }
		public IAsyncRelayCommand LoadUserFollowersFurtherCommand { get; }

		public FollowersViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_followersItems = new();
			FollowersItems = new(_followersItems);

			LoadUserFollowersPageCommand = new AsyncRelayCommand(LoadUserFollowersPageAsync);
			LoadUserFollowersFurtherCommand = new AsyncRelayCommand(LoadUserFollowersFurtherAsync);
		}

		private async Task LoadUserFollowersPageAsync()
		{
			SetTabInformation("Followers", "Followers", "Accounts");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserFollowersPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserFollowersAsync);
				await LoadUserFollowersAsync(Login);

				SetTabInformation("Followers", "Followers");

				if (FollowersItems.Count == 0)
					IsEmpty = true;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadUserFollowersAsync(string login)
		{
			FollowersQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<User>)result.Response;

			_followersItems.Clear();
			foreach (var item in items)
			{
				UserBlockButtonViewModel viewModel = new()
				{
					User = item,
				};

				_followersItems.Add(viewModel);
			}
		}

		private async Task LoadUserFollowersFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				FollowersQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<User>)result.Response;

				foreach (var item in items)
				{
					UserBlockButtonViewModel viewmodel = new()
					{
						User = item,
					};

					_followersItems.Add(viewmodel);
				}
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}
	}
}
