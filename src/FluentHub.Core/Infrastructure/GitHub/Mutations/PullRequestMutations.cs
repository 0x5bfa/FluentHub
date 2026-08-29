// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public sealed class PullRequestMutations
	{
		private const string PullRequestFields = """
			fragment PullRequestMutationFields on PullRequest {
			  id
			  body
			  closed
			  headRefOid
			  mergeable
			  merged
			  state
			  title
			  updatedAt
			  viewerCanClose: viewerCanUpdate
			  viewerCanMergeAsAdmin
			  viewerCanReopen: viewerCanUpdate
			  viewerCanSubscribe
			  viewerCanUpdate
			  viewerSubscription
			}
			""";

		private const string CommentFields = """
			fragment PullRequestCommentMutationFields on IssueComment {
			  authorAssociation
			  body
			  bodyHTML
			  createdAt
			  id
			  lastEditedAt
			  updatedAt
			  url
			  viewerCanDelete
			  viewerCanReact
			  viewerCanUpdate
			  viewerDidAuthor
			  author { avatarUrl(size: 500) login }
			}
			""";

		private const string UpdatePullRequest = """
			mutation UpdatePullRequest($input: UpdatePullRequestInput!) {
			  result: updatePullRequest(input: $input) {
			    clientMutationId
			    pullRequest { ...PullRequestMutationFields }
			  }
			}
			""" + PullRequestFields;

		private const string ClosePullRequest = """
			mutation ClosePullRequest($input: ClosePullRequestInput!) {
			  result: closePullRequest(input: $input) {
			    clientMutationId
			    pullRequest { ...PullRequestMutationFields }
			  }
			}
			""" + PullRequestFields;

		private const string ReopenPullRequest = """
			mutation ReopenPullRequest($input: ReopenPullRequestInput!) {
			  result: reopenPullRequest(input: $input) {
			    clientMutationId
			    pullRequest { ...PullRequestMutationFields }
			  }
			}
			""" + PullRequestFields;

		private const string MergePullRequest = """
			mutation MergePullRequest($input: MergePullRequestInput!) {
			  result: mergePullRequest(input: $input) {
			    clientMutationId
			    pullRequest { ...PullRequestMutationFields }
			  }
			}
			""" + PullRequestFields;

		private const string AddComment = """
			mutation AddComment($input: AddCommentInput!) {
			  result: addComment(input: $input) {
			    clientMutationId
			    commentEdge {
			      cursor
			      node { ...PullRequestCommentMutationFields }
			    }
			  }
			}
			""" + CommentFields;

		private const string AddPullRequestReview = """
			mutation AddPullRequestReview($input: AddPullRequestReviewInput!) {
			  result: addPullRequestReview(input: $input) {
			    clientMutationId
			    pullRequestReview {
			      body
			      createdAt
			      id
			      state
			      submittedAt
			      url
			      author { avatarUrl(size: 500) login }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PullRequestMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<UpdatePullRequestResult> UpdateAsync(
			UpdatePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				UpdatePullRequest,
				GitHubGraphQLJsonContext.Default.GraphQLResultUpdatePullRequestResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("pullRequestId", request.PullRequestId.Value);
					GraphQLInputWriter.WriteOptionalString(writer, "baseRefName", request.BaseRefName);
					GraphQLInputWriter.WriteOptionalString(writer, "title", request.Title);
					GraphQLInputWriter.WriteOptionalString(writer, "body", request.Body);
					if (request.State is not null)
						writer.WriteString("state", ToGraphQL(request.State.Value));
					GraphQLInputWriter.WriteOptionalBoolean(writer, "maintainerCanModify", request.MaintainerCanModify);
					GraphQLInputWriter.WriteOptionalIds(writer, "assigneeIds", request.AssigneeIds);
					GraphQLInputWriter.WriteOptionalId(writer, "milestoneId", request.MilestoneId);
					GraphQLInputWriter.WriteOptionalIds(writer, "labelIds", request.LabelIds);
					GraphQLInputWriter.WriteOptionalIds(writer, "projectIds", request.ProjectIds);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampPullRequest(result.PullRequest);
			return result;
		}

		public async Task<ClosePullRequestResult> CloseAsync(
			ClosePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			var result = await ExecuteMutationAsync(
				ClosePullRequest,
				GitHubGraphQLJsonContext.Default.GraphQLResultClosePullRequestResult,
				writer => WriteIdInput(writer, request.PullRequestId, request.ClientMutationId),
				cancellationToken);
			StampPullRequest(result.PullRequest);
			return result;
		}

		public async Task<ReopenPullRequestResult> ReopenAsync(
			ReopenPullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			var result = await ExecuteMutationAsync(
				ReopenPullRequest,
				GitHubGraphQLJsonContext.Default.GraphQLResultReopenPullRequestResult,
				writer => WriteIdInput(writer, request.PullRequestId, request.ClientMutationId),
				cancellationToken);
			StampPullRequest(result.PullRequest);
			return result;
		}

		public async Task<MergePullRequestResult> MergeAsync(
			MergePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			var result = await ExecuteMutationAsync(
				MergePullRequest,
				GitHubGraphQLJsonContext.Default.GraphQLResultMergePullRequestResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("pullRequestId", request.PullRequestId.Value);
					GraphQLInputWriter.WriteOptionalString(writer, "commitHeadline", request.CommitHeadline);
					GraphQLInputWriter.WriteOptionalString(writer, "commitBody", request.CommitBody);
					GraphQLInputWriter.WriteOptionalString(writer, "expectedHeadOid", request.ExpectedHeadOid);
					if (request.MergeMethod is not null)
						writer.WriteString("mergeMethod", ToGraphQL(request.MergeMethod.Value));
					GraphQLInputWriter.WriteOptionalString(writer, "authorEmail", request.AuthorEmail);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampPullRequest(result.PullRequest);
			return result;
		}

		public async Task<AddCommentResult> AddCommentAsync(
			AddCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			var result = await ExecuteMutationAsync(
				AddComment,
				GitHubGraphQLJsonContext.Default.GraphQLResultAddCommentResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("subjectId", request.SubjectId.Value);
					writer.WriteString("body", request.Body);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampComment(result.CommentEdge?.Node);
			return result;
		}

		public async Task<AddPullRequestReviewResult> AddReviewAsync(
			AddPullRequestReviewRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			if (request.Comments is not null || request.Threads is not null)
				throw new NotSupportedException("Inline review comments are not supported by this mutation wrapper.");

			var result = await ExecuteMutationAsync(
				AddPullRequestReview,
				GitHubGraphQLJsonContext.Default.GraphQLResultAddPullRequestReviewResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("pullRequestId", request.PullRequestId.Value);
					GraphQLInputWriter.WriteOptionalString(writer, "commitOID", request.CommitOID);
					GraphQLInputWriter.WriteOptionalString(writer, "body", request.Body);
					if (request.Event is not null)
						writer.WriteString("event", ToGraphQL(request.Event.Value));
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);

			if (result.PullRequestReview is { } review)
				review.CreatedAtHumanized = review.CreatedAt.ToRelativeTime();
			return result;
		}

		private async Task<TResult> ExecuteMutationAsync<TResult>(
			string query,
			JsonTypeInfo<GraphQLResult<TResult>> typeInfo,
			Action<Utf8JsonWriter> writeVariables,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				query,
				typeInfo,
				writeVariables,
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete pull request mutation response.");
		}

		private static void WriteIdInput(Utf8JsonWriter writer, ID id, string? clientMutationId)
		{
			writer.WriteStartObject("input");
			writer.WriteString("pullRequestId", id.Value);
			GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", clientMutationId);
			writer.WriteEndObject();
		}

		private static void StampPullRequest(PullRequest? pullRequest)
		{
			if (pullRequest is not null)
				pullRequest.UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime();
		}

		private static void StampComment(IssueComment? comment)
		{
			if (comment is null)
				return;
			comment.CreatedAtHumanized = comment.CreatedAt.ToRelativeTime();
			comment.UpdatedAtHumanized = comment.UpdatedAt.ToRelativeTime();
		}

		private static string ToGraphQL(PullRequestUpdateState state)
			=> state switch
			{
				PullRequestUpdateState.Open => "OPEN",
				PullRequestUpdateState.Closed => "CLOSED",
				_ => throw new ArgumentOutOfRangeException(nameof(state), state, "Unknown pull request state."),
			};

		private static string ToGraphQL(PullRequestMergeMethod method)
			=> method switch
			{
				PullRequestMergeMethod.Merge => "MERGE",
				PullRequestMergeMethod.Squash => "SQUASH",
				PullRequestMergeMethod.Rebase => "REBASE",
				_ => throw new ArgumentOutOfRangeException(nameof(method), method, "Unknown pull request merge method."),
			};

		private static string ToGraphQL(PullRequestReviewEvent reviewEvent)
			=> reviewEvent switch
			{
				PullRequestReviewEvent.Comment => "COMMENT",
				PullRequestReviewEvent.Approve => "APPROVE",
				PullRequestReviewEvent.RequestChanges => "REQUEST_CHANGES",
				PullRequestReviewEvent.Dismiss => "DISMISS",
				_ => throw new ArgumentOutOfRangeException(nameof(reviewEvent), reviewEvent, "Unknown review event."),
			};
	}
}
