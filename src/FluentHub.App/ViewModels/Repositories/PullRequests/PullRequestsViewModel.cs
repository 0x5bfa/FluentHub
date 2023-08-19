using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestsPageCommand { get; }

		public PullRequestsViewModel() : base()
		{
			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadRepositoryPullRequestsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsPageAsync);
		}

		private async Task LoadRepositoryPullRequestsPageAsync()
		{
			SetTabInformation("Pull requests", "Pull requests", "PullRequests");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsAsync);
				await LoadRepositoryPullRequestsAsync(Login, Name);

				SetTabInformation($"Pull requests \u2022 {Login}/{Name}", $"Pull requests \u2022 {Login}/{Name}");
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
			var items = await queries.GetAllAsync(name, owner);
			if (items == null) return;

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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
