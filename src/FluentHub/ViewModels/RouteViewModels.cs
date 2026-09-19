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

public abstract partial class RouteViewModelBase : ObservableObject
{
	private CancellationTokenSource? _loadCancellation;
	private Task? _loadTask;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsLoading))]
	[NotifyPropertyChangedFor(nameof(IsLoaded))]
	[NotifyPropertyChangedFor(nameof(HasError))]
	[NotifyCanExecuteChangedFor(nameof(RetryCommand))]
	public partial LoadingState LoadingState { get; private set; } = LoadingState.Loading;

	public bool IsLoading => LoadingState == LoadingState.Loading;

	public bool IsLoaded => LoadingState == LoadingState.Loaded;

	[ObservableProperty]
	public partial string? ErrorMessage { get; private set; }

	public bool HasError => LoadingState == LoadingState.Error;

	public async Task LoadAsync()
	{
		if (IsLoaded || _loadTask is not null)
		{
			return;
		}

		var loadTask = LoadInternalAsync();
		_loadTask = loadTask;
		try
		{
			await loadTask;
		}
		finally
		{
			if (ReferenceEquals(_loadTask, loadTask))
			{
				_loadTask = null;
			}
		}
	}

	[RelayCommand(CanExecute = nameof(CanRetry))]
	public async Task RetryAsync()
	{
		if (IsLoading)
		{
			return;
		}

		LoadingState = LoadingState.Loading;
		await LoadAsync();
	}

	private bool CanRetry()
	{
		return !IsLoading;
	}

	public void CancelLoading()
	{
		_loadCancellation?.Cancel();
	}

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
			{
				_loadCancellation = null;
			}
		}
	}
}

public sealed partial class IssueViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Title))]
	[NotifyPropertyChangedFor(nameof(RepositoryName))]
	[NotifyPropertyChangedFor(nameof(Body))]
	[NotifyPropertyChangedFor(nameof(StateLabel))]
	[NotifyPropertyChangedFor(nameof(StateStatus))]
	[NotifyPropertyChangedFor(nameof(AuthorLogin))]
	[NotifyPropertyChangedFor(nameof(AuthorName))]
	[NotifyPropertyChangedFor(nameof(AuthorAvatarUrl))]
	[NotifyPropertyChangedFor(nameof(CreatedAt))]
	[NotifyPropertyChangedFor(nameof(IsBodyEdited))]
	[NotifyPropertyChangedFor(nameof(HasAuthor))]
	[NotifyPropertyChangedFor(nameof(UpdatedAt))]
	private partial Issue? LoadedIssue { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(TimelineItems))]
	private partial IReadOnlyList<TimelineItemViewModel> LoadedTimelineItems { get; set; } = [];

	public IssueViewModel(IFluentHubGitHubClient gitHub, RepositoryIssueRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryIssueRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> LoadedIssue?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_IssueFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => GetRepositoryName(LoadedIssue?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(LoadedIssue?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: LoadedIssue.Body;

	public string StateLabel
		=> LoadedIssue?.State switch
		{
			IssueState.Open => Strings.Common_Open.GetLocalized(),
			IssueState.Closed => Strings.Common_Closed.GetLocalized(),
			_ => Strings.Common_Loading.GetLocalized(),
		};

	public string StateStatus
		=> LoadedIssue?.StateReason == IssueStateReason.NotPlanned
			? "issueClosedNotPlanned"
			: LoadedIssue?.State switch
			{
				IssueState.Open => "issueOpened",
				IssueState.Closed => "issueClosed",
				_ => "unavailable",
			};

	public string AuthorLogin => LoadedIssue?.Author?.Login ?? string.Empty;

	public string AuthorName => string.IsNullOrWhiteSpace(AuthorLogin)
		? Strings.Common_UnknownUser.GetLocalized()
		: AuthorLogin;

	public string AuthorAvatarUrl => LoadedIssue?.Author?.AvatarUrl ?? string.Empty;

	public string CreatedAt => GetUpdatedAt(LoadedIssue?.CreatedAt, LoadedIssue?.CreatedAtHumanized);

	public bool IsBodyEdited => LoadedIssue?.LastEditedAt is not null;

	public IReadOnlyList<TimelineItemViewModel> TimelineItems => LoadedTimelineItems;

	public bool HasAuthor => !string.IsNullOrWhiteSpace(AuthorLogin);

	public string UpdatedAt => GetUpdatedAt(LoadedIssue?.UpdatedAt, LoadedIssue?.UpdatedAtHumanized);

	public AppRoute RepositoryRoute => new RepositoryRoute(Route.Repository, RepositorySection.Overview);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedIssue = await _gitHub.Repositories.Issues.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);

		try
		{
			LoadedTimelineItems = TimelineItemViewModel.Create(
				await _gitHub.Repositories.IssueEvents.GetAllAsync(
					Route.Repository.Owner,
					Route.Repository.Name,
					Route.Number,
					cancellationToken),
				Route.Repository);
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			LoadedTimelineItems = [];
		}
	}

	internal static string GetRepositoryName(Repository? repository, RepositorySlug fallback)
	{
		return repository?.Owner?.Login is { Length: > 0 } owner &&
				repository.Name is { Length: > 0 } name
				? $"{owner}/{name}"
				: fallback.ToString();
	}

	internal static string GetUpdatedAt(DateTimeOffset? updatedAt, string? humanized)
	{
		return humanized ?? updatedAt?.ToLocalTime().ToString("g") ?? string.Empty;
	}
}

