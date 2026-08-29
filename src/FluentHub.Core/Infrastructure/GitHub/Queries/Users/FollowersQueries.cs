// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class FollowersQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string Query = """
			query Followers($login: String!, $first: Int, $after: String, $last: Int, $before: String) {
			  result: user(login: $login) {
			    followers(first: $first, after: $after, last: $last, before: $before) {
			      edges { node { avatarUrl(size: 500) name bio login id } }
			      pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public FollowersQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<PageResult<User>> GetPageAsync(
			string login,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			ArgumentNullException.ThrowIfNull(page);

			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
				},
				cancellationToken);
			var connection = response.Result?.Followers
				?? throw new InvalidDataException("GitHub returned an incomplete followers response.");

			return new(
				connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [],
				connection.PageInfo);
		}
	}
}
