using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Projects
{
	public class ProjectsViewModel : BaseViewModel
	{
		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryProjectsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryProjectsFurtherCommand { get; }

		public ProjectsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryProjectsPageCommand = new AsyncRelayCommand(LoadRepositoryProjectsPageAsync);
			LoadRepositoryProjectsFurtherCommand = new AsyncRelayCommand(LoadRepositoryProjectsFurtherAsync);
		}

		private async Task LoadRepositoryProjectsPageAsync()
		{
			SetTabInformation("Projects", "Projects", "Projects");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

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

			var result = await queries.GetAllAsync(
				owner: owner,
				name: name,
				first: 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Project>)result.Response;

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

		private async Task LoadRepositoryProjectsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				ProjectQueries queries = new();

				var result = await queries.GetAllAsync(
					owner: Login,
					name: Name,
					first: 20,
					after: _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Project>)result.Response;

				foreach (var item in items)
				{
					ProjectBlockButtonViewModel viewmodel = new()
					{
						Item = item,
					};

					_items.Add(viewmodel);
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
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
