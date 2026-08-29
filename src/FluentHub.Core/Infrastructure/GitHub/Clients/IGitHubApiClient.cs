// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using GraphQL;
using GraphQL.Client.Abstractions;

namespace FluentHub.Core.Infrastructure.GitHub.Clients
{
	public interface IGitHubApiClient
	{
		string CachePartition => "anonymous";

		Task<T> RunRestAsync<T>(
			Func<OctokitRest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default);

		Task<T> RunGraphQLAsync<T>(
			ICompiledQuery<T> query,
			CancellationToken cancellationToken = default);

		Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default);

	}
}
