using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryPullRequestsFurtherCommand { get; }

		public PullRequestsViewModel() : base()
		{
			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadRepositoryPullRequestsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsPageAsync);
			LoadRepositoryPullRequestsFurtherCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsFurtherAsync);
		}

		private async Task LoadRepositoryPullRequestsPageAsync()
		{
			SetTabInformation("Pull requests", "Pull requests", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsAsync);
				await LoadRepositoryPullRequestsAsync(Login, Name);

				SetTabInformation($"Pull requests \u2022 {Login}/{Name}", $"Pull requests \u2022 {Login}/{Name}");

				if (PullItems.Count == 0)
					IsEmpty = true;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				if (PullItems.Count == 0)
					IsEmpty = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadRepositoryPullRequestsAsync(string owner, string name)
		{
			PullRequestQueries queries = new();

			var result = await queries.GetAllAsync(
				owner: owner,
				name: name,
				first: 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<PullRequest>)result.Response;

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

		private async Task LoadRepositoryPullRequestsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				PullRequestQueries queries = new();

				var result = await queries.GetAllAsync(
					owner: Login,
					name: Name,
					first: 20,
					after: _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<PullRequest>)result.Response;

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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
