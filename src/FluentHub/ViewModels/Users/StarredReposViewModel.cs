// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Core.Contracts;
using OctokitGraphQLModel = Octokit.GraphQL.Model;

namespace FluentHub.ViewModels.Users
{
	public class StarredReposViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

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

		public ObservableCollection<string> SortFilterOptions { get; } =
		[
			"Recently starred",
			"Recently active",
			"Most stars",
		];

		private StarredRepositoryListFilters _filters = new();
		private IReadOnlyList<Repository>? _allRepositories;
		private IReadOnlyList<Repository>? _localFilteredRepositories;
		private int _localFilterOffset;
		private bool _languagesLoaded;

		public IAsyncRelayCommand LoadUserStarredRepositoriesPageCommand { get; }
		public IAsyncRelayCommand LoadUserStarredRepositoriesFurtherCommand { get; }
		public IAsyncRelayCommand LoadLanguageOptionsCommand { get; }

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
			LoadLanguageOptionsCommand = new AsyncRelayCommand(LoadLanguageOptionsAsync);
		}

		private async Task LoadUserStarredRepositoriesPageAsync(CancellationToken cancellationToken)
		{
			SetTabInformation("Stars", "Stars", "Starred");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new StarredRepositoryListFilters();
			_allRepositories = null;

			_currentTaskingMethodName = nameof(LoadUserStarredRepositoriesPageAsync);

			try
			{
				var userTask = LoadUserAsync(Login, cancellationToken);
				var repositoriesTask = LoadUserStarredRepositoriesAsync(Login, cancellationToken);
				await Task.WhenAll(userTask, repositoriesTask);

				SetTabInformation("Stars", "Stars");

				IsEmpty = Repositories.Count == 0;
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
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

		private async Task LoadUserStarredRepositoriesAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			var queries = _gitHub.Users.StarredRepositories;
			IReadOnlyList<Repository> items;

			if (RequiresAllRepositories(_filters))
			{
				_allRepositories ??= await queries.GetAllAsync(login, cancellationToken);
				_localFilteredRepositories = UserRepositoryFilter.Apply(_allRepositories, _filters);
				_localFilterOffset = Math.Min(20, _localFilteredRepositories.Count);
				items = _localFilteredRepositories.Take(_localFilterOffset).ToList();
				_lastPageInfo = new PageInfo
				{
					HasNextPage = _localFilterOffset < _localFilteredRepositories.Count,
				};
			}
			else
			{
				_localFilteredRepositories = null;
				_localFilterOffset = 0;
				var result = await queries.GetPageAsync(
					login,
					PageRequest.Forward(20),
					CreateStarOrder(),
					cancellationToken: cancellationToken);
				_lastPageInfo = result.PageInfo;
				items = result.Items;
			}

			ReplaceRepositories(items);
			AddLanguageOptions(items);
		}

		private async Task LoadUserStarredRepositoriesFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				if (_localFilteredRepositories is not null)
				{
					var nextItems = _localFilteredRepositories.Skip(_localFilterOffset).Take(20).ToList();
					_localFilterOffset += nextItems.Count;
					_lastPageInfo = new PageInfo
					{
						HasNextPage = _localFilterOffset < _localFilteredRepositories.Count,
					};
					AppendRepositories(nextItems);
					AddLanguageOptions(nextItems);
					return;
				}

				var queries = _gitHub.Users.StarredRepositories;

				var result = await queries.GetPageAsync(
					Login,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					CreateStarOrder());

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				AppendRepositories(items);
				AddLanguageOptions(items);
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

		public async Task ApplyFiltersAsync(StarredRepositoryListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(filters);

			_filters = filters;
			InitializeNodePagingInfo();
			SetLoadingProgress(true);
			_currentTaskingMethodName = nameof(ApplyFiltersAsync);

			try
			{
				await LoadUserStarredRepositoriesAsync(Login);
				IsEmpty = Repositories.Count == 0;
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

		private async Task LoadLanguageOptionsAsync()
		{
			if (_languagesLoaded)
				return;

			try
			{
				var languages = await _gitHub.Users.StarredRepositories.GetLanguagesAsync(Login);
				AddLanguageOptions(languages);
				_languagesLoaded = true;
			}
			catch (Exception ex)
			{
				_logger?.Warn("Failed to load starred repository language filters: {0}", ex.Message);
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

		private static bool RequiresAllRepositories(StarredRepositoryListFilters filters)
			=> !string.IsNullOrWhiteSpace(filters.SearchText)
			|| !string.IsNullOrWhiteSpace(filters.Language)
			|| filters.Type != UserRepositoryTypeFilter.All
			|| filters.Sort != StarredRepositorySort.RecentlyStarred;

		private static OctokitGraphQLModel.StarOrder CreateStarOrder()
			=> new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.StarOrderField.StarredAt,
			};
	}
}
