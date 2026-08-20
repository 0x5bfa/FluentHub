// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Models;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.Users
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public IAsyncRelayCommand LoadUserPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadUserPullRequestsFurtherCommand { get; }

		public PullRequestsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadUserPullRequestsPageCommand = new AsyncRelayCommand(LoadUserPullRequestsPageAsync);
			LoadUserPullRequestsFurtherCommand = new AsyncRelayCommand(LoadUserPullRequestsFurtherAsync);
		}

		private async Task LoadUserPullRequestsPageAsync()
		{
			SetTabInformation("Pull Requests", "Pull Requests", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserPullRequestsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserPullRequestsAsync);
				await LoadUserPullRequestsAsync(Login);

				SetTabInformation("Pull Requests", "Pull Requests");

				if (PullItems.Count == 0)
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

		private async Task LoadUserPullRequestsAsync(string login)
		{
			var queries = _gitHub.Users.PullRequests;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

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

		private async Task LoadUserPullRequestsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.PullRequests;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}
	}
}
