// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class BlobQueries
	{
		private const string Query = """
			query Blob($name: String!, $owner: String!, $expression: String!) {
			  result: repository(name: $name, owner: $owner) {
			    object(expression: $expression) {
			      ... on Blob {
			        abbreviatedOid
			        byteSize
			        commitUrl
			        id
			        isBinary
			        isTruncated
			        oid
			        text
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public BlobQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<Blob> GetAsync(
			string name,
			string owner,
			string branch,
			string path,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryObjectResultBlob,
				writer =>
				{
					writer.WriteString("name", name);
					writer.WriteString("owner", owner);
					writer.WriteString("expression", $"{branch}:{path}");
				},
				cancellationToken);
			return response.Result?.Object
				?? throw new InvalidDataException($"GitHub blob '{owner}/{name}:{branch}:{path}' was not found.");
		}
	}
}
