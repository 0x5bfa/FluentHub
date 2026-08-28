// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Shared.Controls.ViewModels;
using FluentHub.Shared.Controls.ViewModels.Overview;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Repositories.ViewModels.PullRequests
{
	public class CommitViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest _pullRequest = default!;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

		private Commit _commitItem = default!;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestCommitPageCommand { get; }

		public CommitViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
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
			var queries = _gitHub.Repositories.Diffs;
			var response = await queries.GetPullRequestFilesAsync(owner, name, PullRequest.Number);

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
			var queries = _gitHub.Repositories.PullRequests;
			PullRequest = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullRequest,
				SelectedTag = "commits",
			};
		}

		public async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
