using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class PinnedItemQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public PinnedItemQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Repository>> GetAllAsync(string login, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.User(login)
				.PinnedItems(first: 6)
				.Nodes
				.OfType<OctokitGraphQLModel.Repository>()
				.Select(x => new Repository
				{
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
					{
						Name = y.Name,
						Color = y.Color,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response.ToList();
		}

		public async Task<List<Repository>> GetAllPinnableItemsAsync(string login, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.User(login)
				.PinnableItems(first: 6)
				.Nodes
				.OfType<OctokitGraphQLModel.Repository>()
				.Select(x => new Repository
				{
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
					{
						Name = y.Name,
						Color = y.Color,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response.ToList();
		}

		public async Task<(List<Repository>, List<Repository>)> GetAllPinnableAndPinnedItemsAsync(string login, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.User(login)
				.Select(user => new
				{
					PinnableItems = user.PinnableItems(20, null, null, null, null).Nodes.OfType<OctokitGraphQLModel.Repository>().Select(x => new Repository
					{
						Description = x.Description,
						IsFork = x.IsFork,
						IsInOrganization = x.IsInOrganization,
						Name = x.Name,
						NameWithOwner = x.NameWithOwner,
						StargazerCount = x.StargazerCount,
						ViewerHasStarred = x.ViewerHasStarred,

						Owner = x.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.Single(),

						PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
						{
							Name = y.Name,
							Color = y.Color,
						})
						.SingleOrDefault(),
					})
					.ToList(),

					PinnedItems = user.PinnedItems(6, null, null, null, null).Nodes.OfType<OctokitGraphQLModel.Repository>().Select(x => new Repository
					{
						Description = x.Description,
						IsFork = x.IsFork,
						IsInOrganization = x.IsInOrganization,
						Name = x.Name,
						NameWithOwner = x.NameWithOwner,
						StargazerCount = x.StargazerCount,
						ViewerHasStarred = x.ViewerHasStarred,

						Owner = x.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.Single(),

						PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
						{
							Name = y.Name,
							Color = y.Color,
						})
						.SingleOrDefault(),
					})
					.ToList(),
				})
				
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return (response.PinnableItems, response.PinnedItems);
		}
	}
}
