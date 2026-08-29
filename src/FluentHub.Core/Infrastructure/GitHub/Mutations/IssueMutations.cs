// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public class IssueMutations
	{
		private const string IssueFields = """
			fragment IssueMutationFields on Issue {
			  id
			  body
			  closed
			  number
			  state
			  stateReason
			  title
			  updatedAt
			  viewerCanClose: viewerCanUpdate
			  viewerCanLabel: viewerCanUpdate
			  viewerCanReopen: viewerCanUpdate
			  viewerCanSubscribe
			  viewerCanUpdate
			  viewerSubscription
			}
			""";

		private const string CommentFields = """
			fragment IssueCommentMutationFields on IssueComment {
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

		private const string CreateIssue = """
			mutation CreateIssue($input: CreateIssueInput!) {
			  result: createIssue(input: $input) {
			    clientMutationId
			    issue { ...IssueMutationFields }
			  }
			}
			""" + IssueFields;

		private const string UpdateIssue = """
			mutation UpdateIssue($input: UpdateIssueInput!) {
			  result: updateIssue(input: $input) {
			    clientMutationId
			    issue { ...IssueMutationFields }
			  }
			}
			""" + IssueFields;

		private const string CloseIssue = """
			mutation CloseIssue($input: CloseIssueInput!) {
			  result: closeIssue(input: $input) {
			    clientMutationId
			    issue { ...IssueMutationFields }
			  }
			}
			""" + IssueFields;

		private const string ReopenIssue = """
			mutation ReopenIssue($input: ReopenIssueInput!) {
			  result: reopenIssue(input: $input) {
			    clientMutationId
			    issue { ...IssueMutationFields }
			  }
			}
			""" + IssueFields;

		private const string AddComment = """
			mutation AddComment($input: AddCommentInput!) {
			  result: addComment(input: $input) {
			    clientMutationId
			    commentEdge {
			      cursor
			      node { ...IssueCommentMutationFields }
			    }
			  }
			}
			""" + CommentFields;

		private const string UpdateIssueComment = """
			mutation UpdateIssueComment($input: UpdateIssueCommentInput!) {
			  result: updateIssueComment(input: $input) {
			    clientMutationId
			    issueComment { ...IssueCommentMutationFields }
			  }
			}
			""" + CommentFields;

		private const string DeleteIssueComment = """
			mutation DeleteIssueComment($input: DeleteIssueCommentInput!) {
			  result: deleteIssueComment(input: $input) {
			    clientMutationId
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public IssueMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<CreateIssueResult> CreateIssueAsync(
			CreateIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				CreateIssue,
				GitHubGraphQLJsonContext.Default.GraphQLResultCreateIssueResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("repositoryId", request.RepositoryId.Value);
					writer.WriteString("title", request.Title);
					GraphQLInputWriter.WriteOptionalString(writer, "body", request.Body);
					GraphQLInputWriter.WriteOptionalIds(writer, "assigneeIds", request.AssigneeIds);
					GraphQLInputWriter.WriteOptionalId(writer, "milestoneId", request.MilestoneId);
					GraphQLInputWriter.WriteOptionalIds(writer, "labelIds", request.LabelIds);
					GraphQLInputWriter.WriteOptionalIds(writer, "projectIds", request.ProjectIds);
					GraphQLInputWriter.WriteOptionalString(writer, "issueTemplate", request.IssueTemplate);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampIssue(result.Issue);
			return result;
		}

		public async Task<UpdateIssueResult> UpdateIssueAsync(
			UpdateIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				UpdateIssue,
				GitHubGraphQLJsonContext.Default.GraphQLResultUpdateIssueResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("id", request.Id.Value);
					GraphQLInputWriter.WriteOptionalString(writer, "title", request.Title);
					GraphQLInputWriter.WriteOptionalString(writer, "body", request.Body);
					GraphQLInputWriter.WriteOptionalIds(writer, "assigneeIds", request.AssigneeIds);
					GraphQLInputWriter.WriteOptionalId(writer, "milestoneId", request.MilestoneId);
					GraphQLInputWriter.WriteOptionalIds(writer, "labelIds", request.LabelIds);
					if (request.State is not null)
						writer.WriteString("state", ToGraphQL(request.State.Value));
					GraphQLInputWriter.WriteOptionalIds(writer, "projectIds", request.ProjectIds);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampIssue(result.Issue);
			return result;
		}

		public async Task<CloseIssueResult> CloseIssueAsync(
			CloseIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				CloseIssue,
				GitHubGraphQLJsonContext.Default.GraphQLResultCloseIssueResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("issueId", request.IssueId.Value);
					if (request.StateReason is not null)
						writer.WriteString("stateReason", ToGraphQL(request.StateReason.Value));
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampIssue(result.Issue);
			return result;
		}

		public async Task<ReopenIssueResult> ReopenIssueAsync(
			ReopenIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				ReopenIssue,
				GitHubGraphQLJsonContext.Default.GraphQLResultReopenIssueResult,
				writer => WriteIdInput(writer, "issueId", request.IssueId, request.ClientMutationId),
				cancellationToken);
			StampIssue(result.Issue);
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

		public async Task<UpdateIssueCommentResult> UpdateIssueCommentAsync(
			UpdateIssueCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var result = await ExecuteMutationAsync(
				UpdateIssueComment,
				GitHubGraphQLJsonContext.Default.GraphQLResultUpdateIssueCommentResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("id", request.Id.Value);
					writer.WriteString("body", request.Body);
					GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);
			StampComment(result.IssueComment);
			return result;
		}

		public Task<DeleteIssueCommentResult> DeleteIssueCommentAsync(
			DeleteIssueCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			return ExecuteMutationAsync(
				DeleteIssueComment,
				GitHubGraphQLJsonContext.Default.GraphQLResultDeleteIssueCommentResult,
				writer => WriteIdInput(writer, "id", request.Id, request.ClientMutationId),
				cancellationToken);
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
				?? throw new InvalidDataException("GitHub returned an incomplete issue mutation response.");
		}

		private static void WriteIdInput(
			Utf8JsonWriter writer,
			string idProperty,
			ID id,
			string? clientMutationId)
		{
			writer.WriteStartObject("input");
			writer.WriteString(idProperty, id.Value);
			GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", clientMutationId);
			writer.WriteEndObject();
		}

		private static void StampIssue(Issue? issue)
		{
			if (issue is not null)
				issue.UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime();
		}

		private static void StampComment(IssueComment? comment)
		{
			if (comment is null)
				return;

			comment.CreatedAtHumanized = comment.CreatedAt.ToRelativeTime();
			comment.UpdatedAtHumanized = comment.UpdatedAt.ToRelativeTime();
		}

		private static string ToGraphQL(IssueState state)
			=> state switch
			{
				IssueState.Open => "OPEN",
				IssueState.Closed => "CLOSED",
				_ => throw new ArgumentOutOfRangeException(nameof(state), state, "Unknown issue state."),
			};

		private static string ToGraphQL(IssueClosedStateReason stateReason)
			=> stateReason switch
			{
				IssueClosedStateReason.Completed => "COMPLETED",
				IssueClosedStateReason.NotPlanned => "NOT_PLANNED",
				IssueClosedStateReason.Duplicate => "DUPLICATE",
				_ => throw new ArgumentOutOfRangeException(nameof(stateReason), stateReason, "Unknown issue close reason."),
			};
	}
}
