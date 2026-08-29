// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class OrganizationQueries
	{
		private const string Query = """
			query UserOrganizations($login: String!, $first: Int, $after: String, $last: Int, $before: String) {
			  result: user(login: $login) {
			    organizations(first: $first, after: $after, last: $last, before: $before) {
			      edges { node { avatarUrl(size: 500) description name login } }
			      pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public OrganizationQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<PageResult<Organization>> GetPageAsync(
			string login,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
				},
				cancellationToken);
			var connection = response.Result?.Organizations
				?? throw new InvalidDataException("GitHub returned an incomplete organizations response.");

			return new(
				connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [],
				connection.PageInfo);
		}
	}
}
