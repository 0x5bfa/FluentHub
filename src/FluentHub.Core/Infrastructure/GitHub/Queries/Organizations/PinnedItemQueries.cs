// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Organizations
{
	public class PinnedItemQueries
	{
		private const string Query = """
			query OrganizationPinnedRepositories($login: String!) {
			  result: organization(login: $login) {
			    pinnedItems(first: 6) {
			""" + PinnedRepositoryQuery.Nodes + """
			    }
			  }
			}
			""" + PinnedRepositoryQuery.Fields;

		private readonly IGitHubApiClient _gitHub;

		public PinnedItemQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<Repository>> GetAllAsync(string org, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultPinnedRepositoriesResult,
				writer => writer.WriteString("login", org),
				cancellationToken);
			return PinnedRepositoryQuery.ToList(response.Result?.PinnedItems.Nodes
				?? throw new InvalidDataException("GitHub returned an incomplete pinned repositories response."));
		}
	}
}
