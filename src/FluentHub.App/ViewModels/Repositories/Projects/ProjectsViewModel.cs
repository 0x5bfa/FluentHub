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

namespace FluentHub.App.ViewModels.Repositories.Projects
{
	public class ProjectsViewModel : BaseViewModel
	{
		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryProjectsPageCommand { get; }

		public ProjectsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryProjectsPageCommand = new AsyncRelayCommand(LoadRepositoryProjectsPageAsync);
		}

		private async Task LoadRepositoryProjectsPageAsync()
		{
			SetTabInformation("Projects", "Projects", "Projects");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadRepositoryProjectsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadProjectsPageAsync);
				await LoadProjectsPageAsync(Login, Name);

				SetTabInformation($"Projects \u2022 {Login}/{Name}", $"Projects \u2022 {Login}/{Name}");

				if (Items.Count == 0)
					IsEmpty = true;
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

		private async Task LoadProjectsPageAsync(string owner, string name)
		{
			ProjectQueries queries = new();
			var items = await queries.GetAllAsync(owner, name);

			_items.Clear();
			foreach (var item in items)
			{
				ProjectBlockButtonViewModel viewmodel = new()
				{
					Item = item,
				};

				_items.Add(viewmodel);
			}
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
