// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class ProjectV2Queries
	{
		private const string Query = """
			query RepositoryProjects($owner: String!, $name: String!, $first: Int, $after: String, $last: Int, $before: String) {
			  result: repository(owner: $owner, name: $name) {
			""" + ProjectV2Query.Selection + """
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public ProjectV2Queries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<ProjectV2>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					writer.WriteString("owner", owner);
					writer.WriteString("name", name);
					GraphQLInputWriter.WritePage(writer, page);
				},
				cancellationToken);
			return ProjectV2Query.ToPage(response.Result?.ProjectsV2
				?? throw new InvalidDataException("GitHub returned an incomplete repository projects response."));
		}
	}
}
