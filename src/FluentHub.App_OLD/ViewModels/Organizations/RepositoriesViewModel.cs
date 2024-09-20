// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Organizations;
using System.Text.RegularExpressions;

namespace FluentHub.App.ViewModels.Organizations
{
	public class RepositoriesViewModel : BaseViewModel
	{
		private bool _oauthAppIsRestrictedByOrgSettings;
		public bool OAuthAppIsRestrictedByOrgSettings { get => _oauthAppIsRestrictedByOrgSettings; set => SetProperty(ref _oauthAppIsRestrictedByOrgSettings, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public IAsyncRelayCommand LoadOrganizationRepositoriesPageCommand { get; }

		public RepositoriesViewModel() : base()
		{
			_repositories = new();
			Repositories = new(_repositories);

			LoadOrganizationRepositoriesPageCommand = new AsyncRelayCommand(LoadOrganizationRepositoriesPageAsync);
		}

		private async Task LoadOrganizationRepositoriesPageAsync()
		{
			SetTabInformation("Repositories", "Repositories", "Repositories");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadOrganizationRepositoriesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadOrganizationAsync);
				await LoadOrganizationAsync(Login);

				_currentTaskingMethodName = nameof(LoadOrganizationRepositoriesAsync);
				await LoadOrganizationRepositoriesAsync(Login);

				SetTabInformation("Repositories", "Repositories", "Repositories");
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

		private async Task LoadOrganizationRepositoriesAsync(string org)
		{
			RepositoryQueries queries = new();
			var response = await queries.GetAllAsync(org);

			_repositories.Clear();
			foreach (var item in response)
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
