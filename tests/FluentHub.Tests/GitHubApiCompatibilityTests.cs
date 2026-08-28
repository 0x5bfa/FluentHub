using FluentHub.Core.Infrastructure.GitHub.Clients;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;
using OrganizationProjectV2Queries = FluentHub.Core.Infrastructure.GitHub.Queries.Organizations.ProjectV2Queries;
using RepositoryIssueQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Repositories.IssueQueries;
using RepositoryIssueEventQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Repositories.IssueEventQueries;
using RepositoryProjectV2Queries = FluentHub.Core.Infrastructure.GitHub.Queries.Repositories.ProjectV2Queries;
using RepositoryPullRequestEventQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Repositories.PullRequestEventQueries;
using RepositoryPullRequestQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Repositories.PullRequestQueries;
using UserActivityQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Users.ActivityQueries;
using UserProjectV2Queries = FluentHub.Core.Infrastructure.GitHub.Queries.Users.ProjectV2Queries;
using UserRepositoryQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Users.RepositoryQueries;
using UserStarredRepositoryQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Users.StarredRepoQueries;
using UserQueries = FluentHub.Core.Infrastructure.GitHub.Queries.Users.UserQueries;

namespace FluentHub.Tests;

[TestClass]
public sealed class GitHubApiCompatibilityTests
{
	[TestMethod]
	public async Task ActivityQueriesTreatMissingPushCommitsAsEmpty()
	{
		var activity = new global::Octokit.Activity(
			"PushEvent",
			true,
			null!,
			new global::Octokit.User(),
			null!,
			DateTimeOffset.UtcNow,
			"event-id",
			new global::Octokit.PushEventPayload());
		var api = new FakeGitHubApiClient([activity]);

		var activities = await new UserActivityQueries(api).GetAllAsync("user");

		Assert.AreEqual(1, activities.Count);
		Assert.IsNotNull(activities[0].Details.PushEvent);
		Assert.AreEqual(0, activities[0].Details.PushEvent!.Commits.Count);
	}

	[TestMethod]
	public async Task ActivityQueriesSkipEventsWithMissingActor()
	{
		var activity = new global::Octokit.Activity(
			"PushEvent",
			true,
			null!,
			null!,
			null!,
			DateTimeOffset.UtcNow,
			"event-id",
			new global::Octokit.PushEventPayload());
		var api = new FakeGitHubApiClient([activity]);

		var activities = await new UserActivityQueries(api).GetAllAsync("user");

		Assert.AreEqual(0, activities.Count);
	}

	[TestMethod]
	public async Task ActivityQueriesSkipEventsWithIncompletePayload()
	{
		var activity = new global::Octokit.Activity(
			"IssueEvent",
			true,
			null!,
			new global::Octokit.User(),
			null!,
			DateTimeOffset.UtcNow,
			"event-id",
			new global::Octokit.IssueEventPayload());
		var api = new FakeGitHubApiClient([activity]);

		var activities = await new UserActivityQueries(api).GetAllAsync("user");

		Assert.AreEqual(0, activities.Count);
	}

	[TestMethod]
	public async Task ContributionCalendarQueryRequestsDatesAndMonthMetadata()
	{
		var api = new FakeGitHubApiClient([])
		{
			ThrowAfterGraphQLCompilation = true,
		};

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new UserActivityQueries(api).GetContributionCalendarAsync("octocat"));

		Assert.HasCount(1, api.GraphQLQueries);
		var query = api.GraphQLQueries[0];
		Assert.IsTrue(query.Contains("contributionCalendar", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("months", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("firstDay", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("date", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("contributionLevel", StringComparison.Ordinal));
	}

	[TestMethod]
	public async Task DetailQueriesDoNotRequestClassicProjectCards()
	{
		var api = new FakeGitHubApiClient([]);

		await new RepositoryIssueQueries(api).GetAsync("owner", "repository", 1);
		await new RepositoryPullRequestQueries(api).GetAsync("owner", "repository", 1);

		Assert.AreEqual(2, api.GraphQLQueries.Count);
		foreach (var query in api.GraphQLQueries)
		{
			Assert.IsFalse(
				query.Contains("projectCards", StringComparison.OrdinalIgnoreCase),
				$"The query still requests Projects Classic data:{Environment.NewLine}{query}");
			Assert.IsTrue(query.Contains("authorAssociation", StringComparison.Ordinal));
			Assert.IsTrue(query.Contains("reactionGroups", StringComparison.Ordinal));
			Assert.IsTrue(query.Contains("viewerPermission", StringComparison.Ordinal));
			Assert.IsFalse(query.Contains("reactions(", StringComparison.Ordinal));
		}
	}

	[TestMethod]
	public async Task TimelineQueriesUseReactionSummariesWithoutReactionNodes()
	{
		var api = new FakeGitHubApiClient([])
		{
			ThrowAfterGraphQLCompilation = true,
		};

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new RepositoryIssueEventQueries(api).GetAllAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new RepositoryPullRequestEventQueries(api).GetAllAsync("owner", "repository", 1));

		Assert.AreEqual(2, api.GraphQLQueries.Count);
		foreach (var query in api.GraphQLQueries)
		{
			Assert.IsTrue(query.Contains("reactionGroups", StringComparison.Ordinal));
			Assert.IsFalse(query.Contains("reactions(", StringComparison.Ordinal));
		}
	}

	[TestMethod]
	public async Task ProjectV2QueriesCompileInlineSelections()
	{
		var api = new FakeGitHubApiClient([])
		{
			ThrowAfterGraphQLCompilation = true,
		};
		var page = FluentHub.Core.PageRequest.Forward(10);

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new RepositoryProjectV2Queries(api).GetPageAsync("owner", "repository", page));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new UserProjectV2Queries(api).GetPageAsync("user", page));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new OrganizationProjectV2Queries(api).GetPageAsync("organization", page));

