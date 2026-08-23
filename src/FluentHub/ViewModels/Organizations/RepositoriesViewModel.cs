// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Contracts;
using FluentHub.Core.Queries.Users;
using FluentHub.ViewModels.UserControls.BlockButtons;
using System.Text.RegularExpressions;
using OctokitGraphQLModel = Octokit.GraphQL.Model;

namespace FluentHub.ViewModels.Organizations
{
	public class RepositoriesViewModel : BaseViewModel
	{
		private bool _oauthAppIsRestrictedByOrgSettings;
		public bool OAuthAppIsRestrictedByOrgSettings { get => _oauthAppIsRestrictedByOrgSettings; set => SetProperty(ref _oauthAppIsRestrictedByOrgSettings, value); }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

		public ObservableCollection<string> TypeFilterOptions { get; } =
		[
			"All",
			"Public",
			"Private",
			"Sources",
			"Forks",
			"Archived",
			"Can be sponsored",
			"Mirrors",
			"Templates",
		];

		public ObservableCollection<string> LanguageFilterOptions { get; } = ["All"];

		public ObservableCollection<string> SortFilterOptions { get; } = ["Last updated", "Name", "Stars"];

		private UserRepositoryListFilters _filters = new();
		private IReadOnlyList<Repository>? _localSearchResults;
		private int _localSearchOffset;
		private bool _languagesLoaded;

		public IAsyncRelayCommand LoadOrganizationRepositoriesPageCommand { get; }
		public IAsyncRelayCommand LoadOrganizationRepositoriesFurtherCommand { get; }
		public IAsyncRelayCommand LoadLanguageOptionsCommand { get; }

		public RepositoriesViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_repositories = new();
			Repositories = new(_repositories);

			LoadOrganizationRepositoriesPageCommand = new AsyncRelayCommand(LoadOrganizationRepositoriesPageAsync);
			LoadOrganizationRepositoriesFurtherCommand = new AsyncRelayCommand(LoadOrganizationRepositoriesFurtherAsync);
			LoadLanguageOptionsCommand = new AsyncRelayCommand(LoadLanguageOptionsAsync);
		}

