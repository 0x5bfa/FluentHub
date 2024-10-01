// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Organizations;
using System.Text.RegularExpressions;

namespace FluentHub.App.ViewModels.Organizations
{
	public class OverviewViewModel : BaseViewModel
	{
		private bool _oauthAppIsRestrictedByOrgSettings;
		public bool OAuthAppIsRestrictedByOrgSettings { get => _oauthAppIsRestrictedByOrgSettings; set => SetProperty(ref _oauthAppIsRestrictedByOrgSettings, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnedItems;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnedItems { get; }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public IAsyncRelayCommand LoadOrganizationOverviewPageCommand { get; }

		public OverviewViewModel() : base()
		{
			_pinnedItems = new();
			PinnedItems = new(_pinnedItems);

			_repositories = new();
			Repositories = new(_repositories);

			LoadOrganizationOverviewPageCommand = new AsyncRelayCommand(LoadOrganizationOverviewPageAsync);
		}

		private async Task LoadOrganizationOverviewPageAsync()
		{
			SetTabInformation("Overview", "Overview", "Overview");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadOrganizationOverviewPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadOrganizationAsync);
				await LoadOrganizationAsync(Login);

				_currentTaskingMethodName = nameof(LoadOrganizationOverviewAsync);
				await LoadOrganizationOverviewAsync(Login);

				SetTabInformation("Overview", "Overview");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				// OAuth restriction exception
				if (Regex.IsMatch(ex.Message, @"Although you appear to have the correct authorization credentials, the `.*` organization has enabled OAuth App access restrictions, meaning that data access to third-parties is limited. For more information on these restrictions, including how to enable this app, visit https://docs.github.com/articles/restricting-access-to-your-organization-s-data/"))
				{
					OAuthAppIsRestrictedByOrgSettings = true;
					IsTaskFaulted = false;
				}
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadOrganizationOverviewAsync(string org)
		{
			RepositoryQueries repoQueries = new();
			var repoItems = await repoQueries.GetAllAsync(org);

			_repositories.Clear();
			foreach (var item in repoItems)
			{
				RepoBlockButtonViewModel viewModel = new()
				{
					Repository = item,
					DisplayDetails = false,
					DisplayStarButton = false,
				};

				_repositories.Add(viewModel);
			}

			PinnedItemQueries queries = new();
			var pinnedItems = await queries.GetAllAsync(org);
			if (pinnedItems == null)
				return;

			_pinnedItems.Clear();
			foreach (var item in pinnedItems)
			{
				RepoBlockButtonViewModel viewModel = new()
				{
					Repository = item,
					DisplayDetails = false,
					DisplayStarButton = false,
				};

				_pinnedItems.Add(viewModel);
			}
		}

		private async Task LoadOrganizationAsync(string org)
		{
			OrganizationQueries queries = new();
			var response = await queries.GetAsync(org);

			Organization = response ?? new();

			// View model
			OrganizationProfileOverviewViewModel = new()
			{
				Organization = Organization,
			};
		}
	}
}
