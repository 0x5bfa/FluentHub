using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class ProjectV2Queries
	{
		private readonly IGitHubApiClient _gitHub;

		public ProjectV2Queries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Project>> GetAllAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.ProjectsV2(first: 30)
				.Nodes
				.Select(x => new Project
				{
					//Body = x.Body,
					//Closed = x.Closed,
					//Id = x.Id,
					//Name = x.Name,
					//Number = x.Number,
					//State = (ProjectState)x.State,
					//Url = x.Url,
					//ViewerCanUpdate = x.ViewerCanUpdate,

					//ClosedAt = x.ClosedAt,
					//CreatedAt = x.CreatedAt,
					//UpdatedAt = x.UpdatedAt,

					//Progress = x.Progress.Select(y => new ProjectProgress
					//{
					//	DoneCount = y.DoneCount,
					//	DonePercentage = y.DonePercentage,
					//	Enabled = y.Enabled,
					//	InProgressCount = y.InProgressCount,
					//	InProgressPercentage = y.InProgressPercentage,
					//	TodoCount = y.TodoCount,
					//	TodoPercentage = y.TodoPercentage,
					//})
					//.Single(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response.ToList();
		}

		public async Task<Project> GetAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.ProjectV2(number)
				.Select(x => new Project
				{
					//Body = x.Body,
					//Closed = x.Closed,
					//Id = x.Id,
					//Name = x.Name,
					//Number = x.Number,
					//State = (ProjectState)x.State,
					//Url = x.Url,
					//ViewerCanUpdate = x.ViewerCanUpdate,

					//ClosedAt = x.ClosedAt,
					//CreatedAt = x.CreatedAt,
					//UpdatedAt = x.UpdatedAt,

					//Progress = x.Progress.Select(y => new ProjectProgress
					//{
					//	DoneCount = y.DoneCount,
					//	DonePercentage = y.DonePercentage,
					//	Enabled = y.Enabled,
					//	InProgressCount = y.InProgressCount,
					//	InProgressPercentage = y.InProgressPercentage,
					//	TodoCount = y.TodoCount,
					//	TodoPercentage = y.TodoPercentage,
					//})
					//.Single(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
