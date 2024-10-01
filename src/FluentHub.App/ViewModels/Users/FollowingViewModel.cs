// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class FollowingViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _followingItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowingItems { get; }

		public IAsyncRelayCommand LoadUserFollowingPageCommand { get; }
		public IAsyncRelayCommand LoadUserFollowingFurtherCommand { get; }

		public FollowingViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_followingItems = new();
			FollowingItems = new(_followingItems);

			LoadUserFollowingPageCommand = new AsyncRelayCommand(LoadUserFollowingPageAsync);
			LoadUserFollowingFurtherCommand = new AsyncRelayCommand(LoadUserFollowingFurtherAsync);
		}

		private async Task LoadUserFollowingPageAsync()
		{
			SetTabInformation("Following", "Following", "Accounts");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserFollowingPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserFollowingAsync);
				await LoadUserFollowingAsync(Login);

				SetTabInformation("Following", "Following");

				if (FollowingItems.Count == 0)
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

		private async Task LoadUserFollowingAsync(string login)
		{
			FollowingQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<User>)result.Response;

			_followingItems.Clear();
			foreach (var item in items)
			{
				UserBlockButtonViewModel viewModel = new()
				{
					User = item,
				};

				_followingItems.Add(viewModel);
			}
		}

		private async Task LoadUserFollowingFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				FollowingQueries queries = new();

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

					_followingItems.Add(viewmodel);
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
