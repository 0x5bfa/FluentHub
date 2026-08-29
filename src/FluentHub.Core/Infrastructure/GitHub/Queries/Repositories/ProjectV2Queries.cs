using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class ProjectV2Queries
	{
		private readonly IGitHubApiClient _gitHub;

		public ProjectV2Queries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<PageResult<ProjectV2>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.Repository(name, owner)
				.ProjectsV2(page.First, page.After, page.Last, page.Before)
				.Select(connection => new ProjectV2Connection
				{
					Edges = connection.Edges.Select(edge => (ProjectV2Edge?)new ProjectV2Edge
					{
						Node = edge.Node.Select(project => new ProjectV2
						{
							Closed = project.Closed,
							ClosedAt = project.ClosedAt,
							CreatedAt = project.CreatedAt,
							Id = project.Id,
							Number = project.Number,
							Public = project.Public,
							Readme = project.Readme,
							ResourcePath = project.ResourcePath,
							ShortDescription = project.ShortDescription,
							Title = project.Title,
							UpdatedAt = project.UpdatedAt,
							Url = project.Url,
							ViewerCanUpdate = project.ViewerCanUpdate,
						}).Single(),
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

			return new PageResult<ProjectV2>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
