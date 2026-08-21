// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.Users
{
	public class StarredReposViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public IAsyncRelayCommand LoadUserStarredRepositoriesPageCommand { get; }
		public IAsyncRelayCommand LoadUserStarredRepositoriesFurtherCommand { get; }

		public StarredReposViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_repositories = new();
			Repositories = new(_repositories);

			LoadUserStarredRepositoriesPageCommand = new AsyncRelayCommand(LoadUserStarredRepositoriesPageAsync);
			LoadUserStarredRepositoriesFurtherCommand = new AsyncRelayCommand(LoadUserStarredRepositoriesFurtherAsync);
		}

		private async Task LoadUserStarredRepositoriesPageAsync()
		{
			SetTabInformation("Stars", "Stars", "Starred");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserStarredRepositoriesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserStarredRepositoriesAsync);
				await LoadUserStarredRepositoriesAsync(Login);

				SetTabInformation("Stars", "Stars");

				if (Repositories.Count == 0)
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

		private async Task LoadUserStarredRepositoriesAsync(string login)
		{
			var queries = _gitHub.Users.StarredRepositories;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_repositories.Clear();
			foreach (var item in items)
			{
				RepoBlockButtonViewModel viewModel = new(_gitHub)
				{
					Repository = item,
					DisplayDetails = true,
					DisplayStarButton = true,
				};

				_repositories.Add(viewModel);
			}
		}

		private async Task LoadUserStarredRepositoriesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.StarredRepositories;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					RepoBlockButtonViewModel viewModel = new(_gitHub)
					{
						Repository = item,
						DisplayDetails = true,
						DisplayStarButton = true,
					};

					_repositories.Add(viewModel);
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
