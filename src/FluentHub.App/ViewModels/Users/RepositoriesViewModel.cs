// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class RepositoriesViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public IAsyncRelayCommand LoadUserRepositoriesPageCommand { get; }
		public IAsyncRelayCommand LoadUserRepositoriesFurtherCommand { get; }

		public RepositoriesViewModel() : base()
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

			LoadUserRepositoriesPageCommand = new AsyncRelayCommand(LoadUserRepositoriesPageAsync);
			LoadUserRepositoriesFurtherCommand = new AsyncRelayCommand(LoadUserRepositoriesFurtherAsync);
		}

		private async Task LoadUserRepositoriesPageAsync(CancellationToken token)
		{
			SetTabInformation("Repositories", "Repositories", "Repositories");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserRepositoriesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserRepositoriesAsync);
				await LoadUserRepositoriesAsync(Login);

				SetTabInformation("Repositories", "Repositories");

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

		private async Task LoadUserRepositoriesAsync(string login)
		{
			RepositoryQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Repository>)result.Response;

			_repositories.Clear();
			foreach (var item in items)
			{
				RepoBlockButtonViewModel viewModel = new()
				{
					Repository = item,
					DisplayDetails = true,
					DisplayStarButton = true,
				};

				_repositories.Add(viewModel);
			}
		}

		private async Task LoadUserRepositoriesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				RepositoryQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Repository>)result.Response;

				foreach (var item in items)
				{
					RepoBlockButtonViewModel viewModel = new()
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
