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
		{
			_sessionManager = sessionManager;
		}

		public string CachePartition
		{
			get
			{
				return _sessionManager.CachePartition;
			}
		}

		public async Task<T> RunRestAsync<T>(
			Func<OctokitRest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(operation);

			var session = _sessionManager.GetRequiredSession();
			return await operation(session.Rest, cancellationToken).ConfigureAwait(false);
		}

		public Task<T> RunGraphQLAsync<T>(GraphQLOperation<T> operation, JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default)
		{
			return _sessionManager.GetRequiredSession().GraphQL.ExecuteAsync(
				operation, dataTypeInfo, writeVariables, cancellationToken);
		}

		public Task<T> RunDynamicGraphQLAsync<T>(string query, JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default)
		{
			return _sessionManager.GetRequiredSession().GraphQL.ExecuteDynamicAsync(
				query, dataTypeInfo, writeVariables, cancellationToken);
		}
	}
}
