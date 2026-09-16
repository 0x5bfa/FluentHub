// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentHub.Core;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels;

public abstract class RouteViewModelBase : ObservableObject
{
	private LoadingState _loadingState = LoadingState.Loading;
	private string? _errorMessage;
	private CancellationTokenSource? _loadCancellation;
	private Task? _loadTask;

	protected RouteViewModelBase()
	{
		RetryCommand = new AsyncRelayCommand(
			RetryAsync,
			() => LoadingState != LoadingState.Loading);
	}

	public LoadingState LoadingState
	{
		get => _loadingState;
		private set
		{
			if (!SetProperty(ref _loadingState, value))
				return;

			OnPropertyChanged(nameof(IsLoading));
			OnPropertyChanged(nameof(IsLoaded));
			OnPropertyChanged(nameof(HasError));
			RetryCommand.NotifyCanExecuteChanged();
		}
	}

	public bool IsLoading => LoadingState == LoadingState.Loading;

	public bool IsLoaded => LoadingState == LoadingState.Loaded;

	public string? ErrorMessage
	{
		get => _errorMessage;
		private set
		{
			SetProperty(ref _errorMessage, value);
		}
	}

	public bool HasError => LoadingState == LoadingState.Error;

	public AsyncRelayCommand RetryCommand { get; }

	public async Task LoadAsync()
	{
		if (IsLoaded || _loadTask is not null)
			return;

		var loadTask = LoadInternalAsync();
		_loadTask = loadTask;
		try
		{
			await loadTask;
		}
		finally
		{
			if (ReferenceEquals(_loadTask, loadTask))
				_loadTask = null;
		}
	}

	public async Task RetryAsync()
	{
		if (IsLoading)
			return;

		LoadingState = LoadingState.Loading;
		await LoadAsync();
	}

	public void CancelLoading()
		=> _loadCancellation?.Cancel();

	protected abstract Task LoadCoreAsync(CancellationToken cancellationToken);

	private async Task LoadInternalAsync()
	{
		using var cancellation = new CancellationTokenSource();
		_loadCancellation = cancellation;
		LoadingState = LoadingState.Loading;
		ErrorMessage = null;

		try
		{
			await LoadCoreAsync(cancellation.Token);
			cancellation.Token.ThrowIfCancellationRequested();
			LoadingState = LoadingState.Loaded;
		}
		catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
		{
			// The view was unloaded while the request was in flight.
		}
		catch (Exception)
		{
			ErrorMessage = Strings.RouteViewModelBase_LoadError.GetLocalized();
			LoadingState = LoadingState.Error;
		}
		finally
		{
			if (ReferenceEquals(_loadCancellation, cancellation))
				_loadCancellation = null;
		}
	}
}

public sealed class IssueViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private Issue? _issue;
	private IReadOnlyList<TimelineItemViewModel> _timelineItems = [];

	public IssueViewModel(IFluentHubGitHubClient gitHub, RepositoryIssueRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryIssueRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> _issue?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_IssueFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => GetRepositoryName(_issue?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(_issue?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: _issue.Body;

	public string StateLabel
		=> _issue?.State switch
		{
			IssueState.Open => Strings.Common_Open.GetLocalized(),
			IssueState.Closed => Strings.Common_Closed.GetLocalized(),
			_ => Strings.Common_Loading.GetLocalized(),
		};

	public string AuthorLogin => _issue?.Author?.Login ?? string.Empty;

	public string AuthorName => string.IsNullOrWhiteSpace(AuthorLogin)
		? Strings.Common_UnknownUser.GetLocalized()
		: AuthorLogin;

	public string AuthorAvatarUrl => _issue?.Author?.AvatarUrl ?? string.Empty;

	public string CreatedAt => GetUpdatedAt(_issue?.CreatedAt, _issue?.CreatedAtHumanized);

	public bool IsBodyEdited => _issue?.LastEditedAt is not null;

	public IReadOnlyList<TimelineItemViewModel> TimelineItems => _timelineItems;

	public bool HasAuthor => !string.IsNullOrWhiteSpace(AuthorLogin);

	public string UpdatedAt => GetUpdatedAt(_issue?.UpdatedAt, _issue?.UpdatedAtHumanized);

	public AppRoute RepositoryRoute => new RepositoryRoute(Route.Repository, RepositorySection.Overview);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_issue = await _gitHub.Repositories.Issues.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);

		try
		{
			_timelineItems = TimelineItemViewModel.Create(await _gitHub.Repositories.IssueEvents.GetAllAsync(
				Route.Repository.Owner,
				Route.Repository.Name,
				Route.Number,
				cancellationToken));
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			_timelineItems = [];
		}

		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Title));
		OnPropertyChanged(nameof(RepositoryName));
		OnPropertyChanged(nameof(Body));
		OnPropertyChanged(nameof(StateLabel));
		OnPropertyChanged(nameof(AuthorLogin));
		OnPropertyChanged(nameof(AuthorName));
		OnPropertyChanged(nameof(AuthorAvatarUrl));
		OnPropertyChanged(nameof(CreatedAt));
		OnPropertyChanged(nameof(IsBodyEdited));
		OnPropertyChanged(nameof(TimelineItems));
		OnPropertyChanged(nameof(HasAuthor));
		OnPropertyChanged(nameof(UpdatedAt));
	}

	internal static string GetRepositoryName(Repository? repository, RepositorySlug fallback)
		=> repository?.Owner?.Login is { Length: > 0 } owner &&
			repository.Name is { Length: > 0 } name
			? $"{owner}/{name}"
			: fallback.ToString();

	internal static string GetUpdatedAt(DateTimeOffset? updatedAt, string? humanized)
		=> humanized ?? updatedAt?.ToLocalTime().ToString("g") ?? string.Empty;
}

