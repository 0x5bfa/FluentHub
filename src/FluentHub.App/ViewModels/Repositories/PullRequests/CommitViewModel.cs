// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class CommitViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest _pullRequest;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

		private Commit _commitItem;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestCommitPageCommand { get; }

		public CommitViewModel() : base()
		{
			_diffViewModels = new();
			DiffViewModels = new(_diffViewModels);

			LoadRepositoryPullRequestCommitPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestCommitPageAsync);
		}

		private async Task LoadRepositoryPullRequestCommitPageAsync()
		{
			SetTabInformation("Commit", "Commit", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestOneCommitAsync);
				await LoadRepositoryPullRequestOneCommitAsync(Login, Name);

				SetTabInformation("Commit", "Commit");
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

		private async Task LoadRepositoryPullRequestOneCommitAsync(string owner, string name)
		{
			DiffQueries queries = new();
			var response = await queries.GetAllAsync(owner, name, PullRequest.Number);

			_diffViewModels.Clear();
			foreach (var item in response)
			{
				DiffBlockViewModel viewModel = new()
				{
					ChangedPullRequestFile = item,
				};

				_diffViewModels.Add(viewModel);
			}
		}

		public async Task LoadPullRequestAsync(string owner, string name)
		{
			PullRequestQueries queries = new();
			PullRequest = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullRequest,
				SelectedTag = "commits",
			};
		}

		public async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
