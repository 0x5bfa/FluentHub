using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Projects
{
	public class ProjectViewModel : BaseViewModel
	{
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
			InitializeNodePagingInfo();

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
		}
	}
}
