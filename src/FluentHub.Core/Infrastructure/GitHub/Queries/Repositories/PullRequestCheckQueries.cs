// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public partial class PullRequestCheckQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<Repository>>]
		private const string Query = """
			query PullRequestChecks($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    pullRequest(number: $number) {
			      commits(last: 1) {
			        nodes {
			          commit {
			            checkSuites(first: 20) {
			              nodes {
			                app { name logoBackgroundColor logoUrl(size: 100) }
			                checkRuns(first: 10) {
			                  nodes {
			                    name conclusion status detailsUrl title startedAt completedAt
			                    checkSuite {
			                      app { name }
			                      commit { abbreviatedOid }
			                      creator { login }
			                      workflowRun { runNumber }
			                    }
			                  }
			                }
			              }
			            }
			          }
			        }
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PullRequestCheckQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<CheckSuite>> GetAllAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					writer.WriteString("owner", owner);
					writer.WriteString("name", name);
					writer.WriteNumber("number", number);
				},
				cancellationToken);

			return response.Result?.PullRequest?.Commits?.Nodes?.FirstOrDefault()?.Commit?.CheckSuites?.Nodes?
				.Where(suite => suite is not null).Select(suite => suite!).ToList() ?? [];
		}
	}
}
