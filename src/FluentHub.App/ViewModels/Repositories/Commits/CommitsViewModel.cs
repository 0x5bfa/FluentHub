using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Commits
{
	public class CommitsViewModel : BaseViewModel
	{
		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryCommitsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryCommitsFurtherCommand { get; }

		public CommitsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryCommitsPageAsync);
			LoadRepositoryCommitsFurtherCommand = new AsyncRelayCommand(LoadRepositoryCommitsFurtherAsync);
		}

		private async Task LoadRepositoryCommitsPageAsync()
		{
			SetTabInformation("Commits", "Commits", "Commits");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryCommitsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryCommitsAsync);
				await LoadRepositoryCommitsAsync(Login, Name);

				SetTabInformation($"Commits \u2022 {Login}/{Name}", $"Commits \u2022 {Login}/{Name}");
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

		private async Task LoadRepositoryCommitsAsync(string owner, string name)
		{
			CommitQueries queries = new();

			var result = await queries.GetAllAsync(
				owner: owner,
				name: name,
				first: 20,
				refs: ContextViewModel.BranchName,
				path: ContextViewModel.Path);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Commit>)result.Response;

			_items.Clear();
			foreach (var item in items)
			{
				CommitBlockButtonViewModel viewModel = new()
				{
					CommitItem = item,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadRepositoryCommitsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				CommitQueries queries = new();

				var result = await queries.GetAllAsync(
					owner: Login,
					name: Name,
					refs: ContextViewModel.BranchName,
					first: 20,
					after: _lastPageInfo.EndCursor,
					path: ContextViewModel.Path);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Commit>)result.Response;

				foreach (var item in items)
				{
					CommitBlockButtonViewModel viewModel = new()
					{
						CommitItem = item,
					};

					_items.Add(viewModel);
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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
