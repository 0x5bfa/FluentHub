using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Releases
{
	public class ReleasesViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private readonly ObservableCollection<Release> _items;
		public ReadOnlyObservableCollection<Release> Items { get; }

		private Release _latestRelease;
		public Release LatestRelease { get => _latestRelease; set => SetProperty(ref _latestRelease, value); }

		public IAsyncRelayCommand LoadRepositoryReleasesPageCommand { get; }

		public ReleasesViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryReleasesPageCommand = new AsyncRelayCommand(LoadRepositoryReleasesPageAsync);
		}

		private async Task LoadRepositoryReleasesPageAsync()
		{
			SetTabInformation("Releases", "Releases", "Repositories");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryReleasesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryReleasesAsync);
				await LoadRepositoryReleasesAsync(Login, Name);

				SetTabInformation("Releases", "Releases");
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

		private async Task LoadRepositoryReleasesAsync(string login, string name)
		{
			ReleaseQueries queries = new();
			var items = await queries.GetAllAsync(login, name);

			if (items.Any())
			{
				LatestRelease = items.FirstOrDefault();
			}

			_items.Clear();
			foreach (var item in items) _items.Add(item);
		}

		public async Task LoadRepositoryAsync(string owner, string name)
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