		private async Task LoadOrganizationRepositoriesPageAsync(CancellationToken token)
		{
			SetTabInformation("Repositories", "Repositories", "Repositories");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new UserRepositoryListFilters();
			_languagesLoaded = false;
			LanguageFilterOptions.Clear();
			LanguageFilterOptions.Add("All");
			OAuthAppIsRestrictedByOrgSettings = false;
			_currentTaskingMethodName = nameof(LoadOrganizationRepositoriesPageAsync);

			try
			{
				var organizationTask = LoadOrganizationAsync(Login, token);
				var repositoriesTask = LoadOrganizationRepositoriesAsync(Login, token);
				await Task.WhenAll(organizationTask, repositoriesTask);

				SetTabInformation("Repositories", "Repositories", "Repositories");
				IsEmpty = Repositories.Count == 0;
			}
			catch (OperationCanceledException) when (token.IsCancellationRequested)
			{
			}
			catch (Exception ex)
			{
				HandleLoadException(ex);
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadOrganizationRepositoriesAsync(
			string organization,
			CancellationToken cancellationToken = default)
		{
			var items = await GetInitialRepositoriesAsync(organization, cancellationToken);
			ReplaceRepositories(items);
			AddLanguageOptions(items);
		}

		private async Task LoadOrganizationRepositoriesFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);
			try
			{
				if (_localSearchResults is not null)
				{
					var nextItems = _localSearchResults.Skip(_localSearchOffset).Take(20).ToList();
					_localSearchOffset += nextItems.Count;
					_lastPageInfo = new PageInfo
					{
						HasNextPage = _localSearchOffset < _localSearchResults.Count,
					};
					AppendRepositories(nextItems);
					AddLanguageOptions(nextItems);
					return;
				}

				var result = await GetRepositoryPageAsync(
					Login,
					PageRequest.Forward(20, _lastPageInfo.EndCursor));
				_lastPageInfo = result.PageInfo;
				AppendRepositories(result.Items);
				AddLanguageOptions(result.Items);
			}
			catch (Exception ex)
			{
				HandleLoadException(ex);
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		public async Task ApplyFiltersAsync(UserRepositoryListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(filters);

			_filters = filters;
			InitializeNodePagingInfo();
			SetLoadingProgress(true);
			_currentTaskingMethodName = nameof(ApplyFiltersAsync);

			try
			{
				await LoadOrganizationRepositoriesAsync(Login);
				IsEmpty = Repositories.Count == 0;
			}
			catch (Exception ex)
			{
				HandleLoadException(ex);
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task<IReadOnlyList<Repository>> GetInitialRepositoriesAsync(
			string organization,
			CancellationToken cancellationToken)
		{
			var queries = _gitHub.Organizations.Repositories;
			if (RequiresSearch(_filters))
			{
				_localSearchResults = await queries.SearchAllAsync(organization, _filters, cancellationToken);
				_localSearchOffset = Math.Min(20, _localSearchResults.Count);
				_lastPageInfo = new PageInfo
				{
					HasNextPage = _localSearchOffset < _localSearchResults.Count,
				};
				return _localSearchResults.Take(_localSearchOffset).ToList();
			}

			_localSearchResults = null;
			_localSearchOffset = 0;
			var result = await GetRepositoryPageAsync(
				organization,
				PageRequest.Forward(20),
				cancellationToken);
			_lastPageInfo = result.PageInfo;
			return result.Items;
		}

		private Task<PageResult<Repository>> GetRepositoryPageAsync(
			string organization,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			var isArchived = _filters.Type == UserRepositoryTypeFilter.Archived ? (bool?)true : null;
			var isFork = _filters.Type switch
			{
				UserRepositoryTypeFilter.Sources => false,
				UserRepositoryTypeFilter.Forks => true,
				_ => (bool?)null,
			};
			var privacy = _filters.Type switch
			{
				UserRepositoryTypeFilter.Public => OctokitGraphQLModel.RepositoryPrivacy.Public,
				UserRepositoryTypeFilter.Private => OctokitGraphQLModel.RepositoryPrivacy.Private,
				_ => (OctokitGraphQLModel.RepositoryPrivacy?)null,
			};
			var order = _filters.Sort switch
			{
				UserRepositorySort.LastUpdated => new OctokitGraphQLModel.RepositoryOrder
				{
					Direction = OctokitGraphQLModel.OrderDirection.Desc,
					Field = OctokitGraphQLModel.RepositoryOrderField.UpdatedAt,
				},
				UserRepositorySort.Name => new OctokitGraphQLModel.RepositoryOrder
				{
					Direction = OctokitGraphQLModel.OrderDirection.Asc,
					Field = OctokitGraphQLModel.RepositoryOrderField.Name,
				},
				UserRepositorySort.Stars => new OctokitGraphQLModel.RepositoryOrder
				{
					Direction = OctokitGraphQLModel.OrderDirection.Desc,
					Field = OctokitGraphQLModel.RepositoryOrderField.Stargazers,
				},
				_ => throw new ArgumentOutOfRangeException(nameof(_filters.Sort), _filters.Sort, "Unsupported repository sort."),
			};

			return _gitHub.Organizations.Repositories.GetPageAsync(
				organization,
				page,
				isArchived: isArchived,
				isFork: isFork,
				orderBy: order,
				privacy: privacy,
				cancellationToken: cancellationToken);
		}

		private async Task LoadLanguageOptionsAsync()
		{
			if (_languagesLoaded)
				return;

			try
			{
				var languages = await _gitHub.Organizations.Repositories.GetLanguagesAsync(Login);
				AddLanguageOptions(languages);
				_languagesLoaded = true;
			}
			catch (Exception ex)
			{
				_logger?.Warn("Failed to load organization repository language filters: {0}", ex.Message);
			}
		}

		private void ReplaceRepositories(IEnumerable<Repository> repositories)
		{
			_repositories.Clear();
			AppendRepositories(repositories);
		}

		private void AppendRepositories(IEnumerable<Repository> repositories)
		{
			foreach (var repository in repositories)
			{
				_repositories.Add(new RepoBlockButtonViewModel(_gitHub)
				{
					Repository = repository,
					DisplayDetails = true,
					DisplayStarButton = true,
				});
			}
		}

		private void AddLanguageOptions(IEnumerable<Repository> repositories)
			=> AddLanguageOptions(repositories
				.Select(repository => repository.PrimaryLanguage?.Name)
				.Where(language => !string.IsNullOrWhiteSpace(language))
				.Select(language => language!));

		private void AddLanguageOptions(IEnumerable<string> languages)
		{
			foreach (var language in languages
				.Where(language => !string.IsNullOrWhiteSpace(language))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(language => language, StringComparer.OrdinalIgnoreCase))
			{
				if (LanguageFilterOptions.Contains(language, StringComparer.OrdinalIgnoreCase))
					continue;

				var index = 1;
				while (index < LanguageFilterOptions.Count
					&& StringComparer.OrdinalIgnoreCase.Compare(LanguageFilterOptions[index], language) < 0)
				{
					index++;
				}
				LanguageFilterOptions.Insert(index, language);
			}
		}

		private async Task LoadOrganizationAsync(
			string organization,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.Organizations.Organizations.GetAsync(organization, cancellationToken);
			Organization = response ?? new();
			OrganizationProfileOverviewViewModel = new()
			{
				Organization = Organization,
			};
		}

		private void HandleLoadException(Exception exception)
		{
			TaskException = exception;
			IsTaskFaulted = true;

			if (Regex.IsMatch(exception.Message, @"Although you appear to have the correct authorization credentials, the `.*` organization has enabled OAuth App access restrictions, meaning that data access to third-parties is limited. For more information on these restrictions, including how to enable this app, visit https://docs.github.com/articles/restricting-access-to-your-organization-s-data/"))
			{
				OAuthAppIsRestrictedByOrgSettings = true;
				IsTaskFaulted = false;
			}
		}

		private static bool RequiresSearch(UserRepositoryListFilters filters)
			=> !string.IsNullOrWhiteSpace(filters.SearchText)
			|| !string.IsNullOrWhiteSpace(filters.Language)
			|| filters.Type is UserRepositoryTypeFilter.Sponsorable
				or UserRepositoryTypeFilter.Mirrors
				or UserRepositoryTypeFilter.Templates;
	}
}
