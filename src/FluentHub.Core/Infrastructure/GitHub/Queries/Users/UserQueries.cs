// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class UserQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string UserQuery = """
			query User($login: String!) {
			  result: user(login: $login) {
			    avatarUrl(size: 500) bio company email
			    isCampusExpert isBountyHunter isDeveloperProgramMember isEmployee isGitHubStar isViewer
			    location login name twitterUsername viewerIsFollowing websiteUrl
			    followers { totalCount }
			    following { totalCount }
			    status { emoji message indicatesLimitedAvailability }
			  }
			}
			""";

		[GeneratedGraphQLOperation<GraphQLResult<ProfileReadmeQueryResult>>]
		private const string ProfileReadmeQuery = """
			query ProfileReadme($login: String!) {
			  result: repository(name: $login, owner: $login) {
			    isPrivate
			    name
			    defaultBranchRef { name }
			    owner { login }
			    object(expression: "HEAD:README.md") { ... on Blob { text } }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public UserQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public Task<User> GetAsync(string login, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			if (_cache is null)
				return GetUncachedAsync(login, cancellationToken);

			var key = CacheKey.ForAccount(_gitHub.CachePartition, "users", login.Trim().ToLowerInvariant());
			return _cache.GetOrCreateAsync(
				key,
				CachePolicies.User,
				GitHubCacheSerializers.User,
				token => GetUncachedAsync(login, token),
				cancellationToken);
		}

		private async Task<User> GetUncachedAsync(string login, CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				UserQueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer => writer.WriteString("login", login),
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException($"GitHub user '{login}' was not found.");
		}

		public Task<ProfileReadme> GetProfileReadmeAsync(string login, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			if (_cache is null)
				return GetProfileReadmeUncachedAsync(login, cancellationToken);

			var key = CacheKey.ForAccount(_gitHub.CachePartition, "profile-readmes-v2", login.Trim().ToLowerInvariant());
			return _cache.GetOrCreateAsync(
				key,
				CachePolicies.User,
				GitHubCacheSerializers.ProfileReadme,
				token => GetProfileReadmeUncachedAsync(login, token),
				cancellationToken);
		}

		private async Task<ProfileReadme> GetProfileReadmeUncachedAsync(
			string login,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				ProfileReadmeQueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultProfileReadmeQueryResult,
				writer => writer.WriteString("login", login),
				cancellationToken);
			var repository = response.Result;
			if (repository is null || repository.IsPrivate ||
				string.IsNullOrWhiteSpace(repository.DefaultBranchRef?.Name) ||
				string.IsNullOrWhiteSpace(repository.Object?.Text) ||
				string.IsNullOrWhiteSpace(repository.Name) ||
				string.IsNullOrWhiteSpace(repository.Owner?.Login))
			{
				return new();
			}

			return new()
			{
				DefaultBranchName = repository.DefaultBranchRef.Name,
				Markdown = repository.Object.Text,
				OwnerLogin = repository.Owner.Login,
				RepositoryName = repository.Name,
			};
		}

		public async Task<string> GetViewerLoginAsync(CancellationToken cancellationToken = default)
		{
			var user = await _gitHub.RunRestAsync(
				(client, token) => client.Users.GetAuthenticatedAsync(token),
				cancellationToken);
			var login = user.Login?.Trim();
			if (string.IsNullOrWhiteSpace(login))
				throw new InvalidOperationException("GitHub returned an authenticated user without a login.");
			return login;
		}
	}
}
