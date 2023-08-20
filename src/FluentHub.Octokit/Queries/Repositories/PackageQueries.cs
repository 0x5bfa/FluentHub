namespace FluentHub.Octokit.Queries.Repositories
{
	public class PackageQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string owner,
			string name,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			IEnumerable<string>? names = null,
			OctokitGraphQLModel.PackageOrder? orderBy = null,
			OctokitGraphQLModel.PackageType? packageType = null,
			ID? repositoryId = null)
		{
			var query = new Query()
				.Repository(name, owner)
				.Packages(
					first,
					after,
					last,
					before,
					names is not null ? new OctokitGraphQLCore.Arg<IEnumerable<string>>(names) : null,
					orderBy,
					packageType,
					repositoryId)
				.Select(root => new PackageConnection
				{
					Edges = root.Edges.Select(x => new PackageEdge
					{
						Node = x.Node.Select(x => new Package
						{
							Id = x.Id,
							Name = x.Name,
							PackageType = (PackageType)x.PackageType,

							LatestVersion = x.LatestVersion.Select(lv => new PackageVersion
							{
								Version = lv.Version,
							}).SingleOrDefault(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							Statistics = x.Statistics.Select(stat => new PackageStatistics
							{
								DownloadsTotalCount = stat.DownloadsTotalCount,
							}).SingleOrDefault(),
						}).Single(),
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = root.PageInfo.EndCursor,
						HasNextPage = root.PageInfo.HasNextPage,
						HasPreviousPage = root.PageInfo.HasPreviousPage,
						StartCursor = root.PageInfo.StartCursor,
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
	}
}
