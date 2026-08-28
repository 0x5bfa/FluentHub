// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.Controls.BlockButtons;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Users
{
	public class FollowersViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _followersItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> FollowersItems { get; }
		private readonly List<User> _loadedFollowers = [];
		private string? _searchText;

		public IAsyncRelayCommand LoadUserFollowersPageCommand { get; }
		public IAsyncRelayCommand LoadUserFollowersFurtherCommand { get; }

		public FollowersViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

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
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserFollowersAsync(Login));

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
			var queries = _gitHub.Users.Followers;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_loadedFollowers.Clear();
			_loadedFollowers.AddRange(items);
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
					&& _followersItems.Count < 20
					&& _lastPageInfo is { HasNextPage: true })
				{
					await LoadNextPageAsync();
				}
				IsEmpty = _followersItems.Count == 0;
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

		private async Task LoadUserFollowersFurtherAsync()
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

			var result = await _gitHub.Users.Followers.GetPageAsync(
				Login,
				PageRequest.Forward(100, _lastPageInfo.EndCursor));
			_lastPageInfo = result.PageInfo;
			_loadedFollowers.AddRange(result.Items);
			AppendVisibleItems(result.Items);
		}

		private void RebuildVisibleItems()
		{
			_followersItems.Clear();
			AppendVisibleItems(_loadedFollowers);
		}

		private void AppendVisibleItems(IEnumerable<User> users)
		{
			foreach (var user in users.Where(user => UserProfileListSearch.Matches(user, _searchText)))
				_followersItems.Add(new UserBlockButtonViewModel { User = user });
		}
	}
}
