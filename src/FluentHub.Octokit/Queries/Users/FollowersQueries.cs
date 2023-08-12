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
				.Select(root => new FollowerConnection
				{
					Edges = root.Edges.Select(x => new UserEdge
					{
						Node = x.Node.Select(x => new User
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
