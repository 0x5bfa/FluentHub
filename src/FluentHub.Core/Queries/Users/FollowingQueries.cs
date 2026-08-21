using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class FollowingQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public FollowingQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<User>> GetPageAsync(
			string login,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

				var query = new Query()
				.User(login)
				.Following(page.First, page.After, page.Last, page.Before)
				.Select(connection => new FollowingConnection
				{
					Edges = connection.Edges.Select(edge => (UserEdge?)new UserEdge
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

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<User>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
