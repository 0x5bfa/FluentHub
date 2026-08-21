using FluentHub.Core.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryPullRequestsFurtherCommand { get; }

		public PullRequestsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
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
			var queries = _gitHub.Repositories.PullRequests;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20));

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

		private async Task LoadRepositoryPullRequestsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.PullRequests;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor));

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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