public sealed partial class PullRequestViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Title))]
	[NotifyPropertyChangedFor(nameof(RepositoryName))]
	[NotifyPropertyChangedFor(nameof(Body))]
	[NotifyPropertyChangedFor(nameof(StateLabel))]
	[NotifyPropertyChangedFor(nameof(StateStatus))]
	[NotifyPropertyChangedFor(nameof(AuthorName))]
	[NotifyPropertyChangedFor(nameof(AuthorAvatarUrl))]
	[NotifyPropertyChangedFor(nameof(CreatedAt))]
	[NotifyPropertyChangedFor(nameof(IsBodyEdited))]
	private partial PullRequest? LoadedPullRequest { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(TimelineItems))]
	private partial IReadOnlyList<TimelineItemViewModel> LoadedTimelineItems { get; set; } = [];

	public PullRequestViewModel(IFluentHubGitHubClient gitHub, RepositoryPullRequestRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryPullRequestRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> LoadedPullRequest?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_PullRequestFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => IssueViewModel.GetRepositoryName(LoadedPullRequest?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(LoadedPullRequest?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: LoadedPullRequest.Body;

	public string StateLabel
		=> LoadedPullRequest?.IsDraft == true
			? Strings.RouteViewModel_PullRequestDraftState.GetLocalized()
			: LoadedPullRequest?.State switch
			{
				PullRequestState.Open => Strings.Common_Open.GetLocalized(),
				PullRequestState.Closed => Strings.Common_Closed.GetLocalized(),
				PullRequestState.Merged => Strings.RouteViewModel_PullRequestMergedState.GetLocalized(),
				_ => Strings.Common_Loading.GetLocalized(),
			};

	public string StateStatus
		=> LoadedPullRequest?.IsDraft == true
			? "draft"
			: LoadedPullRequest?.State switch
			{
				PullRequestState.Open => "pullOpened",
				PullRequestState.Closed => "pullClosed",
				PullRequestState.Merged => "pullMerged",
				_ => "unavailable",
			};

	public string AuthorName
	{
		get
		{
			var authorLogin = LoadedPullRequest?.Author?.Login;
			return string.IsNullOrWhiteSpace(authorLogin)
				? Strings.Common_UnknownUser.GetLocalized()
				: authorLogin;
		}
	}

	public string AuthorAvatarUrl => LoadedPullRequest?.Author?.AvatarUrl ?? string.Empty;

	public string CreatedAt => IssueViewModel.GetUpdatedAt(LoadedPullRequest?.CreatedAt, LoadedPullRequest?.CreatedAtHumanized);

	public bool IsBodyEdited => LoadedPullRequest?.LastEditedAt is not null;

	public IReadOnlyList<TimelineItemViewModel> TimelineItems => LoadedTimelineItems;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedPullRequest = await _gitHub.Repositories.PullRequests.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);

		try
		{
			LoadedTimelineItems = TimelineItemViewModel.Create(
				await _gitHub.Repositories.PullRequestEvents.GetAllAsync(
					Route.Repository.Owner,
					Route.Repository.Name,
					Route.Number,
					cancellationToken),
				Route.Repository);
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			LoadedTimelineItems = [];
		}
	}
}

