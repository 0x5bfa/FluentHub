using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Organizations
{
	public class ProjectV2Queries
	{
		private readonly IGitHubApiClient _gitHub;

		public ProjectV2Queries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Project>> GetAllAsync(string org, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Organization(org)
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

		public async Task<Project> GetAsync(string org, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Organization(org)
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
