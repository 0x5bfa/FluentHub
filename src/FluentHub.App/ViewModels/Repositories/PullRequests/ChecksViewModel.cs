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
	public class ChecksViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CheckSuite> _items;
		public ReadOnlyObservableCollection<CheckSuite> Items { get; }

		private CheckRun _selectedCheckRun;
		public CheckRun SelectedCheckRun { get => _selectedCheckRun; set => SetProperty(ref _selectedCheckRun, value); }

		public IAsyncRelayCommand LoadRepositoryPullRequestChecksPageCommand { get; }

		public ChecksViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryPullRequestChecksPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestChecksPageAsync);
		}

		private async Task LoadRepositoryPullRequestChecksPageAsync()
		{
			SetTabInformation("Checks", "Checks", "PullRequests");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryPullRequestChecksPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestChecksAsync);
				await LoadRepositoryPullRequestChecksAsync(Login, Name);

				SetTabInformation("Checks", "Checks");
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

		private async Task LoadRepositoryPullRequestChecksAsync(string owner, string name)
		{
			PullRequestCheckQueries queries = new();
			var response = await queries.GetAllAsync(owner, name, Number);

			// Remove elements that doen't have any CheckRuns
			response.RemoveAll(p => p.CheckRuns.Nodes.Count == 0);

			foreach (var item in response)
			{
				_items.Add(item);
			}
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			PullRequestQueries queries = new();
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "checks",
			};
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
