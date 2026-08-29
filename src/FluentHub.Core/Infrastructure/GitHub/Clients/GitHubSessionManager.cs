// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Security.Cryptography;
using FluentHub.Core.Application.Abstractions.Authentication;
using Octokit.Rest;
using Octokit.Transport;

namespace FluentHub.Core.Infrastructure.GitHub.Clients
{
	public sealed class GitHubSessionManager : IUserSession, IDisposable
	{
		private readonly Lock _syncRoot = new();
		private readonly List<GitHubSession> _sessions = [];
		private GitHubSession? _current;
		private string _cachePartition = "anonymous";
		private bool _disposed;

		public bool IsAuthenticated
			=> Volatile.Read(ref _current) is not null;

		internal string CachePartition
			=> Volatile.Read(ref _cachePartition);

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
				Volatile.Write(ref _cachePartition, CreateCachePartition(accessToken));
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
				Volatile.Write(ref _cachePartition, "anonymous");

				foreach (var session in _sessions)
					session.Dispose();

				_sessions.Clear();
			}
		}

		private static string CreateCachePartition(string accessToken)
		{
			var hash = SHA256.HashData(Encoding.UTF8.GetBytes(accessToken));
			return "account-" + Convert.ToHexString(hash.AsSpan(0, 8)).ToLowerInvariant();
		}

		internal sealed class GitHubSession : IDisposable
		{
			public GitHubSession(string accessToken)
			{
				Transport = GitHubHttpClient.Create(accessToken, "FluentHub");
				Rest = new GitHubRestClient(Transport);
				GraphQL = new GitHubGraphQLClient(Transport);
			}

			public GitHubRestClient Rest { get; }

			public GitHubGraphQLClient GraphQL { get; }

			public GitHubHttpClient Transport { get; }

			public void Dispose()
			{
				Transport.Dispose();
			}
		}
	}
}
