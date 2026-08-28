// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Shared.Controls.ViewModels;
using FluentHub.Shared.Controls.ViewModels.Overview;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Repositories.ViewModels.PullRequests
{
	public class FileChangesViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem = default!;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestFileChangesPageCommand { get; }

		public FileChangesViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_diffViewModels = new();
			DiffViewModels = new(_diffViewModels);

			LoadRepositoryPullRequestFileChangesPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestFileChangesPageAsync);
		}

		private async Task LoadRepositoryPullRequestFileChangesPageAsync()
		{
			SetTabInformation("File changes", "File changes", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestFileChangesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestFileChangesAsync);
				await LoadRepositoryPullRequestFileChangesAsync(Login, Name);

				SetTabInformation("File changes", "File changes");
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

		private async Task LoadRepositoryPullRequestFileChangesAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Diffs;
			var response = await queries.GetPullRequestFilesAsync(
				PullItem.Repository.Owner.Login,
				PullItem.Repository.Name,
				PullItem.Number);

			if (response.Any() is false) return;

			_diffViewModels.Clear();
			foreach (var item in response)
			{
				DiffBlockViewModel viewModel = new()
				{
					ChangedFile = item,
				};

				_diffViewModels.Add(viewModel);
			}
		}

		public async Task LoadPullRequestAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;
			PullItem = await queries.GetAsync(Repository.Owner.Login, Repository.Name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "filechanges",
			};
		}

		public async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
