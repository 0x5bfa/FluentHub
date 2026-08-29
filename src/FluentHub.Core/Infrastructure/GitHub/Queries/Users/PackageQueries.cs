// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class PackageQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string Query = """
			query UserPackages($login: String!, $first: Int, $after: String, $last: Int, $before: String, $names: [String!], $orderBy: PackageOrder, $packageType: PackageType, $repositoryId: ID) {
			  result: user(login: $login) {
			    packages(first: $first, after: $after, last: $last, before: $before, names: $names, orderBy: $orderBy, packageType: $packageType, repositoryId: $repositoryId) {
			""" + PackageQuery.Selection + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PackageQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<Package>> GetPageAsync(
			string login,
			PageRequest page,
			IEnumerable<string>? names = null,
			PackageOrder? orderBy = null,
			PackageType? packageType = null,
			ID? repositoryId = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					PackageQuery.WriteFilters(writer, page, names, orderBy, packageType, repositoryId);
				},
				cancellationToken);
			return PackageQuery.ToPage(response.Result?.Packages
				?? throw new InvalidDataException("GitHub returned an incomplete user packages response."));
		}
	}
}
