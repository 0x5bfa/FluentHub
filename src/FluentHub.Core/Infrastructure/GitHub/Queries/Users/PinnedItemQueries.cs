// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class PinnedItemQueries
	{
		private const string PinnedQuery = """
			query PinnedRepositories($login: String!) {
			  result: user(login: $login) {
			    pinnedItems(first: 6) {
			""" + PinnedRepositoryQuery.Nodes + """
			    }
			  }
			}
			""" + PinnedRepositoryQuery.Fields;

		private const string PinnableQuery = """
			query PinnableRepositories($login: String!) {
			  result: user(login: $login) {
			    pinnableItems(first: 6) {
			""" + PinnedRepositoryQuery.Nodes + """
			    }
			  }
			}
			""" + PinnedRepositoryQuery.Fields;

		private const string CombinedQuery = """
			query PinnableAndPinnedRepositories($login: String!) {
			  result: user(login: $login) {
			    pinnableItems(first: 20) {
			""" + PinnedRepositoryQuery.Nodes + """
			    }
			    pinnedItems(first: 6) {
			""" + PinnedRepositoryQuery.Nodes + """
			    }
			  }
			}
			""" + PinnedRepositoryQuery.Fields;

		private readonly IGitHubApiClient _gitHub;

		public PinnedItemQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<Repository>> GetAllAsync(string login, CancellationToken cancellationToken = default)
			=> PinnedRepositoryQuery.ToList((await ExecuteAsync(PinnedQuery, login, cancellationToken)).PinnedItems.Nodes);

		public async Task<List<Repository>> GetAllPinnableItemsAsync(string login, CancellationToken cancellationToken = default)
			=> PinnedRepositoryQuery.ToList((await ExecuteAsync(PinnableQuery, login, cancellationToken)).PinnableItems.Nodes);

		public async Task<(List<Repository>, List<Repository>)> GetAllPinnableAndPinnedItemsAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			var result = await ExecuteAsync(CombinedQuery, login, cancellationToken);
			return (
				PinnedRepositoryQuery.ToList(result.PinnableItems.Nodes),
				PinnedRepositoryQuery.ToList(result.PinnedItems.Nodes));
		}

		private async Task<PinnedRepositoriesResult> ExecuteAsync(
			string query,
			string login,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				query,
				GitHubGraphQLJsonContext.Default.GraphQLResultPinnedRepositoriesResult,
				writer => writer.WriteString("login", login),
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete pinned repositories response.");
		}
	}
}