public sealed partial class DiscussionViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Title))]
	[NotifyPropertyChangedFor(nameof(RepositoryName))]
	[NotifyPropertyChangedFor(nameof(Body))]
	[NotifyPropertyChangedFor(nameof(CategoryName))]
	[NotifyPropertyChangedFor(nameof(AuthorLogin))]
	[NotifyPropertyChangedFor(nameof(HasAuthor))]
	[NotifyPropertyChangedFor(nameof(StateLabel))]
	[NotifyPropertyChangedFor(nameof(EngagementSummary))]
	[NotifyPropertyChangedFor(nameof(UpdatedAt))]
	private partial Discussion? LoadedDiscussion { get; set; }

	public DiscussionViewModel(IFluentHubGitHubClient gitHub, RepositoryDiscussionRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryDiscussionRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Title
		=> LoadedDiscussion?.Title ?? string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_DiscussionFallbackTitle.GetLocalized(),
			Route.Number);

	public string RepositoryName => IssueViewModel.GetRepositoryName(LoadedDiscussion?.Repository, Route.Repository);

	public string Body => string.IsNullOrWhiteSpace(LoadedDiscussion?.Body)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: LoadedDiscussion.Body;

	public string CategoryName => LoadedDiscussion?.Category?.Name ?? string.Empty;

	public string AuthorLogin => LoadedDiscussion?.Author?.Login ?? string.Empty;

	public bool HasAuthor => !string.IsNullOrWhiteSpace(AuthorLogin);

	public string StateLabel => LoadedDiscussion?.Closed == true
		? Strings.RouteViewModel_DiscussionClosedState.GetLocalized()
		: Strings.RouteViewModel_DiscussionOpenState.GetLocalized();

	public string EngagementSummary => LoadedDiscussion is null
		? string.Empty
		: string.Format(
			CultureInfo.CurrentCulture,
			Strings.RouteViewModel_DiscussionEngagementSummary.GetLocalized(),
			LoadedDiscussion.UpvoteCount);

	public string UpdatedAt => IssueViewModel.GetUpdatedAt(LoadedDiscussion?.UpdatedAt, LoadedDiscussion?.UpdatedAtHumanized);

	public AppRoute RepositoryRoute => new RepositoryRoute(Route.Repository, RepositorySection.Overview);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedDiscussion = await _gitHub.Repositories.Discussions.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			Route.Number,
			cancellationToken);
	}
}

