using Octokit.GraphQL.Core;

namespace FluentHub.Octokit.Queries.Users
{
	public class PackageQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string login,
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
				.User(login)
				.Packages(
					first,
					after,
					last,
					before,
					names is null ? null : new Arg<IEnumerable<string>>(names!),
					orderBy is null ? null : new Arg<OctokitGraphQLModel.PackageOrder>(orderBy!),
					packageType is null ? null : new Arg<OctokitGraphQLModel.PackageType>((OctokitGraphQLModel.PackageType)packageType),
					repositoryId)
				.Select(connection => new PackageConnection
				{
					Edges = connection.Edges.Select(edge => new PackageEdge
					{
						Node = edge.Node.Select(x => new Package
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
						}).Single()
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
	}
}
