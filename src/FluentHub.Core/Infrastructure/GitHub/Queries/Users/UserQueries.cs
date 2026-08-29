using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class UserQueries
	{
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
			var query = new Query()
				.User(login)
				.Select(x => new User
				{
					AvatarUrl = x.AvatarUrl(500),
					Bio = x.Bio,
					Company = x.Company,
					Email = x.Email,
					IsCampusExpert = x.IsCampusExpert,
					IsBountyHunter = x.IsBountyHunter,
					IsDeveloperProgramMember = x.IsDeveloperProgramMember,
					IsEmployee = x.IsEmployee,
					IsGitHubStar = x.IsGitHubStar,
					IsViewer = x.IsViewer,
					Location = x.Location,
					Login = x.Login,
					Name = x.Name,
					TwitterUsername = x.TwitterUsername,
					ViewerIsFollowing = x.ViewerIsFollowing,
					WebsiteUrl = x.WebsiteUrl,

					Followers = x.Followers(null, null, null, null).Select(followers => new FollowerConnection
					{
						TotalCount = followers.TotalCount,
					})
					.SingleOrDefault(),

					Following = x.Following(null, null, null, null).Select(following => new FollowingConnection
					{
						TotalCount = following.TotalCount,
					})
					.SingleOrDefault(),

					Status = x.Status.Select(status => new UserStatus
					{
						Emoji = status.Emoji,
						Message = status.Message,
						IndicatesLimitedAvailability = status.IndicatesLimitedAvailability,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
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
			var query = new Query()
				.Repository(name: login, owner: login)
				.Select(repository => new
				{
					repository.IsPrivate,
					repository.Name,
					DefaultBranchName = repository.DefaultBranchRef
						.Select(reference => reference.Name)
						.SingleOrDefault(),
					OwnerLogin = repository.Owner
						.Select(owner => owner.Login)
						.Single(),
					Markdown = repository.Object(expression: "HEAD:README.md")
						.Cast<OctokitGraphQLModel.Blob>()
						.Select(blob => blob.Text)
						.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			if (response is null
				|| response.IsPrivate
				|| string.IsNullOrWhiteSpace(response.DefaultBranchName)
				|| string.IsNullOrWhiteSpace(response.Markdown)
				|| string.IsNullOrWhiteSpace(response.Name)
				|| string.IsNullOrWhiteSpace(response.OwnerLogin))
			{
				return new();
			}

			return new()
			{
				DefaultBranchName = response.DefaultBranchName,
				Markdown = response.Markdown,
				OwnerLogin = response.OwnerLogin,
				RepositoryName = response.Name,
			};
		}

		public async Task<string> GetViewerLoginAsync(CancellationToken cancellationToken = default)
		{
			var user = await _gitHub.RunRestAsync(
				(client, token) => client.Users.GetAuthenticatedAsync(token),
				cancellationToken);
			var login = user.Login?.Trim();

			if (string.IsNullOrWhiteSpace(login))
				throw new InvalidOperationException("GitHub did not return a login for the authenticated user.");

			return login;
		}
	}
}