public sealed class PullRequestViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private PullRequest? _pullRequest;
	private IReadOnlyList<TimelineItemViewModel> _timelineItems = [];

	public PullRequestViewModel(IFluentHubGitHubClient gitHub, RepositoryPullRequestRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryPullRequestRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> _pullRequest?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_PullRequestFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => IssueViewModel.GetRepositoryName(_pullRequest?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(_pullRequest?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: _pullRequest.Body;

	public string StateLabel
		=> _pullRequest is { IsDraft: true }
			? Strings.RouteViewModel_PullRequestDraftState.GetLocalized()
			: _pullRequest?.Merged == true
				? Strings.RouteViewModel_PullRequestMergedState.GetLocalized()
				: _pullRequest?.State switch
				{
					PullRequestState.Open => Strings.Common_Open.GetLocalized(),
					PullRequestState.Closed => Strings.Common_Closed.GetLocalized(),
					_ => Strings.Common_Loading.GetLocalized(),
				};

	public string AuthorLogin => _pullRequest?.Author?.Login ?? string.Empty;

	public string AuthorName => string.IsNullOrWhiteSpace(AuthorLogin)
		? Strings.Common_UnknownUser.GetLocalized()
		: AuthorLogin;

	public string AuthorAvatarUrl => _pullRequest?.Author?.AvatarUrl ?? string.Empty;

	public string CreatedAt => IssueViewModel.GetUpdatedAt(_pullRequest?.CreatedAt, _pullRequest?.CreatedAtHumanized);

	public bool IsBodyEdited => _pullRequest?.LastEditedAt is not null;

	public IReadOnlyList<TimelineItemViewModel> TimelineItems => _timelineItems;

	public bool HasAuthor => !string.IsNullOrWhiteSpace(AuthorLogin);

	public string BranchSummary
		=> _pullRequest is null
			? string.Empty
			: string.Format(
				CultureInfo.CurrentCulture,
				Strings.RouteViewModel_PullRequestBranchSummary.GetLocalized(),
				_pullRequest.HeadRefName,
				_pullRequest.BaseRefName);

	public string ChangeSummary
		=> _pullRequest is null
			? string.Empty
			: string.Format(
				CultureInfo.CurrentCulture,
				Strings.RouteViewModel_PullRequestChangeSummary.GetLocalized(),
				_pullRequest.Additions,
				_pullRequest.Deletions,
				_pullRequest.ChangedFiles);

	public string UpdatedAt => IssueViewModel.GetUpdatedAt(_pullRequest?.UpdatedAt, _pullRequest?.UpdatedAtHumanized);

	public AppRoute RepositoryRoute => new RepositoryRoute(Route.Repository, RepositorySection.Overview);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_pullRequest = await _gitHub.Repositories.PullRequests.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);

		try
		{
			_timelineItems = TimelineItemViewModel.Create(await _gitHub.Repositories.PullRequestEvents.GetAllAsync(
				Route.Repository.Owner,
				Route.Repository.Name,
				Route.Number,
				cancellationToken));
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			_timelineItems = [];
		}

		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Title));
		OnPropertyChanged(nameof(RepositoryName));
		OnPropertyChanged(nameof(Body));
		OnPropertyChanged(nameof(StateLabel));
		OnPropertyChanged(nameof(AuthorLogin));
		OnPropertyChanged(nameof(AuthorName));
		OnPropertyChanged(nameof(AuthorAvatarUrl));
		OnPropertyChanged(nameof(CreatedAt));
		OnPropertyChanged(nameof(IsBodyEdited));
		OnPropertyChanged(nameof(TimelineItems));
		OnPropertyChanged(nameof(HasAuthor));
		OnPropertyChanged(nameof(BranchSummary));
		OnPropertyChanged(nameof(ChangeSummary));
		OnPropertyChanged(nameof(UpdatedAt));
	}
}

