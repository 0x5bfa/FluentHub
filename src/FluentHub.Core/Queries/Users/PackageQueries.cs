using Octokit.GraphQL.Core;

using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class PackageQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public PackageQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Package>> GetPageAsync(
			string login,
			PageRequest page,
			IEnumerable<string>? names = null,
			OctokitGraphQLModel.PackageOrder? orderBy = null,
			OctokitGraphQLModel.PackageType? packageType = null,
			ID? repositoryId = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.User(login)
				.Packages(
					page.First,
					page.After,
					page.Last,
					page.Before,
					names is null ? null! : new Arg<IEnumerable<string>>(names!),
					orderBy is null ? null! : new Arg<OctokitGraphQLModel.PackageOrder>(orderBy!),
					packageType is null ? null : new Arg<OctokitGraphQLModel.PackageType>((OctokitGraphQLModel.PackageType)packageType),
					repositoryId)
				.Select(connection => new PackageConnection
				{
					Edges = connection.Edges.Select(edge => (PackageEdge?)new PackageEdge
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

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Package>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
