using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Projects
{
	public class ProjectViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private Project _project;
		public Project Project { get => _project; set => SetProperty(ref _project, value); }

		public IAsyncRelayCommand LoadRepositoryProjectPageCommand { get; }

		public ProjectViewModel() : base()
		{
			LoadRepositoryProjectPageCommand = new AsyncRelayCommand(LoadRepositoryProjectPageAsync);
		}

		private async Task LoadRepositoryProjectPageAsync()
		{
			SetTabInformation("Project", "Project", "Projects");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadRepositoryProjectPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadProjectPageAsync);
				await LoadProjectPageAsync(Login, Name);

				SetTabInformation($"{Project.Name}", $"{Project.Name}");
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

		private async Task LoadProjectPageAsync(string owner, string name)
		{
			ProjectQueries queries = new();
			var response = await queries.GetAsync(owner, name, Number);

			Project = response;
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

				SelectedTag = "projects",
			};
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Project";
			currentItem.Description = "Project";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
			};
		}
	}
}