public sealed class DiscussionViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private Discussion? _discussion;

	public DiscussionViewModel(IFluentHubGitHubClient gitHub, RepositoryDiscussionRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryDiscussionRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> _discussion?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_DiscussionFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => IssueViewModel.GetRepositoryName(_discussion?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(_discussion?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: _discussion.Body;

	public string CategoryName => _discussion?.Category?.Name ?? string.Empty;

	public string AuthorLogin => _discussion?.Author?.Login ?? string.Empty;

	public bool HasAuthor => !string.IsNullOrWhiteSpace(AuthorLogin);

	public string StateLabel => _discussion?.Closed == true
		? Strings.RouteViewModel_DiscussionClosedState.GetLocalized()
		: Strings.RouteViewModel_DiscussionOpenState.GetLocalized();

	public string EngagementSummary => _discussion is null
		? string.Empty
		: string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_DiscussionEngagementSummary.GetLocalized(),
			_discussion.UpvoteCount);

	public string UpdatedAt => IssueViewModel.GetUpdatedAt(_discussion?.UpdatedAt, _discussion?.UpdatedAtHumanized);

	public AppRoute RepositoryRoute => new RepositoryRoute(Route.Repository, RepositorySection.Overview);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_discussion = await _gitHub.Repositories.Discussions.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);
		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Title));
		OnPropertyChanged(nameof(RepositoryName));
		OnPropertyChanged(nameof(Body));
		OnPropertyChanged(nameof(CategoryName));
		OnPropertyChanged(nameof(AuthorLogin));
		OnPropertyChanged(nameof(HasAuthor));
		OnPropertyChanged(nameof(StateLabel));
		OnPropertyChanged(nameof(EngagementSummary));
		OnPropertyChanged(nameof(UpdatedAt));
	}
}

public sealed class RepositoryViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private Repository? _repository;

	public RepositoryViewModel(IFluentHubGitHubClient gitHub, RepositoryRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Name => _repository?.Name ?? Route.Repository.Name;

	public string FullName => IssueViewModel.GetRepositoryName(_repository, Route.Repository);

	public string Description => string.IsNullOrWhiteSpace(_repository?.Description)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: _repository.Description;

	public bool HasDescription => !string.IsNullOrWhiteSpace(_repository?.Description);

	public string DefaultBranch => _repository?.DefaultBranchRef?.Name ?? string.Empty;

	public int OpenIssues => _repository?.Issues?.TotalCount ?? 0;

	public int OpenPullRequests => _repository?.PullRequests?.TotalCount ?? 0;

	public int Stars => _repository?.StargazerCount ?? 0;

	public int Forks => _repository?.ForkCount ?? 0;

	public bool IsPrivate => _repository?.IsPrivate == true;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_repository = await _gitHub.Repositories.Repositories.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			cancellationToken);
		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Name));
		OnPropertyChanged(nameof(FullName));
		OnPropertyChanged(nameof(Description));
		OnPropertyChanged(nameof(HasDescription));
		OnPropertyChanged(nameof(DefaultBranch));
		OnPropertyChanged(nameof(OpenIssues));
		OnPropertyChanged(nameof(OpenPullRequests));
		OnPropertyChanged(nameof(Stars));
		OnPropertyChanged(nameof(Forks));
		OnPropertyChanged(nameof(IsPrivate));
	}
}

