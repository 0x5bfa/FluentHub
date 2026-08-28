// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Users.ViewModels
{
	public class ProjectsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _projects;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Projects { get; }

		public IAsyncRelayCommand LoadUserProjectsPageCommand { get; }
		public IAsyncRelayCommand LoadUserProjectsFurtherCommand { get; }

		public ProjectsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

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
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserProjectsAsync(Login));

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
			var queries = _gitHub.Users.ProjectsV2;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

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
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.ProjectsV2;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
