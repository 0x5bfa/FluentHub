// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using GraphQL;
using GraphQL.Client.Abstractions;

namespace FluentHub.Core.Clients
{
	public sealed class GitHubApiClient : IGitHubApiClient
	{
		private readonly GitHubSessionManager _sessionManager;

		public GitHubApiClient(GitHubSessionManager sessionManager)
			=> _sessionManager = sessionManager;

		public async Task<T> RunRestAsync<T>(
			Func<OctokitV3.IGitHubClient, Task<T>> operation,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(operation);

			var session = _sessionManager.GetRequiredSession();
			return await operation(session.Rest).WaitAsync(cancellationToken);
		}

		public Task<T> RunGraphQLAsync<T>(
			ICompiledQuery<T> query,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(query);

			return _sessionManager.GetRequiredSession().GraphQL.Run(
				query,
				cancellationToken: cancellationToken);
		}

		public Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			return _sessionManager.GetRequiredSession().RawGraphQL.SendQueryAsync<T>(
				request,
				cancellationToken);
		}

		public Task<HttpResponseMessage> SendRestAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			return _sessionManager.GetRequiredSession().RawRest.SendAsync(
				request,
				HttpCompletionOption.ResponseHeadersRead,
				cancellationToken);
		}
	}
}
