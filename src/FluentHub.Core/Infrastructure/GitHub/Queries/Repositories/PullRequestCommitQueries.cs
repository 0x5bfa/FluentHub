// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class PullRequestCommitQueries
	{
		private const string Query = """
			query PullRequestCommits($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    pullRequest(number: $number) {
			      commits(first: 30) {
			        nodes {
			          commit {
			            abbreviatedOid committedDate message messageHeadline oid
			            author { avatarUrl(size: 500) user { login } }
			            repository { name owner { login } }
			          }
			        }
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PullRequestCommitQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<Commit>> GetAllAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				Query,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer => WriteVariables(writer, owner, name, number),
				cancellationToken);
			return response.Result?.PullRequest?.Commits?.Nodes?
				.Where(node => node?.Commit is not null).Select(node => node!.Commit!).ToList()
				?? throw new InvalidDataException("GitHub returned an incomplete pull request commits response.");
		}

		private static void WriteVariables(System.Text.Json.Utf8JsonWriter writer, string owner, string name, int number)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
			writer.WriteNumber("number", number);
		}
	}
}
