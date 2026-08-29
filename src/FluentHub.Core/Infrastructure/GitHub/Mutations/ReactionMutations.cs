// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public sealed class ReactionMutations
	{
		private const string ReactionFields = """
			clientMutationId
			reactionGroups {
			  content
			  viewerHasReacted
			  reactors { totalCount }
			}
			""";

		private const string AddReaction = """
			mutation AddReaction($input: AddReactionInput!) {
			  result: addReaction(input: $input) {
			""" + ReactionFields + """
			  }
			}
			""";

		private const string RemoveReaction = """
			mutation RemoveReaction($input: RemoveReactionInput!) {
			  result: removeReaction(input: $input) {
			""" + ReactionFields + """
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public ReactionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<AddReactionResult> AddAsync(
			AddReactionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var response = await _gitHub.RunGraphQLAsync(
				AddReaction,
				GitHubGraphQLJsonContext.Default.GraphQLResultAddReactionResult,
				writer => WriteInput(writer, request.SubjectId, request.Content, request.ClientMutationId),
				cancellationToken);

			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete add-reaction response.");
		}

		public async Task<RemoveReactionResult> RemoveAsync(
			RemoveReactionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var response = await _gitHub.RunGraphQLAsync(
				RemoveReaction,
				GitHubGraphQLJsonContext.Default.GraphQLResultRemoveReactionResult,
				writer => WriteInput(writer, request.SubjectId, request.Content, request.ClientMutationId),
				cancellationToken);

			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete remove-reaction response.");
		}

		private static void WriteInput(
			System.Text.Json.Utf8JsonWriter writer,
			ID subjectId,
			ReactionContent content,
			string? clientMutationId)
		{
			writer.WriteStartObject("input");
			writer.WriteString("subjectId", subjectId.Value);
			writer.WriteString("content", ToGraphQL(content));
			GraphQLInputWriter.WriteOptionalString(writer, "clientMutationId", clientMutationId);
			writer.WriteEndObject();
		}

		private static string ToGraphQL(ReactionContent content)
			=> content switch
			{
				ReactionContent.ThumbsUp => "THUMBS_UP",
				ReactionContent.ThumbsDown => "THUMBS_DOWN",
				ReactionContent.Laugh => "LAUGH",
				ReactionContent.Hooray => "HOORAY",
				ReactionContent.Confused => "CONFUSED",
				ReactionContent.Heart => "HEART",
				ReactionContent.Rocket => "ROCKET",
				ReactionContent.Eyes => "EYES",
				_ => throw new ArgumentOutOfRangeException(nameof(content), content, "Unknown reaction content."),
			};
	}
}
