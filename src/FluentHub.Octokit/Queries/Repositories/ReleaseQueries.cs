using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class ReleaseQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public ReleaseQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Release>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			OctokitGraphQLModel.ReleaseOrder? orderBy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
			};

			var query = new Query()
				.Repository(name, owner)
				.Releases(page.First, page.After, page.Last, page.Before, orderBy)
				.Select(connection => new ReleaseConnection
				{
					Edges = connection.Edges.Select(edge => (ReleaseEdge?)new ReleaseEdge
					{
						Node = edge.Node.Select(x => new Release
						{
							Author = x.Author.Select(author => new User
							{
								Login = author.Login,
								AvatarUrl = author.AvatarUrl(500),
							}).Single(),

							DescriptionHTML = x.DescriptionHTML,
							IsDraft = x.IsDraft,
							IsLatest = x.IsLatest,
							IsPrerelease = x.IsPrerelease,
							Name = x.Name,
							PublishedAt = x.PublishedAt,
							PublishedAtHumanized = x.PublishedAt.Humanize(null, null),
							TagName = x.TagName,
						}).Single(),
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Release>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<Release> GetAsync(string owner, string name, string tagName, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.Release(tagName)
				.Select(x => new Release
				{
					DescriptionHTML = x.DescriptionHTML,
					IsDraft = x.IsDraft,
					IsLatest = x.IsLatest,
					IsPrerelease = x.IsPrerelease,
					Name = x.Name,
					PublishedAt = x.PublishedAt,
					PublishedAtHumanized = x.PublishedAt.Humanize(null, null),
					TagName = x.TagName,

					Author = x.Author.Select(author => new User
					{
						Login = author.Login,
						AvatarUrl = author.AvatarUrl(500),
					})
					.SingleOrDefault(),

					ReleaseAssets = x.ReleaseAssets(10, null, null, null, null).Select(assets => new ReleaseAssetConnection
					{
						Nodes = assets.Nodes.Select(asset => (ReleaseAsset?)new ReleaseAsset
						{
							Name = asset.Name,
							ContentType = asset.ContentType,
							DownloadCount = asset.DownloadCount,
							DownloadUrl = asset.DownloadUrl,
							Size = asset.Size,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					TagCommit = x.TagCommit.Select(commit => new Commit
					{
						AbbreviatedOid = commit.AbbreviatedOid,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
