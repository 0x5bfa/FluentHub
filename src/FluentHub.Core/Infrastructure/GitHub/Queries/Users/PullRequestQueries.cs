// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class PullRequestQueries
	{
		private const string Query = """
			query UserPullRequests($login: String!, $first: Int, $after: String, $last: Int, $before: String, $baseRefName: String, $headRefName: String, $labels: [String!], $orderBy: IssueOrder, $states: [PullRequestState!]) {
			  result: user(login: $login) {
			    pullRequests(first: $first, after: $after, last: $last, before: $before, baseRefName: $baseRefName, headRefName: $headRefName, labels: $labels, orderBy: $orderBy, states: $states) {
			      edges {
			        node {
			          baseRefName closed headRefName isDraft merged number title updatedAt
			          repository { name owner { avatarUrl(size: 500) id login } }
			          headRepository { name owner { avatarUrl(size: 500) login } }
			          comments { totalCount }
			          labels(first: 10) { nodes { color description name } }
			          latestReviews: reviews(last: 1) { nodes { state } }
			          commits(last: 1) { nodes { commit { statusCheckRollup { state } } } }
			        }
			      }
			      pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PullRequestQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<PullRequest>> GetPageAsync(
			string login,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetUserPullRequestPageAsync(login, page, filters, cancellationToken);

		public Task<RepositoryItemFilterOptions> GetFilterOptionsAsync(
			string login,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetUserFilterOptionsAsync(login, true, cancellationToken);

		public async Task<PageResult<PullRequest>> GetPageAsync(
			string login,
			PageRequest page,
			string? baseRefName = null,
			string? headRefName = null,
			IEnumerable<string>? labels = null,
			IssueOrder? orderBy = null,
			IEnumerable<PullRequestState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			orderBy ??= new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };
			states ??= [PullRequestState.Open];
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
					GraphQLInputWriter.WriteOptionalString(writer, "baseRefName", baseRefName);
					GraphQLInputWriter.WriteOptionalString(writer, "headRefName", headRefName);
					GraphQLInputWriter.WriteOptionalStrings(writer, "labels", labels);
					writer.WriteStartObject("orderBy");
					writer.WriteString("field", orderBy.Field == IssueOrderField.UpdatedAt ? "UPDATED_AT" : "CREATED_AT");
					writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
					writer.WriteEndObject();
					writer.WriteStartArray("states");
					foreach (var state in states)
						writer.WriteStringValue(state switch
						{
							PullRequestState.Open => "OPEN",
							PullRequestState.Closed => "CLOSED",
							PullRequestState.Merged => "MERGED",
							_ => throw new ArgumentOutOfRangeException(nameof(states), state, "Unknown pull request state."),
						});
					writer.WriteEndArray();
				},
				cancellationToken);
			var connection = response.Result?.PullRequests
				?? throw new InvalidDataException("GitHub returned an incomplete user pull requests response.");
			var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
			foreach (var pullRequest in items)
				pullRequest.UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime();
			return new(items, connection.PageInfo);
		}
	}
}
