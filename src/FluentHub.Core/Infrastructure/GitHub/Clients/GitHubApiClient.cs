// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using GraphQL;
using GraphQL.Client.Abstractions;
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
			Func<OctokitV3.IGitHubClient, Task<T>> operation,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(operation);

			var session = _sessionManager.GetRequiredSession();
			return await operation(session.Rest).WaitAsync(cancellationToken);
		}

		public Task<T> GetRestAsync<T>(
			string relativeUri,
			JsonTypeInfo<T> responseTypeInfo,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(relativeUri);
			ArgumentNullException.ThrowIfNull(responseTypeInfo);

			return _sessionManager.GetRequiredSession().Transport.GetAsync(
				relativeUri,
				responseTypeInfo,
				cancellationToken);
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

			return _sessionManager.GetRequiredSession().Transport.SendAsync(
				request,
				cancellationToken);
		}
	}
}
