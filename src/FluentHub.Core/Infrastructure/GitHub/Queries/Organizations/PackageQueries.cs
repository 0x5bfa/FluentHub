// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Organizations
{
	public class PackageQueries
	{
		private const string Query = """
			query OrganizationPackages($login: String!) {
			  result: organization(login: $login) {
			    packages(first: 30) {
			""" + PackageQuery.NodesSelection + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PackageQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<Package>> GetAllAsync(string org, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultOrganization,
				writer => writer.WriteString("login", org),
				cancellationToken);
			return response.Result?.Packages?.Nodes?.Where(package => package is not null).Select(package => package!).ToList()
				?? throw new InvalidDataException("GitHub returned an incomplete organization packages response.");
		}
	}
}
