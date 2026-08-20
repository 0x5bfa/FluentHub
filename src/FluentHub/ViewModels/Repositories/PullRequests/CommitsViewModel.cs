// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class CommitsViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem = default!;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestCommitsPageCommand { get; }

		public CommitsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryPullRequestCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestCommitsPageAsync);
		}

		private async Task LoadRepositoryPullRequestCommitsPageAsync()
		{
			SetTabInformation("Commits", "Commits", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsAsync);
				await LoadRepositoryPullRequestCommitsAsync(Login, Name);

				SetTabInformation("Commits", "Commits");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
				PullRequestOverviewViewModel.Loaded = true;
			}
		}

		private async Task LoadRepositoryPullRequestCommitsAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequestCommits;
			var items = await queries.GetAllAsync(owner, name, PullItem.Number);

			_items.Clear();
			foreach (var item in items)
			{
				CommitBlockButtonViewModel viewModel = new()
				{
					CommitItem = item,
					PullRequest = pullItem,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "commits",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
