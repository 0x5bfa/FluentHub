// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class FileChangesViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestFileChangesPageCommand { get; }

		public FileChangesViewModel() : base()
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
			DiffQueries queries = new();
			var response = await queries.GetAllAsync(PullItem.Repository.Owner.Login, PullItem.Repository.Name, PullItem.Number);

			if (response.Any() is false) return;

			_diffViewModels.Clear();
			foreach (var item in response)
			{
				DiffBlockViewModel viewModel = new()
				{
					ChangedFile = new(
						item.FileName,
						item.Additions,
						item.Deletions,
						item.Changes,
						item.Status,
						item.BlobUrl,
						item.ContentsUrl,
						item.RawUrl,
						item.Sha,
						item.Patch,
						item.PreviousFileName),
				};

				_diffViewModels.Add(viewModel);
			}
		}

		public async Task LoadPullRequestAsync(string owner, string name)
		{
			PullRequestQueries queries = new();
			PullItem = await queries.GetAsync(Repository.Owner.Login, Repository.Name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "filechanges",
			};
		}

		public async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
