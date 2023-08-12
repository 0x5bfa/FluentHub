using Octokit.GraphQL.Core;

namespace FluentHub.Octokit.Queries.Users
{
	public class ProjectQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string login,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			OctokitGraphQLModel.ProjectOrder? orderBy = null,
			string? search = null,
			IEnumerable<OctokitGraphQLModel.ProjectState>? states = null)
		{
			var query = new Query()
				.User(login)
				.Projects(
					first,
					after,
					last,
					before,
					orderBy is null ? null : new Arg<OctokitGraphQLModel.ProjectOrder>(orderBy),
					search,
					states is null ? null : new Arg<IEnumerable<OctokitGraphQLModel.ProjectState>>(states))
				.Select(root => new ProjectConnection
				{
					Edges = root.Edges.Select(x => new ProjectEdge
					{
						Node = x.Node.Select(x => new Project
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

		public async Task<Project> GetAsync(string login, int number)
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

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
