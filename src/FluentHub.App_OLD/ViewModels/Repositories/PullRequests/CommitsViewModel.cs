// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class CommitsViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestCommitsPageCommand { get; }

		public CommitsViewModel() : base()
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
			PullRequestCommitQueries queries = new();
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
			PullRequestQueries queries = new();
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "commits",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
