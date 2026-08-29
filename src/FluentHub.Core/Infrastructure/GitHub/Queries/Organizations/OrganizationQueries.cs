// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Organizations
{
	public partial class OrganizationQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<Organization>>]
		private const string Query = """
			query Organization($login: String!) {
			  result: organization(login: $login) {
			    avatarUrl(size: 500)
			    description
			    email
			    id
			    isVerified
			    location
			    login
			    name
			    twitterUsername
			    url
			    viewerCanChangePinnedItems
			    viewerCanSponsor
			    viewerIsAMember
			    viewerIsFollowing
			    viewerIsSponsoring
			    websiteUrl
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public OrganizationQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public Task<Organization> GetAsync(string org, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(org);
			if (_cache is null)
				return GetUncachedAsync(org, cancellationToken);

			var key = CacheKey.ForAccount(_gitHub.CachePartition, "organizations", org.Trim().ToLowerInvariant());
			return _cache.GetOrCreateAsync(
				key,
				CachePolicies.Organization,
				GitHubCacheSerializers.Organization,
				token => GetUncachedAsync(org, token),
				cancellationToken);
		}

		private async Task<Organization> GetUncachedAsync(string org, CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultOrganization,
				writer => writer.WriteString("login", org),
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException($"GitHub organization '{org}' was not found.");
		}
	}
}
