// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class ProjectsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _projects;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Projects { get; }

		public IAsyncRelayCommand LoadUserProjectsPageCommand { get; }
		public IAsyncRelayCommand LoadUserProjectsFurtherCommand { get; }

		public ProjectsViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_projects = new();
			Projects = new(_projects);

			LoadUserProjectsPageCommand = new AsyncRelayCommand(LoadUserProjectsPageAsync);
			LoadUserProjectsFurtherCommand = new AsyncRelayCommand(LoadUserProjectsFurtherAsync);
		}

		private async Task LoadUserProjectsPageAsync()
		{
			SetTabInformation("Projects", "Projects", "Projects");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserProjectsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserProjectsAsync);
				await LoadUserProjectsAsync(Login);

				SetTabInformation("Projects", "Projects");

				if (Projects.Count == 0)
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

		private async Task LoadUserProjectsAsync(string login)
		{
			ProjectQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Project>)result.Response;

			_projects.Clear();
			foreach (var item in items)
			{
				ProjectBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_projects.Add(viewModel);
			}
		}

		private async Task LoadUserProjectsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				ProjectQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
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

					_projects.Add(viewmodel);
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
	}
}
