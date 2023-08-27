namespace FluentHub.Octokit.Queries.Users
{
	public class FollowersQueries
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
				.Followers(first, after, last, before)
				.Select(connection => new FollowerConnection
				{
					Edges = connection.Edges.Select(edge => new UserEdge
					{
						Node = edge.Node.Select(x => new User
						{
							AvatarUrl = x.AvatarUrl(500),
							Name = x.Name,
							Bio = x.Bio,
							Login = x.Login,
							Id = x.Id,
						})
						.Single()
					})
					.ToList(),

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
