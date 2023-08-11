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
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

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

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryPullRequestsPageAsync);

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

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
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

			RepositoryOverviewViewModel = new()
			{
				Repository = Repository,
				RepositoryName = Repository.Name,
				RepositoryOwnerLogin = Repository.Owner.Login,
				ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

				SelectedTag = "pullrequests",
			};
		}
	}
}
