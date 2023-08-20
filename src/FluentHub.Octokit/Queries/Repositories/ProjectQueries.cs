namespace FluentHub.Octokit.Queries.Repositories
{
	public class ProjectQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string owner,
			string name,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			OctokitGraphQLModel.ProjectOrder? orderBy = null,
			string? search = null,
			IEnumerable<OctokitGraphQLModel.ProjectState>? states = null)
		{
			var query = new Query()
				.Repository(name, owner)
				.Projects(
					first,
					after,
					last,
					before,
					orderBy,
					search,
					states is not null ? new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.ProjectState>>(states) : null)
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
							}).Single(),
						}).Single(),
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

		public async Task<Project> GetAsync(string owner, string name, int number)
		{
			var query = new Query()
				.Repository(name, owner)
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
