using Octokit.GraphQL.Core;

using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Users
{
	public class ProjectQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public ProjectQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Project>> GetPageAsync(
			string login,
			PageRequest page,
			OctokitGraphQLModel.ProjectOrder? orderBy = null,
			string? search = null,
			IEnumerable<OctokitGraphQLModel.ProjectState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.User(login)
				.Projects(
					page.First,
					page.After,
					page.Last,
					page.Before,
					orderBy is null ? null! : new Arg<OctokitGraphQLModel.ProjectOrder>(orderBy),
					search,
					states is null ? null! : new Arg<IEnumerable<OctokitGraphQLModel.ProjectState>>(states))
				.Select(connection => new ProjectConnection
				{
					Edges = connection.Edges.Select(edge => (ProjectEdge?)new ProjectEdge
					{
						Node = edge.Node.Select(x => new Project
						{
							Body = x.Body,
							Closed = x.Closed,
							Id = x.Id,
							Name = x.Name,
							Number = x.Number,
							State = (ProjectState)x.State,
							Url = x.Url,
							ViewerCanUpdate = x.ViewerCanUpdate,

							ClosedAt = x.ClosedAt,
							CreatedAt = x.CreatedAt,
							UpdatedAt = x.UpdatedAt,

							Progress = x.Progress.Select(y => new ProjectProgress
							{
								DoneCount = y.DoneCount,
								DonePercentage = y.DonePercentage,
								Enabled = y.Enabled,
								InProgressCount = y.InProgressCount,
								InProgressPercentage = y.InProgressPercentage,
								TodoCount = y.TodoCount,
								TodoPercentage = y.TodoPercentage,
							})
					.Single(),
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

			return new PageResult<Project>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<Project> GetAsync(string login, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.User(login)
				.Project(number)
				.Select(x => new Project
				{
					Body = x.Body,
					Closed = x.Closed,
					Id = x.Id,
					Name = x.Name,
					Number = x.Number,
					State = (ProjectState)x.State,
					Url = x.Url,
					ViewerCanUpdate = x.ViewerCanUpdate,

					ClosedAt = x.ClosedAt,
					CreatedAt = x.CreatedAt,
					UpdatedAt = x.UpdatedAt,

					Progress = x.Progress.Select(y => new ProjectProgress
					{
						DoneCount = y.DoneCount,
						DonePercentage = y.DonePercentage,
						Enabled = y.Enabled,
						InProgressCount = y.InProgressCount,
						InProgressPercentage = y.InProgressPercentage,
						TodoCount = y.TodoCount,
						TodoPercentage = y.TodoPercentage,
					})
					.Single(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
