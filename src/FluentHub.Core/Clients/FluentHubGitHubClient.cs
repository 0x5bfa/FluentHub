// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Caching;

namespace FluentHub.Core.Clients
{
	public sealed class FluentHubGitHubClient : IFluentHubGitHubClient
	{
		public FluentHubGitHubClient(IGitHubApiClient apiClient)
			: this(apiClient, null)
		{
		}

		public FluentHubGitHubClient(IGitHubApiClient apiClient, ICacheService? cache)
		{
			Organizations = new(apiClient, cache);
			Repositories = new(apiClient, cache);
			Users = new(apiClient, cache);
			Searches = new(apiClient);
			Mutations = new(apiClient);
		}

		public OrganizationApiClient Organizations { get; }

		public RepositoryApiClient Repositories { get; }

		public UserApiClient Users { get; }

		public SearchApiClient Searches { get; }

		public MutationApiClient Mutations { get; }
	}

	public sealed class OrganizationApiClient
	{
		internal OrganizationApiClient(IGitHubApiClient apiClient, ICacheService? cache)
		{
			Organizations = new(apiClient, cache);
			Packages = new(apiClient);
			PinnedItems = new(apiClient);
			ProjectsV2 = new(apiClient);
			Repositories = new(apiClient);
		}

		public Queries.Organizations.OrganizationQueries Organizations { get; }
		public Queries.Organizations.PackageQueries Packages { get; }
		public Queries.Organizations.PinnedItemQueries PinnedItems { get; }
		public Queries.Organizations.ProjectV2Queries ProjectsV2 { get; }
		public Queries.Organizations.RepositoryQueries Repositories { get; }
	}

	public sealed class RepositoryApiClient
	{
		internal RepositoryApiClient(IGitHubApiClient apiClient, ICacheService? cache)
		{
			Blobs = new(apiClient);
			Commits = new(apiClient);
			Diffs = new(apiClient);
			Discussions = new(apiClient);
			Insights = new(apiClient);
			IssueEvents = new(apiClient);
			Issues = new(apiClient);
			Packages = new(apiClient);
			ProjectsV2 = new(apiClient);
			PullRequestChecks = new(apiClient);
			PullRequestCommits = new(apiClient);
			PullRequestEvents = new(apiClient);
			PullRequests = new(apiClient);
			Releases = new(apiClient);
			Repositories = new(apiClient, cache);
			Trees = new(apiClient);
		}

		public Queries.Repositories.BlobQueries Blobs { get; }
		public Queries.Repositories.CommitQueries Commits { get; }
		public Queries.Repositories.DiffQueries Diffs { get; }
		public Queries.Repositories.DiscussionQueries Discussions { get; }
		public Queries.Repositories.InsightQueries Insights { get; }
		public Queries.Repositories.IssueEventQueries IssueEvents { get; }
		public Queries.Repositories.IssueQueries Issues { get; }
		public Queries.Repositories.PackageQueries Packages { get; }
		public Queries.Repositories.ProjectV2Queries ProjectsV2 { get; }
		public Queries.Repositories.PullRequestCheckQueries PullRequestChecks { get; }
		public Queries.Repositories.PullRequestCommitQueries PullRequestCommits { get; }
		public Queries.Repositories.PullRequestEventQueries PullRequestEvents { get; }
		public Queries.Repositories.PullRequestQueries PullRequests { get; }
		public Queries.Repositories.ReleaseQueries Releases { get; }
		public Queries.Repositories.RepositoryQueries Repositories { get; }
		public Queries.Repositories.TreeQueries Trees { get; }
	}

	public sealed class UserApiClient
	{
		internal UserApiClient(IGitHubApiClient apiClient, ICacheService? cache)
		{
			Activities = new(apiClient);
			Discussions = new(apiClient);
			Followers = new(apiClient);
			Following = new(apiClient);
			Issues = new(apiClient);
			Notifications = new(apiClient);
			Organizations = new(apiClient);
			Packages = new(apiClient);
			PinnedItems = new(apiClient);
			ProjectsV2 = new(apiClient);
			PullRequests = new(apiClient);
			Repositories = new(apiClient);
			StarredRepositories = new(apiClient);
			Users = new(apiClient, cache);
		}

		public Queries.Users.ActivityQueries Activities { get; }
		public Queries.Users.DiscussionQueries Discussions { get; }
		public Queries.Users.FollowersQueries Followers { get; }
		public Queries.Users.FollowingQueries Following { get; }
		public Queries.Users.IssueQueries Issues { get; }
		public Queries.Users.NotificationQueries Notifications { get; }
		public Queries.Users.OrganizationQueries Organizations { get; }
		public Queries.Users.PackageQueries Packages { get; }
		public Queries.Users.PinnedItemQueries PinnedItems { get; }
		public Queries.Users.ProjectV2Queries ProjectsV2 { get; }
		public Queries.Users.PullRequestQueries PullRequests { get; }
		public Queries.Users.RepositoryQueries Repositories { get; }
		public Queries.Users.StarredRepoQueries StarredRepositories { get; }
		public Queries.Users.UserQueries Users { get; }
	}

	public sealed class SearchApiClient
	{
		internal SearchApiClient(IGitHubApiClient apiClient)
		{
			Code = new(apiClient);
			Commits = new();
			Issues = new(apiClient);
			PullRequests = new();
			Repositories = new(apiClient);
			Topics = new();
			Users = new(apiClient);
		}

		public Searches.CodeSearches Code { get; }
		public Searches.CommitSearches Commits { get; }
		public Searches.IssueSearches Issues { get; }
		public Searches.PullRequestSearches PullRequests { get; }
		public Searches.RepositorySearches Repositories { get; }
		public Searches.TopicSearches Topics { get; }
		public Searches.UserSearches Users { get; }
	}

	public sealed class MutationApiClient
	{
		internal MutationApiClient(IGitHubApiClient apiClient)
		{
			AddStar = new(apiClient);
			ForkRepository = new(apiClient);
			Issues = new(apiClient);
			PullRequests = new(apiClient);
			Reactions = new(apiClient);
			RemoveStar = new(apiClient);
			Subscriptions = new(apiClient);
		}

		public Mutations.AddStarMutation AddStar { get; }
		public Mutations.ForkRepositoryMutation ForkRepository { get; }
		public Mutations.IssueMutations Issues { get; }
		public Mutations.PullRequestMutations PullRequests { get; }
		public Mutations.ReactionMutations Reactions { get; }
		public Mutations.RemoveStarMutation RemoveStar { get; }
		public Mutations.SubscriptionMutations Subscriptions { get; }
	}
}