public sealed class UserViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private User? _user;

	public UserViewModel(IFluentHubGitHubClient gitHub, UserRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public UserRoute Route { get; }

	public string Login => _user?.Login ?? Route.Login;

	public string DisplayName => string.IsNullOrWhiteSpace(_user?.Name) ? Login : _user.Name;

	public string Bio => string.IsNullOrWhiteSpace(_user?.Bio)
		? Strings.RouteViewModel_UserNoBioProvided.GetLocalized()
		: _user.Bio;

	public string Location => _user?.Location ?? string.Empty;

	public string Website => _user?.WebsiteUrl ?? string.Empty;

	public bool HasLocation => !string.IsNullOrWhiteSpace(Location);

	public bool HasWebsite => !string.IsNullOrWhiteSpace(Website);

	public int Followers => _user?.Followers?.TotalCount ?? 0;

	public int Following => _user?.Following?.TotalCount ?? 0;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_user = await _gitHub.Users.Users.GetAsync(Route.Login, cancellationToken);
		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Login));
		OnPropertyChanged(nameof(DisplayName));
		OnPropertyChanged(nameof(Bio));
		OnPropertyChanged(nameof(Location));
		OnPropertyChanged(nameof(Website));
		OnPropertyChanged(nameof(HasLocation));
		OnPropertyChanged(nameof(HasWebsite));
		OnPropertyChanged(nameof(Followers));
		OnPropertyChanged(nameof(Following));
	}
}

public sealed class OrganizationViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private Organization? _organization;

	public OrganizationViewModel(IFluentHubGitHubClient gitHub, OrganizationRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public OrganizationRoute Route { get; }

	public string Login => _organization?.Login ?? Route.Login;

	public string DisplayName => string.IsNullOrWhiteSpace(_organization?.Name) ? Login : _organization.Name;

	public string Description => string.IsNullOrWhiteSpace(_organization?.Description)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: _organization.Description;

	public string Location => _organization?.Location ?? string.Empty;

	public string Website => _organization?.WebsiteUrl ?? string.Empty;

	public bool HasLocation => !string.IsNullOrWhiteSpace(Location);

	public bool HasWebsite => !string.IsNullOrWhiteSpace(Website);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		_organization = await _gitHub.Organizations.Organizations.GetAsync(Route.Login, cancellationToken);
		NotifyDetailsChanged();
	}

	private void NotifyDetailsChanged()
	{
		OnPropertyChanged(nameof(Login));
		OnPropertyChanged(nameof(DisplayName));
		OnPropertyChanged(nameof(Description));
		OnPropertyChanged(nameof(Location));
		OnPropertyChanged(nameof(Website));
		OnPropertyChanged(nameof(HasLocation));
		OnPropertyChanged(nameof(HasWebsite));
	}
}

public enum RepositoryItemListKind
{
	Issues,
	PullRequests,
	Discussions,
}

public sealed class RouteListItemViewModel
{
	public RouteListItemViewModel(
		RepositorySlug repository,
		int number,
		string title,
		string repositoryName,
		string updatedAt)
	{
		Repository = repository;
		Number = number;
		Title = title;
		RepositoryName = repositoryName;
		UpdatedAt = updatedAt;
	}

	public RepositorySlug Repository { get; }

	public int Number { get; }

	public string Title { get; }

	public string RepositoryName { get; }

	public string UpdatedAt { get; }
}

