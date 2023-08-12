namespace FluentHub.Octokit.Queries.Users
{
	public class OrganizationQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string login,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null)
		{
			var query = new Query()
				.User(login)
				.Organizations(first, after, last, before)
				.Select(root => new OrganizationConnection
				{
					Edges = root.Edges.Select(x => new OrganizationEdge
					{
						Node = x.Node.Select(x => new Organization
						{
							AvatarUrl = x.AvatarUrl(500),
							Description = x.Description,
							Name = x.Name,
							Login = x.Login,
						}).Single()
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
