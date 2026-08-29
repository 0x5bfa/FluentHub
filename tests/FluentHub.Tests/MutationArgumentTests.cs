using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace FluentHub.Tests;

[TestClass]
public sealed class MutationArgumentTests
{
	[TestMethod]
	public async Task IssueMutationsRejectNullInputs()
	{
		var mutations = new IssueMutations(null!);

		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.CreateIssueAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.UpdateIssueAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.CloseIssueAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.ReopenIssueAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.AddCommentAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.UpdateIssueCommentAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.DeleteIssueCommentAsync(null!));
	}

	[TestMethod]
	public async Task PullRequestMutationsRejectNullInputs()
	{
		var mutations = new PullRequestMutations(null!);

		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.UpdateAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.CloseAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.ReopenAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.MergeAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.AddCommentAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.AddReviewAsync(null!));
	}

	[TestMethod]
	public async Task ReactionMutationsRejectNullInputs()
	{
		var mutations = new ReactionMutations(null!);

		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.AddAsync(null!));
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.RemoveAsync(null!));
	}

	[TestMethod]
	public async Task SubscriptionMutationsRejectNullInputs()
	{
		var mutations = new SubscriptionMutations(null!);

		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutations.UpdateAsync(null!));
	}

	[TestMethod]
	public async Task PullRequestReviewRejectsInlineCommentsUntilSupported()
	{
		var mutations = new PullRequestMutations(null!);
		var input = new AddPullRequestReviewRequest
		{
			Comments = [],
		};

		await Assert.ThrowsExactlyAsync<NotSupportedException>(() => mutations.AddReviewAsync(input));
	}

	[TestMethod]
	public async Task MutationOperationsAreBuiltBeforeExecution()
	{
		var api = new FakeGitHubApiClient();
		var id = new ID("test-node-id");
		var issueMutations = new IssueMutations(api);
		var pullRequestMutations = new PullRequestMutations(api);
		var reactionMutations = new ReactionMutations(api);
		var subscriptionMutations = new SubscriptionMutations(api);

		await issueMutations.CreateIssueAsync(new CreateIssueRequest { RepositoryId = id, Title = "Test" });
		await issueMutations.UpdateIssueAsync(new UpdateIssueRequest { Id = id, Title = "Test" });
		await issueMutations.CloseIssueAsync(new CloseIssueRequest { IssueId = id });
		await issueMutations.ReopenIssueAsync(new ReopenIssueRequest { IssueId = id });
		await issueMutations.AddCommentAsync(new AddCommentRequest { SubjectId = id, Body = "Test" });
		await issueMutations.UpdateIssueCommentAsync(new UpdateIssueCommentRequest { Id = id, Body = "Test" });
		await issueMutations.DeleteIssueCommentAsync(new DeleteIssueCommentRequest { Id = id });

		await pullRequestMutations.UpdateAsync(new UpdatePullRequestRequest { PullRequestId = id, Title = "Test" });
		await pullRequestMutations.CloseAsync(new ClosePullRequestRequest { PullRequestId = id });
		await pullRequestMutations.ReopenAsync(new ReopenPullRequestRequest { PullRequestId = id });
		await pullRequestMutations.MergeAsync(new MergePullRequestRequest { PullRequestId = id });
		await pullRequestMutations.AddCommentAsync(new AddCommentRequest { SubjectId = id, Body = "Test" });
		await pullRequestMutations.AddReviewAsync(new AddPullRequestReviewRequest { PullRequestId = id });

		await reactionMutations.AddAsync(new AddReactionRequest { SubjectId = id, Content = ReactionContent.Heart });
		await reactionMutations.RemoveAsync(new RemoveReactionRequest { SubjectId = id, Content = ReactionContent.Heart });
		await subscriptionMutations.UpdateAsync(new UpdateSubscriptionRequest
		{
			SubscribableId = id,
			State = SubscriptionState.Subscribed,
		});

		Assert.AreEqual(16, api.GraphQLCallCount);
	}

	[TestMethod]
	public async Task UpdateIssueOmitsUnspecifiedOptionalInputs()
	{
		var api = new FakeGitHubApiClient();
		await new IssueMutations(api).UpdateIssueAsync(new UpdateIssueRequest
		{
			Id = new ID("issue-id"),
			Title = "Updated title",
		});

		using var document = JsonDocument.Parse(api.LastVariables);
		var input = document.RootElement.GetProperty("input");
		Assert.AreEqual("issue-id", input.GetProperty("id").GetString());
		Assert.AreEqual("Updated title", input.GetProperty("title").GetString());
		Assert.IsFalse(input.TryGetProperty("body", out _));
		Assert.IsFalse(input.TryGetProperty("assigneeIds", out _));
		Assert.IsFalse(input.TryGetProperty("milestoneId", out _));
	}

	[TestMethod]
	public async Task ModifiedQueryOperationsAreBuiltBeforeExecution()
	{
		var api = new FakeGitHubApiClient { ThrowAfterGraphQLRequest = true };

		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new IssueQueries(api).GetAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new IssueQueries(api).GetBodyAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new PullRequestQueries(api).GetAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new PullRequestQueries(api).GetBodyAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new RepositoryQueries(api).GetIssueOptionsAsync("owner", "repository"));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new IssueEventQueries(api).GetAllAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<GraphQLRequestCapturedException>(
			() => new PullRequestEventQueries(api).GetAllAsync("owner", "repository", 1));

		Assert.AreEqual(7, api.GraphQLCallCount);
	}

	private sealed class FakeGitHubApiClient : IGitHubApiClient
	{
		public int GraphQLCallCount { get; private set; }
		public string LastVariables { get; private set; } = string.Empty;
		public bool ThrowAfterGraphQLRequest { get; init; }

		public Task<T> RunGraphQLAsync<T>(global::Octokit.GraphQL.GraphQLOperation<T> operation,
			JsonTypeInfo<T> dataTypeInfo, Action<Utf8JsonWriter>? writeVariables = null,
			CancellationToken cancellationToken = default)
		{
			GraphQLCallCount++;
			var buffer = new ArrayBufferWriter<byte>();
			using (var writer = new Utf8JsonWriter(buffer))
			{
				writer.WriteStartObject();
				writeVariables?.Invoke(writer);
				writer.WriteEndObject();
			}
			LastVariables = System.Text.Encoding.UTF8.GetString(buffer.WrittenSpan);
			if (ThrowAfterGraphQLRequest)
				throw new GraphQLRequestCapturedException();
			return Task.FromResult(JsonSerializer.Deserialize("{\"result\":{}}", dataTypeInfo)!);
		}

		public Task<T> RunRestAsync<T>(
			Func<global::Octokit.Rest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
	}

	private sealed class GraphQLRequestCapturedException : Exception;
}
