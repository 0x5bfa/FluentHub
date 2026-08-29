// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class IssueQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string Query = """
			query UserIssues($login: String!, $first: Int, $after: String, $last: Int, $before: String, $filterBy: IssueFilters, $labels: [String!], $orderBy: IssueOrder, $states: [IssueState!]) {
			  result: user(login: $login) {
			    issues(first: $first, after: $after, last: $last, before: $before, filterBy: $filterBy, labels: $labels, orderBy: $orderBy, states: $states) {
			      edges {
			        node {
			          closed number title updatedAt
			          repository { name owner { avatarUrl(size: 500) id login } }
			          comments { totalCount }
			          labels(first: 10) { nodes { color description name } }
			        }
			      }
			      pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public IssueQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<Issue>> GetPageAsync(
			string login,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetUserIssuePageAsync(login, page, filters, cancellationToken);

		public Task<RepositoryItemFilterOptions> GetFilterOptionsAsync(
			string login,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetUserFilterOptionsAsync(login, false, cancellationToken);

		public async Task<PageResult<Issue>> GetPageAsync(
			string login,
			PageRequest page,
			IssueFilters? filterBy = null,
			IEnumerable<string>? labels = null,
			IssueOrder? orderBy = null,
			IEnumerable<IssueState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			orderBy ??= new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };
			states ??= [IssueState.Open];
			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
					if (filterBy is not null)
					{
						writer.WriteStartObject("filterBy");
						GraphQLInputWriter.WriteOptionalString(writer, "assignee", filterBy.Assignee);
						GraphQLInputWriter.WriteOptionalStrings(writer, "labels", filterBy.Labels);
						GraphQLInputWriter.WriteOptionalString(writer, "milestone", filterBy.Milestone);
						GraphQLInputWriter.WriteOptionalString(writer, "type", filterBy.Type);
						writer.WriteEndObject();
					}
					GraphQLInputWriter.WriteOptionalStrings(writer, "labels", labels);
					writer.WriteStartObject("orderBy");
					writer.WriteString("field", orderBy.Field == IssueOrderField.UpdatedAt ? "UPDATED_AT" : "CREATED_AT");
					writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
					writer.WriteEndObject();
					writer.WriteStartArray("states");
					foreach (var state in states)
						writer.WriteStringValue(state == IssueState.Open ? "OPEN" : "CLOSED");
					writer.WriteEndArray();
				},
				cancellationToken);
			var connection = response.Result?.Issues
				?? throw new InvalidDataException("GitHub returned an incomplete user issues response.");
			var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
			foreach (var issue in items)
				issue.UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime();
			return new(items, connection.PageInfo);
		}
	}
}
