// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Net.Http.Headers;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace FluentHub.Core.Clients
{
	public sealed class GitHubSessionManager : IGitHubSessionManager, IDisposable
	{
		private readonly Lock _syncRoot = new();
		private readonly List<GitHubSession> _sessions = [];
		private GitHubSession? _current;
		private bool _disposed;

		public bool IsAuthenticated
			=> Volatile.Read(ref _current) is not null;

		public void SwitchAccount(string accessToken)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

			var session = new GitHubSession(accessToken);

			lock (_syncRoot)
			{
				if (_disposed)
				{
					session.Dispose();
					throw new ObjectDisposedException(nameof(GitHubSessionManager));
				}

				_sessions.Add(session);
				Volatile.Write(ref _current, session);
			}
		}

		internal GitHubSession GetRequiredSession()
			=> Volatile.Read(ref _current)
				?? throw new InvalidOperationException("The GitHub API session has not been initialized.");

		public void Dispose()
		{
			lock (_syncRoot)
			{
				if (_disposed)
					return;

				_disposed = true;
				Volatile.Write(ref _current, null);

				foreach (var session in _sessions)
					session.Dispose();

				_sessions.Clear();
			}
		}

		internal sealed class GitHubSession : IDisposable
		{
			public GitHubSession(string accessToken)
			{
				Rest = new OctokitV3.GitHubClient(new OctokitV3.ProductHeaderValue("FluentHub"))
				{
					Credentials = new OctokitV3.Credentials(accessToken),
				};

				GraphQL = new Connection(new global::Octokit.GraphQL.ProductHeaderValue("FluentHub"), accessToken);

				RawGraphQL = new GraphQLHttpClient(
					"https://api.github.com/graphql",
					new NewtonsoftJsonSerializer());
				RawGraphQL.HttpClient.DefaultRequestHeaders.Authorization
					= new AuthenticationHeaderValue("Bearer", accessToken);
				RawGraphQL.HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FluentHub");

				RawRest = new HttpClient
				{
					BaseAddress = new Uri("https://api.github.com/"),
				};
				RawRest.DefaultRequestHeaders.Authorization
					= new AuthenticationHeaderValue("Bearer", accessToken);
				RawRest.DefaultRequestHeaders.UserAgent.ParseAdd("FluentHub");
			}

			public OctokitV3.IGitHubClient Rest { get; }

			public Connection GraphQL { get; }

			public GraphQLHttpClient RawGraphQL { get; }

			public HttpClient RawRest { get; }

			public void Dispose()
			{
				RawGraphQL.Dispose();
				RawRest.Dispose();
			}
		}
	}
}
