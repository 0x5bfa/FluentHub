// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.Controls.BlockButtons;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Users
{
	public class OrganizationsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<OrgBlockButtonViewModel> _organizations;
		public ReadOnlyObservableCollection<OrgBlockButtonViewModel> Organizations { get; }
		private readonly List<Organization> _loadedOrganizations = [];
		private string? _searchText;

		public IAsyncRelayCommand LoadUserOrganizationsPageCommand { get; }
		public IAsyncRelayCommand LoadUserOrganizationsFurtherCommand { get; }

		public OrganizationsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

			_organizations = new();
			Organizations = new(_organizations);

			LoadUserOrganizationsPageCommand = new AsyncRelayCommand(LoadUserOrganizationsPageAsync);
			LoadUserOrganizationsFurtherCommand = new AsyncRelayCommand(LoadUserOrganizationsFurtherAsync);
		}

		private async Task LoadUserOrganizationsPageAsync()
		{
			SetTabInformation("Organizations", "Organizations", "Organizations");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserOrganizationsPageAsync);

			try
			{
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserOrganizationsAsync(Login));

				SetTabInformation("Organizations", "Organizations");

				if (Organizations.Count == 0)
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

		private async Task LoadUserOrganizationsAsync(string login)
		{
			var queries = _gitHub.Users.Organizations;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_loadedOrganizations.Clear();
			_loadedOrganizations.AddRange(items);
			_searchText = null;
			RebuildVisibleItems();
		}

		public async Task ApplySearchAsync(string? searchText)
		{
			_searchText = searchText?.Trim();
			SetLoadingProgress(true);

			try
			{
				RebuildVisibleItems();
				while (!string.IsNullOrWhiteSpace(_searchText)
					&& _organizations.Count < 20
					&& _lastPageInfo is { HasNextPage: true })
				{
					await LoadNextPageAsync();
				}
				IsEmpty = _organizations.Count == 0;
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

		private async Task LoadUserOrganizationsFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				await LoadNextPageAsync();
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

		private async Task LoadNextPageAsync()
		{
			if (_lastPageInfo is not { HasNextPage: true })
				return;

			var result = await _gitHub.Users.Organizations.GetPageAsync(
				Login,
				PageRequest.Forward(100, _lastPageInfo.EndCursor));
			_lastPageInfo = result.PageInfo;
			_loadedOrganizations.AddRange(result.Items);
			AppendVisibleItems(result.Items);
		}

		private void RebuildVisibleItems()
		{
			_organizations.Clear();
			AppendVisibleItems(_loadedOrganizations);
		}

		private void AppendVisibleItems(IEnumerable<Organization> organizations)
		{
			foreach (var organization in organizations.Where(item => UserProfileListSearch.Matches(item, _searchText)))
				_organizations.Add(new OrgBlockButtonViewModel { OrgItem = organization });
		}
	}
}
