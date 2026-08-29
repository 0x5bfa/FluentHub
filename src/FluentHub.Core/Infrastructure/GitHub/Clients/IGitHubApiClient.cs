// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace FluentHub.Core.Infrastructure.GitHub.Clients
{
	public interface IGitHubApiClient
	{
		string CachePartition
		{
			get
			{
				return "anonymous";
			}
		}

		Task<T> RunRestAsync<T>(
			Func<OctokitRest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default);

		Task<T> RunGraphQLAsync<T>(GraphQLOperation<T> operation, JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default);

		Task<T> RunDynamicGraphQLAsync<T>(string query, JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default)
		{
			throw new NotSupportedException("Dynamic GraphQL operations are not supported by this client.");
		}
	}
}
