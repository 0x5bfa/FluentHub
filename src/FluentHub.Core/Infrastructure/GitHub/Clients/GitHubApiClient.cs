// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace FluentHub.Core.Infrastructure.GitHub.Clients
{
	public sealed class GitHubApiClient : IGitHubApiClient
	{
		private readonly GitHubSessionManager _sessionManager;

		public GitHubApiClient(GitHubSessionManager sessionManager)
			=> _sessionManager = sessionManager;

		public string CachePartition
			=> _sessionManager.CachePartition;

		public async Task<T> RunRestAsync<T>(
			Func<OctokitRest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(operation);

			var session = _sessionManager.GetRequiredSession();
			return await operation(session.Rest, cancellationToken).ConfigureAwait(false);
		}

		public Task<T> RunGraphQLAsync<T>(
			string query,
			JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null,
			CancellationToken cancellationToken = default)
			=> _sessionManager.GetRequiredSession().GraphQL.ExecuteAsync(
				query,
				dataTypeInfo,
				writeVariables,
				cancellationToken);
	}
}
