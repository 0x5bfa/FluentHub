// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class ProjectV2Queries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string Query = """
			query UserProjects($login: String!, $first: Int, $after: String, $last: Int, $before: String) {
			  result: user(login: $login) {
			""" + ProjectV2Query.Selection + """
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public ProjectV2Queries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<ProjectV2>> GetPageAsync(
			string login,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
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
			return ProjectV2Query.ToPage(response.Result?.ProjectsV2
				?? throw new InvalidDataException("GitHub returned an incomplete user projects response."));
		}
	}
}
