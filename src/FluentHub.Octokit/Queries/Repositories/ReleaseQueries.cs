namespace FluentHub.Octokit.Queries.Repositories
{
	public class ReleaseQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string owner,
			string name,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			OctokitGraphQLModel.ReleaseOrder? orderBy = null)
		{
			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
			};

			var query = new Query()
				.Repository(name, owner)
				.Releases(first, after, last, before, orderBy)
				.Select(connection => new ReleaseConnection
				{
					Edges = connection.Edges.Select(edge => new ReleaseEdge
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

			var response = await App.Connection.Run(query);

			var result = new OctokitQueryResult()
			{
				PageInfo = response.PageInfo,
				Response = response.Edges.Select(x => x.Node).ToList(),
			};

			return result;
		}

		public async Task<Release> GetAsync(string owner, string name, string tagName)
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
						Nodes = assets.Nodes.Select(asset => new ReleaseAsset
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

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
