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
				.Select(connection => new OrganizationConnection
				{
					Edges = connection.Edges.Select(edge => new OrganizationEdge
					{
						Node = edge.Node.Select(x => new Organization
						{
							AvatarUrl = x.AvatarUrl(500),
							Description = x.Description,
							Name = x.Name,
							Login = x.Login,
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