		Assert.AreEqual(3, api.GraphQLQueries.Count);
	}

	[TestMethod]
	public async Task UserRepositorySearchRequestsFilterMetadata()
	{
		var api = new FakeGitHubApiClient([]);
		await Assert.ThrowsExactlyAsync<InvalidDataException>(() => new UserRepositoryQueries(api).SearchAllAsync(
			"octocat",
			new FluentHub.Core.Infrastructure.GitHub.Queries.Users.UserRepositoryListFilters()));

		Assert.HasCount(1, api.RawGraphQLQueries);
		var query = api.RawGraphQLQueries[0];
		Assert.IsTrue(query.Contains("hasSponsorshipsEnabled", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("isMirror", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("isTemplate", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("primaryLanguage", StringComparison.Ordinal));
	}

	[TestMethod]
	public async Task UserRepositoryLanguageQueriesUseLightweightConnections()
	{
		var api = new FakeGitHubApiClient([])
		{
			ThrowAfterGraphQLCompilation = true,
		};

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new UserRepositoryQueries(api).GetLanguagesAsync("octocat"));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new UserStarredRepositoryQueries(api).GetLanguagesAsync("octocat"));

		Assert.HasCount(2, api.GraphQLQueries);
		foreach (var query in api.GraphQLQueries)
		{
			Assert.IsTrue(query.Contains("primaryLanguage", StringComparison.Ordinal));
			Assert.IsFalse(query.Contains("issues(", StringComparison.Ordinal));
			Assert.IsFalse(query.Contains("pullRequests(", StringComparison.Ordinal));
		}
	}

	[TestMethod]
	public async Task ProfileReadmeQueryRequestsVisibilityAndRootReadme()
	{
		var api = new FakeGitHubApiClient([])
		{
			ThrowAfterGraphQLCompilation = true,
		};

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new UserQueries(api).GetProfileReadmeAsync("octocat"));

		Assert.HasCount(1, api.GraphQLQueries);
		var query = api.GraphQLQueries[0];
		Assert.IsTrue(query.Contains("repository(", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("isPrivate", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("defaultBranchRef", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("object(", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("... on Blob", StringComparison.Ordinal));
		Assert.IsTrue(query.Contains("text", StringComparison.Ordinal));
	}

	private sealed class FakeGitHubApiClient(IReadOnlyList<global::Octokit.Activity> activities) : IGitHubApiClient
	{
		public List<string> GraphQLQueries { get; } = [];
		public List<string> RawGraphQLQueries { get; } = [];
		public bool ThrowAfterGraphQLCompilation { get; init; }

		public Task<T> RunRestAsync<T>(
			Func<global::Octokit.IGitHubClient, Task<T>> operation,
			CancellationToken cancellationToken = default)
		{
			if (activities is T response)
				return Task.FromResult(response);

			throw new NotSupportedException($"Unexpected REST response type: {typeof(T)}");
		}

		public Task<T> RunGraphQLAsync<T>(ICompiledQuery<T> query, CancellationToken cancellationToken = default)
		{
			GraphQLQueries.Add(((ICompiledQuery)query).ToString(0));
			if (ThrowAfterGraphQLCompilation)
				throw new QueryCompiledException();
			return Task.FromResult(default(T)!);
		}

		public Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default)
		{
			RawGraphQLQueries.Add(request.Query ?? string.Empty);
			return Task.FromResult(new GraphQLResponse<T>());
		}

		public Task<HttpResponseMessage> SendRestAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
	}

	private sealed class QueryCompiledException : Exception;
}