public sealed class RepositoryItemListViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;
	private readonly AppRoute _route;

	public RepositoryItemListViewModel(
		IFluentHubGitHubClient gitHub,
		AppRoute route,
		RepositoryItemListKind kind)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		_route = route ?? throw new ArgumentNullException(nameof(route));
		Kind = kind;
	}

	public RepositoryItemListKind Kind { get; }

	public ObservableCollection<RouteListItemViewModel> Items { get; } = [];

	public string Title
		=> _route switch
		{
			RepositoryRoute route => string.Format(
				CultureInfo.CurrentCulture,
				Strings.RepositoryItemListViewModel_RepositoryTitle.GetLocalized(),
				route.Repository,
				GetKindTitle()),
			UserRoute route => string.Format(
				CultureInfo.CurrentCulture,
				Strings.RepositoryItemListViewModel_UserTitle.GetLocalized(),
				route.Login,
				GetKindTitle()),
			_ => GetKindTitle(),
		};

	public string EmptyText => string.Format(
		CultureInfo.CurrentCulture,
		Strings.RepositoryItemListViewModel_EmptyText.GetLocalized(),
		GetKindTitle().ToLower(CultureInfo.CurrentCulture));

	public bool IsEmpty => IsLoaded && Items.Count == 0;

	public AppRoute CreateRoute(RouteListItemViewModel item)
		=> Kind switch
		{
			RepositoryItemListKind.Issues => new RepositoryIssueRoute(item.Repository, item.Number),
			RepositoryItemListKind.PullRequests => new RepositoryPullRequestRoute(item.Repository, item.Number),
			RepositoryItemListKind.Discussions => new RepositoryDiscussionRoute(item.Repository, item.Number),
			_ => throw new ArgumentOutOfRangeException(),
		};

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		Items.Clear();

		switch (_route)
		{
			case RepositoryRoute repository:
				await LoadRepositoryItemsAsync(repository.Repository, cancellationToken);
				break;
			case UserRoute user:
				await LoadUserItemsAsync(user.Login, cancellationToken);
				break;
			default:
				throw new InvalidOperationException("The selected route does not represent an item list.");
		}

		OnPropertyChanged(nameof(IsEmpty));
	}

	private async Task LoadRepositoryItemsAsync(RepositorySlug repository, CancellationToken cancellationToken)
	{
		var page = PageRequest.Forward(30);
		var filters = new RepositoryItemListFilters { State = RepositoryItemStateFilter.All };

		switch (Kind)
		{
			case RepositoryItemListKind.Issues:
				foreach (var issue in (await _gitHub.Repositories.Issues.GetPageAsync(repository.Owner, repository.Name, page, filters, cancellationToken)).Items)
					AddIssue(issue, repository);
				break;
			case RepositoryItemListKind.PullRequests:
				foreach (var pullRequest in (await _gitHub.Repositories.PullRequests.GetPageAsync(repository.Owner, repository.Name, page, filters, cancellationToken)).Items)
					AddPullRequest(pullRequest, repository);
				break;
			case RepositoryItemListKind.Discussions:
				foreach (var discussion in (await _gitHub.Repositories.Discussions.GetPageAsync(
					repository.Owner,
					repository.Name,
					page,
					categoryId: null,
					orderBy: null,
					cancellationToken)).Items)
					AddDiscussion(discussion, repository);
				break;
		}
	}

	private async Task LoadUserItemsAsync(string login, CancellationToken cancellationToken)
	{
		var page = PageRequest.Forward(30);
		var filters = new RepositoryItemListFilters { State = RepositoryItemStateFilter.All };

		switch (Kind)
		{
			case RepositoryItemListKind.Issues:
				foreach (var issue in (await _gitHub.Users.Issues.GetPageAsync(login, page, filters, cancellationToken)).Items)
					AddIssue(issue, default);
				break;
			case RepositoryItemListKind.PullRequests:
				foreach (var pullRequest in (await _gitHub.Users.PullRequests.GetPageAsync(login, page, filters, cancellationToken)).Items)
					AddPullRequest(pullRequest, default);
				break;
			case RepositoryItemListKind.Discussions:
				foreach (var discussion in (await _gitHub.Users.Discussions.GetPageAsync(
					login,
					page,
					answered: null,
					orderBy: null,
					repositoryId: null,
					cancellationToken)).Items)
					AddDiscussion(discussion, default);
				break;
		}
	}

	private void AddIssue(Issue issue, RepositorySlug fallback)
	{
		if (!TryGetRepositorySlug(issue.Repository, fallback, out var repository))
			return;

		Items.Add(new RouteListItemViewModel(
			repository,
			issue.Number,
			issue.Title,
			IssueViewModel.GetRepositoryName(issue.Repository, repository),
			IssueViewModel.GetUpdatedAt(issue.UpdatedAt, issue.UpdatedAtHumanized)));
	}

	private void AddPullRequest(PullRequest pullRequest, RepositorySlug fallback)
	{
		if (!TryGetRepositorySlug(pullRequest.Repository, fallback, out var repository))
			return;

		Items.Add(new RouteListItemViewModel(
			repository,
			pullRequest.Number,
			pullRequest.Title,
			IssueViewModel.GetRepositoryName(pullRequest.Repository, repository),
			IssueViewModel.GetUpdatedAt(pullRequest.UpdatedAt, pullRequest.UpdatedAtHumanized)));
	}

	private void AddDiscussion(Discussion discussion, RepositorySlug fallback)
	{
		if (!TryGetRepositorySlug(discussion.Repository, fallback, out var repository))
			return;

		Items.Add(new RouteListItemViewModel(
			repository,
			discussion.Number,
			discussion.Title,
			IssueViewModel.GetRepositoryName(discussion.Repository, repository),
			IssueViewModel.GetUpdatedAt(discussion.UpdatedAt, discussion.UpdatedAtHumanized)));
	}

	private static bool TryGetRepositorySlug(
		Repository? repository,
		RepositorySlug fallback,
		out RepositorySlug slug)
	{
		if (repository?.Owner?.Login is { Length: > 0 } owner && repository.Name is { Length: > 0 } name)
		{
			slug = new RepositorySlug(owner, name);
			return true;
		}

		if (!string.IsNullOrWhiteSpace(fallback.Owner) && !string.IsNullOrWhiteSpace(fallback.Name))
		{
			slug = fallback;
			return true;
		}

		slug = default;
		return false;
	}

	private string GetKindTitle()
		=> Kind switch
		{
			RepositoryItemListKind.Issues => Strings.RepositoryItemListViewModel_IssuesTitle.GetLocalized(),
			RepositoryItemListKind.PullRequests => Strings.RepositoryItemListViewModel_PullRequestsTitle.GetLocalized(),
			RepositoryItemListKind.Discussions => Strings.RepositoryItemListViewModel_DiscussionsTitle.GetLocalized(),
			_ => Strings.RepositoryItemListViewModel_ItemsTitle.GetLocalized(),
		};
}

