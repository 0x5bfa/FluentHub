using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;

namespace FluentHub.Tests;

[TestClass]
public sealed class MutationArgumentTests
{
	[TestMethod]
	public void IssueMutationsRejectNullInputs()
	{
		var mutations = new IssueMutations(null!);

		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.CreateIssueAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.UpdateIssueAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.CloseIssueAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.ReopenIssueAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.AddCommentAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.UpdateIssueCommentAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.DeleteIssueCommentAsync(null!));
	}

	[TestMethod]
	public void PullRequestMutationsRejectNullInputs()
	{
		var mutations = new PullRequestMutations(null!);

		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.UpdateAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.CloseAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.ReopenAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.MergeAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.AddCommentAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.AddReviewAsync(null!));
	}

	[TestMethod]
	public void ReactionMutationsRejectNullInputs()
	{
		var mutations = new ReactionMutations(null!);

		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.AddAsync(null!));
		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.RemoveAsync(null!));
	}

	[TestMethod]
	public void SubscriptionMutationsRejectNullInputs()
	{
		var mutations = new SubscriptionMutations(null!);

		Assert.ThrowsExactly<ArgumentNullException>(() => mutations.UpdateAsync(null!));
	}

	[TestMethod]
	public void PullRequestReviewRejectsInlineCommentsUntilSupported()
	{
		var mutations = new PullRequestMutations(null!);
		var input = new AddPullRequestReviewRequest
		{
			Comments = [],
		};

		Assert.ThrowsExactly<NotSupportedException>(() => mutations.AddReviewAsync(input));
	}

	[TestMethod]
	public void MutationExpressionsCompileBeforeExecution()
	{
		var api = new FakeGitHubApiClient();
		var id = new ID("test-node-id");
		var issueMutations = new IssueMutations(api);
		var pullRequestMutations = new PullRequestMutations(api);
		var reactionMutations = new ReactionMutations(api);
		var subscriptionMutations = new SubscriptionMutations(api);

		issueMutations.CreateIssueAsync(new CreateIssueRequest { RepositoryId = id, Title = "Test" });
		issueMutations.UpdateIssueAsync(new UpdateIssueRequest { Id = id, Title = "Test" });
		issueMutations.CloseIssueAsync(new CloseIssueRequest { IssueId = id });
		issueMutations.ReopenIssueAsync(new ReopenIssueRequest { IssueId = id });
		issueMutations.AddCommentAsync(new AddCommentRequest { SubjectId = id, Body = "Test" });
		issueMutations.UpdateIssueCommentAsync(new UpdateIssueCommentRequest { Id = id, Body = "Test" });
		issueMutations.DeleteIssueCommentAsync(new DeleteIssueCommentRequest { Id = id });

		pullRequestMutations.UpdateAsync(new UpdatePullRequestRequest { PullRequestId = id, Title = "Test" });
		pullRequestMutations.CloseAsync(new ClosePullRequestRequest { PullRequestId = id });
		pullRequestMutations.ReopenAsync(new ReopenPullRequestRequest { PullRequestId = id });
		pullRequestMutations.MergeAsync(new MergePullRequestRequest { PullRequestId = id });
		pullRequestMutations.AddCommentAsync(new AddCommentRequest { SubjectId = id, Body = "Test" });
		pullRequestMutations.AddReviewAsync(new AddPullRequestReviewRequest { PullRequestId = id });

		reactionMutations.AddAsync(new AddReactionRequest { SubjectId = id, Content = ReactionContent.Heart });
		reactionMutations.RemoveAsync(new RemoveReactionRequest { SubjectId = id, Content = ReactionContent.Heart });
		subscriptionMutations.UpdateAsync(new UpdateSubscriptionRequest
		{
			SubscribableId = id,
			State = SubscriptionState.Subscribed,
		});

		Assert.AreEqual(16, api.GraphQLCallCount);
	}

	[TestMethod]
	public async Task ModifiedQueryExpressionsCompileBeforeExecution()
	{
		var api = new FakeGitHubApiClient { ThrowAfterGraphQLCompilation = true };

		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new IssueQueries(api).GetAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new IssueQueries(api).GetBodyAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new PullRequestQueries(api).GetAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new PullRequestQueries(api).GetBodyAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new RepositoryQueries(api).GetIssueOptionsAsync("owner", "repository"));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new IssueEventQueries(api).GetAllAsync("owner", "repository", 1));
		await Assert.ThrowsExactlyAsync<QueryCompiledException>(
			() => new PullRequestEventQueries(api).GetAllAsync("owner", "repository", 1));

		Assert.AreEqual(7, api.GraphQLCallCount);
	}

	private sealed class FakeGitHubApiClient : IGitHubApiClient
	{
		public int GraphQLCallCount { get; private set; }
		public bool ThrowAfterGraphQLCompilation { get; init; }

		public Task<T> RunGraphQLAsync<T>(ICompiledQuery<T> query, CancellationToken cancellationToken = default)
		{
			GraphQLCallCount++;
			if (ThrowAfterGraphQLCompilation)
				throw new QueryCompiledException();
			return Task.FromResult(default(T)!);
		}

		public Task<T> RunRestAsync<T>(
			Func<global::Octokit.Rest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();

		public Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
	}

	private sealed class QueryCompiledException : Exception;
}
