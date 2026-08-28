// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Users.ViewModels
{
	public class FollowingViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _followingItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowingItems { get; }
		private readonly List<User> _loadedFollowing = [];
		private string? _searchText;

		public IAsyncRelayCommand LoadUserFollowingPageCommand { get; }
		public IAsyncRelayCommand LoadUserFollowingFurtherCommand { get; }

		public FollowingViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

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
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserFollowingAsync(Login));

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
			var queries = _gitHub.Users.Following;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_loadedFollowing.Clear();
			_loadedFollowing.AddRange(items);
			_searchText = null;
			RebuildVisibleItems();
		}

		public async Task ApplySearchAsync(string? searchText)
		{
			_searchText = searchText?.Trim();
			SetLoadingProgress(true);

			try
			{
				RebuildVisibleItems();
				while (!string.IsNullOrWhiteSpace(_searchText)
					&& _followingItems.Count < 20
					&& _lastPageInfo is { HasNextPage: true })
				{
					await LoadNextPageAsync();
				}
				IsEmpty = _followingItems.Count == 0;
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

		private async Task LoadUserFollowingFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				await LoadNextPageAsync();
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

		private async Task LoadNextPageAsync()
		{
			if (_lastPageInfo is not { HasNextPage: true })
				return;

			var result = await _gitHub.Users.Following.GetPageAsync(
				Login,
				PageRequest.Forward(100, _lastPageInfo.EndCursor));
			_lastPageInfo = result.PageInfo;
			_loadedFollowing.AddRange(result.Items);
			AppendVisibleItems(result.Items);
		}

		private void RebuildVisibleItems()
		{
			_followingItems.Clear();
			AppendVisibleItems(_loadedFollowing);
		}

		private void AppendVisibleItems(IEnumerable<User> users)
		{
			foreach (var user in users.Where(user => UserProfileListSearch.Matches(user, _searchText)))
				_followingItems.Add(new UserBlockButtonViewModel { User = user });
		}
	}
}
