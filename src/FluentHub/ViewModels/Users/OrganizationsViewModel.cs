// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.Users
{
	public class OrganizationsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<OrgBlockButtonViewModel> _organizations;
		public ReadOnlyObservableCollection<OrgBlockButtonViewModel> Organizations { get; }

		public IAsyncRelayCommand LoadUserOrganizationsPageCommand { get; }
		public IAsyncRelayCommand LoadUserOrganizationsFurtherCommand { get; }

		public OrganizationsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
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
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.Organizations;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