public sealed partial class RepositoryViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Name))]
	[NotifyPropertyChangedFor(nameof(FullName))]
	[NotifyPropertyChangedFor(nameof(Description))]
	[NotifyPropertyChangedFor(nameof(HasDescription))]
	[NotifyPropertyChangedFor(nameof(DefaultBranch))]
	[NotifyPropertyChangedFor(nameof(OpenIssues))]
	[NotifyPropertyChangedFor(nameof(OpenPullRequests))]
	[NotifyPropertyChangedFor(nameof(Stars))]
	[NotifyPropertyChangedFor(nameof(Forks))]
	[NotifyPropertyChangedFor(nameof(IsPrivate))]
	private partial Repository? LoadedRepository { get; set; }

	public RepositoryViewModel(IFluentHubGitHubClient gitHub, RepositoryRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public RepositoryRoute Route { get; }

	public RepositorySlug Repository => Route.Repository;

	public string Name => LoadedRepository?.Name ?? Route.Repository.Name;

	public string FullName => IssueViewModel.GetRepositoryName(LoadedRepository, Route.Repository);

	public string Description => string.IsNullOrWhiteSpace(LoadedRepository?.Description)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: LoadedRepository.Description;

	public bool HasDescription => !string.IsNullOrWhiteSpace(LoadedRepository?.Description);

	public string DefaultBranch => LoadedRepository?.DefaultBranchRef?.Name ?? string.Empty;

	public int OpenIssues => LoadedRepository?.Issues?.TotalCount ?? 0;

	public int OpenPullRequests => LoadedRepository?.PullRequests?.TotalCount ?? 0;

	public int Stars => LoadedRepository?.StargazerCount ?? 0;

	public int Forks => LoadedRepository?.ForkCount ?? 0;

	public bool IsPrivate => LoadedRepository?.IsPrivate == true;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedRepository = await _gitHub.Repositories.Repositories.GetAsync(
			Route.Repository.Owner,
			Route.Repository.Name,
			cancellationToken);
	}
}

public sealed partial class UserViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Login))]
	[NotifyPropertyChangedFor(nameof(DisplayName))]
	[NotifyPropertyChangedFor(nameof(Bio))]
	[NotifyPropertyChangedFor(nameof(Location))]
	[NotifyPropertyChangedFor(nameof(Website))]
	[NotifyPropertyChangedFor(nameof(HasLocation))]
	[NotifyPropertyChangedFor(nameof(HasWebsite))]
	[NotifyPropertyChangedFor(nameof(Followers))]
	[NotifyPropertyChangedFor(nameof(Following))]
	private partial User? LoadedUser { get; set; }

	public UserViewModel(IFluentHubGitHubClient gitHub, UserRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public UserRoute Route { get; }

	public string Login => LoadedUser?.Login ?? Route.Login;

	public string DisplayName => string.IsNullOrWhiteSpace(LoadedUser?.Name) ? Login : LoadedUser.Name;

	public string Bio => string.IsNullOrWhiteSpace(LoadedUser?.Bio)
		? Strings.RouteViewModel_UserNoBioProvided.GetLocalized()
		: LoadedUser.Bio;

	public string Location => LoadedUser?.Location ?? string.Empty;

	public string Website => LoadedUser?.WebsiteUrl ?? string.Empty;

	public bool HasLocation => !string.IsNullOrWhiteSpace(Location);

	public bool HasWebsite => !string.IsNullOrWhiteSpace(Website);

	public int Followers => LoadedUser?.Followers?.TotalCount ?? 0;

	public int Following => LoadedUser?.Following?.TotalCount ?? 0;

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedUser = await _gitHub.Users.Users.GetAsync(Route.Login, cancellationToken);
	}
}

