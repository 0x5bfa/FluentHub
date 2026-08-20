using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class InsightQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public InsightQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task GetContributorsAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			// just testing (one of those api requests requires push access rights)
			//var contributors = await _gitHub.RunRestAsync(
			//	client => client.Repository.Statistics.GetContributors(owner, name),
			//	cancellationToken);
		}
	}
}
