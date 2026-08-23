using FluentHub.Core.Clients;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;
using OrganizationProjectV2Queries = FluentHub.Core.Queries.Organizations.ProjectV2Queries;
using RepositoryIssueQueries = FluentHub.Core.Queries.Repositories.IssueQueries;
using RepositoryIssueEventQueries = FluentHub.Core.Queries.Repositories.IssueEventQueries;
using RepositoryProjectV2Queries = FluentHub.Core.Queries.Repositories.ProjectV2Queries;
using RepositoryPullRequestEventQueries = FluentHub.Core.Queries.Repositories.PullRequestEventQueries;
using RepositoryPullRequestQueries = FluentHub.Core.Queries.Repositories.PullRequestQueries;
using UserActivityQueries = FluentHub.Core.Queries.Users.ActivityQueries;
using UserProjectV2Queries = FluentHub.Core.Queries.Users.ProjectV2Queries;

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

	private sealed class FakeGitHubApiClient(IReadOnlyList<global::Octokit.Activity> activities) : IGitHubApiClient
	{
		public List<string> GraphQLQueries { get; } = [];
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
			=> throw new NotSupportedException();

		public Task<HttpResponseMessage> SendRestAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
	}

	private sealed class QueryCompiledException : Exception;
}
