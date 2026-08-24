using FluentHub.Core.Clients;
using FluentHub.Core.Caching;

namespace FluentHub.Core.Queries.Organizations
{
	public class OrganizationQueries
	{
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
			var query = new Query()
				.Organization(org)
				.Select(x => new Organization
				{
					AvatarUrl = x.AvatarUrl(500),
					Description = x.Description,
					Email = x.Email,
					Id = x.Id,
					IsVerified = x.IsVerified,
					Location = x.Location,
					Login = x.Login,
					Name = x.Name,
					TwitterUsername = x.TwitterUsername,
					Url = x.Url,
					ViewerCanChangePinnedItems = x.ViewerCanChangePinnedItems,
					ViewerCanSponsor = x.ViewerCanSponsor,
					ViewerIsAMember = x.ViewerIsAMember,
					ViewerIsFollowing = x.ViewerIsFollowing,
					ViewerIsSponsoring = x.ViewerIsSponsoring,
					WebsiteUrl = x.WebsiteUrl,
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
