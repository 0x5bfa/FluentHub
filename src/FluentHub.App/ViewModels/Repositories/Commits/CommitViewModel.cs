using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Commits
{
	public class CommitViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private Commit _commitItem;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryCommitPageCommand { get; }

		public CommitViewModel() : base()
		{
			_diffViewModels = new();
			DiffViewModels = new(_diffViewModels);

			LoadRepositoryCommitPageCommand = new AsyncRelayCommand(LoadRepositoryCommitPageAsync);
		}

		private async Task LoadRepositoryCommitPageAsync()
		{
			SetTabInformation("Commit", "Commit", "Commits");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			_currentTaskingMethodName = nameof(LoadRepositoryCommitPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryOneCommitAsync);
				await LoadRepositoryOneCommitAsync(Login, Name);

				SetTabInformation($"{CommitItem.Message}", $"{CommitItem.Message}");
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

		private async Task LoadRepositoryOneCommitAsync(string owner, string name)
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			try
			{
				DiffQueries queries = new();
				var response = await queries.GetAllAsync(owner, name, CommitItem.Oid);

				_diffViewModels.Clear();
				foreach (var item in response.Files)
				{
					DiffBlockViewModel viewModel = new()
					{
						ChangedFile = item,
					};

					_diffViewModels.Add(viewModel);
				}
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryOneCommitAsync), ex);
				if (_messenger != null)
				{
					UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
					_messenger.Send(notification);
				}
				throw;
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
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

				SelectedTag = "code",
			};
		}
	}
}