public sealed partial class OrganizationViewModel : RouteViewModelBase
{
	private readonly IFluentHubGitHubClient _gitHub;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Login))]
	[NotifyPropertyChangedFor(nameof(DisplayName))]
	[NotifyPropertyChangedFor(nameof(Description))]
	[NotifyPropertyChangedFor(nameof(Location))]
	[NotifyPropertyChangedFor(nameof(Website))]
	[NotifyPropertyChangedFor(nameof(HasLocation))]
	[NotifyPropertyChangedFor(nameof(HasWebsite))]
	private partial Organization? LoadedOrganization { get; set; }

	public OrganizationViewModel(IFluentHubGitHubClient gitHub, OrganizationRoute route)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		Route = route;
	}

	public OrganizationRoute Route { get; }

	public string Login => LoadedOrganization?.Login ?? Route.Login;

	public string DisplayName => string.IsNullOrWhiteSpace(LoadedOrganization?.Name) ? Login : LoadedOrganization.Name;

	public string Description => string.IsNullOrWhiteSpace(LoadedOrganization?.Description)
		? Strings.Common_NoDescriptionProvided.GetLocalized()
		: LoadedOrganization.Description;

	public string Location => LoadedOrganization?.Location ?? string.Empty;

	public string Website => LoadedOrganization?.WebsiteUrl ?? string.Empty;

	public bool HasLocation => !string.IsNullOrWhiteSpace(Location);

	public bool HasWebsite => !string.IsNullOrWhiteSpace(Website);

	protected override async Task LoadCoreAsync(CancellationToken cancellationToken)
	{
		LoadedOrganization = await _gitHub.Organizations.Organizations.GetAsync(Route.Login, cancellationToken);
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
	{
		return Kind switch
		{
			RepositoryItemListKind.Issues => new RepositoryIssueRoute(item.Repository, item.Number),
			RepositoryItemListKind.PullRequests => new RepositoryPullRequestRoute(item.Repository, item.Number),
			RepositoryItemListKind.Discussions => new RepositoryDiscussionRoute(item.Repository, item.Number),
			_ => throw new ArgumentOutOfRangeException(),
		};
	}

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
	}

	private async Task LoadRepositoryItemsAsync(RepositorySlug repository, CancellationToken cancellationToken)
	{
		var page = PageRequest.Forward(30);
		var filters = new RepositoryItemListFilters { State = RepositoryItemStateFilter.All };

		switch (Kind)
		{
			case RepositoryItemListKind.Issues:
				foreach (var issue in (await _gitHub.Repositories.Issues.GetPageAsync(repository.Owner, repository.Name, page, filters, cancellationToken)).Items)
				{
					AddIssue(issue, repository);
				}

				break;
			case RepositoryItemListKind.PullRequests:
				foreach (var pullRequest in (await _gitHub.Repositories.PullRequests.GetPageAsync(repository.Owner, repository.Name, page, filters, cancellationToken)).Items)
				{
					AddPullRequest(pullRequest, repository);
				}

				break;
			case RepositoryItemListKind.Discussions:
				foreach (var discussion in (await _gitHub.Repositories.Discussions.GetPageAsync(
					repository.Owner,
					repository.Name,
					page,
					categoryId: null,
					orderBy: null,
					cancellationToken)).Items)
				{
					AddDiscussion(discussion, repository);
				}

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
				{
					AddIssue(issue, default);
				}

				break;
			case RepositoryItemListKind.PullRequests:
				foreach (var pullRequest in (await _gitHub.Users.PullRequests.GetPageAsync(login, page, filters, cancellationToken)).Items)
				{
					AddPullRequest(pullRequest, default);
				}

				break;
			case RepositoryItemListKind.Discussions:
				foreach (var discussion in (await _gitHub.Users.Discussions.GetPageAsync(
					login,
					page,
					answered: null,
					orderBy: null,
					repositoryId: null,
					cancellationToken)).Items)
				{
					AddDiscussion(discussion, default);
				}

				break;
		}
	}

	private void AddIssue(Issue issue, RepositorySlug fallback)
	{
		if (!TryGetRepositorySlug(issue.Repository, fallback, out var repository))
		{
			return;
		}

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
		{
			return;
		}

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
		{
			return;
		}

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
	{
		return Kind switch
		{
			RepositoryItemListKind.Issues => Strings.RepositoryItemListViewModel_IssuesTitle.GetLocalized(),
			RepositoryItemListKind.PullRequests => Strings.RepositoryItemListViewModel_PullRequestsTitle.GetLocalized(),
			RepositoryItemListKind.Discussions => Strings.RepositoryItemListViewModel_DiscussionsTitle.GetLocalized(),
			_ => Strings.RepositoryItemListViewModel_ItemsTitle.GetLocalized(),
		};
	}
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
			{
				continue;
			}

			var slug = new RepositorySlug(owner, name);
			Items.Add(new RepositoryListItemViewModel(
				slug,
				name,
				$"{owner}/{name}",
				repository.Description ?? Strings.Common_NoDescriptionProvided.GetLocalized(),
				repository.IsPrivate));
		}
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
