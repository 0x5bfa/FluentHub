// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Discussions;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class DiscussionQueries
	{
		private const string PageQuery = """
			query RepositoryDiscussions($owner: String!, $name: String!, $first: Int, $after: String, $last: Int, $before: String, $categoryId: ID, $orderBy: DiscussionOrder) {
			  result: repository(owner: $owner, name: $name) {
			    discussions(first: $first, after: $after, last: $last, before: $before, categoryId: $categoryId, orderBy: $orderBy) {
			""" + DiscussionQuery.Connection + """
			    }
			  }
			}
			""" + DiscussionQuery.ListFields;

		private const string ItemQuery = """
			query Discussion($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    discussion(number: $number) {
			      activeLockReason answerChosenAt authorAssociation bodyHTML createdAt id includesCreatedEdit
			      lastEditedAt locked number publishedAt title updatedAt upvoteCount url
			      viewerCanDelete viewerCanReact viewerCanSubscribe viewerCanUpdate viewerCanUpvote
			      viewerDidAuthor viewerHasUpvoted viewerSubscription
			      category { createdAt description emoji id name updatedAt }
			      repository { name owner { avatarUrl(size: 500) id login } }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public DiscussionQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<Discussion>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			DiscussionListFilters filters,
			CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetRepositoryPageAsync(owner, name, page, filters, cancellationToken);

		public Task<IReadOnlyList<string>> GetLabelNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetRepositoryLabelNamesAsync(owner, name, cancellationToken);

		public async Task<PageResult<Discussion>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			ID? categoryId = null,
			DiscussionOrder? orderBy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				PageQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					WriteRepository(writer, owner, name);
					GraphQLInputWriter.WritePage(writer, page);
					GraphQLInputWriter.WriteOptionalId(writer, "categoryId", categoryId);
					DiscussionQuery.WriteOrder(writer, orderBy);
				},
				cancellationToken);
			return DiscussionQuery.ToPage(response.Result?.Discussions
				?? throw new InvalidDataException("GitHub returned an incomplete repository discussions response."));
		}

		public async Task<Discussion> GetAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				ItemQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					WriteRepository(writer, owner, name);
					writer.WriteNumber("number", number);
				},
				cancellationToken);
			var discussion = response.Result?.Discussion
				?? throw new InvalidDataException($"GitHub discussion '{owner}/{name}#{number}' was not found.");
			discussion.UpdatedAtHumanized = discussion.UpdatedAt.ToRelativeTime();
			return discussion;
		}

		private static void WriteRepository(System.Text.Json.Utf8JsonWriter writer, string owner, string name)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
		}
	}
}
