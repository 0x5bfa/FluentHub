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
	public class CommitsViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryPullRequestCommitsPageCommand { get; }

		public CommitsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryPullRequestCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestCommitsPageAsync);
		}

		private async Task LoadRepositoryPullRequestCommitsPageAsync()
		{
			SetTabInformation("Commits", "Commits", "PullRequests");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsAsync);
				await LoadRepositoryPullRequestCommitsAsync(Login, Name);

				SetTabInformation("Commits", "Commits");
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

		private async Task LoadRepositoryPullRequestCommitsAsync(string owner, string name)
		{
			PullRequestCommitQueries queries = new();
			var items = await queries.GetAllAsync(owner, name, PullItem.Number);

			_items.Clear();
			foreach (var item in items)
			{
				CommitBlockButtonViewModel viewModel = new()
				{
					CommitItem = item,
					PullRequest = pullItem,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			PullRequestQueries queries = new();
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "commits",
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
