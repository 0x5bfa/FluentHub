using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class OrganizationQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public OrganizationQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Organization>> GetPageAsync(
			string login,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.User(login)
				.Organizations(page.First, page.After, page.Last, page.Before)
				.Select(connection => new OrganizationConnection
				{
					Edges = connection.Edges.Select(edge => (OrganizationEdge?)new OrganizationEdge
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

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Organization>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
