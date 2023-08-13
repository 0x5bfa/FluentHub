namespace FluentHub.Octokit.Queries.Organizations
{
	public class PackageQueries
	{
		public async Task<List<Package>> GetAllAsync(string org)
		{
			var query = new Query()
				.Organization(org)
				.Packages(first: 30)
				.Nodes
				.Select(x => new Package
				{
					Id = x.Id,
					Name = x.Name,
					PackageType = (PackageType)x.PackageType,

					LatestVersion = x.LatestVersion.Select(lv => new PackageVersion
					{
						Version = lv.Version,
					})
					.SingleOrDefault(),

					Repository = x.Repository.Select(repo => new Repository
					{
						Name = repo.Name,
						Owner = repo.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),

					Statistics = x.Statistics.Select(stat => new PackageStatistics
					{
						DownloadsTotalCount = stat.DownloadsTotalCount,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response.ToList();
		}
	}
}
