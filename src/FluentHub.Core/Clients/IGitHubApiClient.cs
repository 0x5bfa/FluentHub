// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using GraphQL;
using GraphQL.Client.Abstractions;

namespace FluentHub.Core.Clients
{
	public interface IGitHubApiClient
	{
		string CachePartition => "anonymous";

		Task<T> RunRestAsync<T>(
			Func<OctokitV3.IGitHubClient, Task<T>> operation,
			CancellationToken cancellationToken = default);

		Task<T> RunGraphQLAsync<T>(
			ICompiledQuery<T> query,
			CancellationToken cancellationToken = default);

		Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default);

		Task<HttpResponseMessage> SendRestAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken = default);
	}
}
