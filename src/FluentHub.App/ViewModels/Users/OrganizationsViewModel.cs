// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class OrganizationsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<OrgBlockButtonViewModel> _organizations;
		public ReadOnlyObservableCollection<OrgBlockButtonViewModel> Organizations { get; }

		public IAsyncRelayCommand LoadUserOrganizationsPageCommand { get; }
		public IAsyncRelayCommand LoadUserOrganizationsFurtherCommand { get; }

		public OrganizationsViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

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
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserOrganizationsAsync);
				await LoadUserOrganizationsAsync(Login);

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
			OrganizationQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Organization>)result.Response;

			_organizations.Clear();
			foreach (var item in items)
			{
				OrgBlockButtonViewModel viewModel = new()
				{
					OrgItem = item
				};

				_organizations.Add(viewModel);
			}
		}

		private async Task LoadUserOrganizationsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				DiscussionQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Organization>)result.Response;

				foreach (var item in items)
				{
					OrgBlockButtonViewModel viewmodel = new()
					{
						OrgItem = item
					};

					_organizations.Add(viewmodel);
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