public enum RepositoryListOwnerKind
{
	User,
	Organization,
}

public sealed class RepositoryListItemViewModel
{
	public RepositoryListItemViewModel(RepositorySlug repository, string name, string fullName, string description, bool isPrivate)
	{
		Repository = repository;
		Name = name;
		FullName = fullName;
		Description = description;
		IsPrivate = isPrivate;
	}

	public RepositorySlug Repository { get; }

	public string Name { get; }

	public string FullName { get; }

	public string Description { get; }

	public bool IsPrivate { get; }
}

public sealed class RepositoryListViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	public RepositoryListViewModel(
		IFluentHubGitHubClient gitHub,
		string ownerLogin,
		RepositoryListOwnerKind ownerKind)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		OwnerLogin = ownerLogin ?? throw new ArgumentNullException(nameof(ownerLogin));
		OwnerKind = ownerKind;
	}

	public string OwnerLogin { get; }

	public RepositoryListOwnerKind OwnerKind { get; }

	public string Title => string.Format(
		CultureInfo.CurrentCulture,
		Strings.RepositoryListViewModel_Title.GetLocalized(),
		OwnerLogin);

	public ObservableCollection<RepositoryListItemViewModel> Items { get; } = [];

	public bool IsEmpty => IsLoaded && Items.Count == 0;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		Items.Clear();
		IReadOnlyList<Repository> repositories = OwnerKind switch
		{
			RepositoryListOwnerKind.User => (await _gitHub.Users.Repositories.GetPageAsync(
				OwnerLogin,
				PageRequest.Forward(30),
				cancellationToken: cancellationToken)).Items,
			RepositoryListOwnerKind.Organization => await _gitHub.Organizations.Repositories.GetAllAsync(OwnerLogin, cancellationToken),
			_ => [],
		};

		foreach (var repository in repositories)
		{
			if (repository.Owner?.Login is not { Length: > 0 } owner || repository.Name is not { Length: > 0 } name)
				continue;

			var slug = new RepositorySlug(owner, name);
			Items.Add(new RepositoryListItemViewModel(
				slug,
				name,
				$"{owner}/{name}",
				repository.Description ?? Strings.Common_NoDescriptionProvided.GetLocalized(),
				repository.IsPrivate));
		}

		OnPropertyChanged(nameof(IsEmpty));
	}
}

public sealed class ActionsViewModel
{
	public ActionsViewModel(RepositoryRoute route)
	{
		Route = route;
		RepositoryName = route.Repository.ToString();
		ActionsUrl = $"https://github.com/{Uri.EscapeDataString(route.Repository.Owner)}/{Uri.EscapeDataString(route.Repository.Name)}/actions";
	}

	public RepositoryRoute Route { get; }

	public string RepositoryName { get; }

	public string ActionsUrl { get; }
}
