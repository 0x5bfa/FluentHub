using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class PullRequestCheckQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public PullRequestCheckQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<CheckSuite>> GetAllAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.PullRequest(number)
				.Commits(last: 1)
				.Nodes
				.Select(x => x.Commit.Select(y => new Commit
				{
					CheckSuites = y.CheckSuites(20, null, null, null, null).Select(suites => new CheckSuiteConnection
					{
						Nodes = suites.Nodes.Select(suite => (CheckSuite?)new CheckSuite
						{
							App = suite.App.Select(app => new global::FluentHub.Core.Application.Models.App
							{
								Name = app.Name,
								LogoBackgroundColor = app.LogoBackgroundColor,
								LogoUrl = app.LogoUrl(100),
							})
							.SingleOrDefault(),

							CheckRuns = suite.CheckRuns(10, null, null, null, null).Select(runs => new CheckRunConnection
							{
								Nodes = runs.Nodes.Select(run => (CheckRun?)new CheckRun
								{
									Name = run.Name,
									Conclusion = (CheckConclusionState?)run.Conclusion,
									Status = (CheckStatusState)run.Status,
									DetailsUrl = run.DetailsUrl,
									Title = run.Title,
									StartedAt = run.StartedAt,
									CompletedAt = run.CompletedAt,

									CheckSuite = run.CheckSuite.Select(runsuite => new CheckSuite
									{
										App = runsuite.App.Select(runapp => new global::FluentHub.Core.Application.Models.App
										{
											Name = runapp.Name,
										})
										.SingleOrDefault(),

										Commit = runsuite.Commit.Select(commit => new Commit
										{
											AbbreviatedOid = commit.AbbreviatedOid,
										})
										.SingleOrDefault(),

										Creator = runsuite.Creator.Select(creator => new User
										{
											Login = creator.Login,
										})
										.SingleOrDefault(),

										WorkflowRun = runsuite.WorkflowRun.Select(workflowrun => new WorkflowRun
										{
											RunNumber = workflowrun.RunNumber,
										})
										.SingleOrDefault(),
									})
									.SingleOrDefault(),
								})
								.ToList(),
							})
							.SingleOrDefault(),
						})
						.ToList()
					})
					.SingleOrDefault(),
				})
				.Single())
				.Compile();

			var result = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return result.ToList().FirstOrDefault()?.CheckSuites?.Nodes?
				.Where(suite => suite is not null)
				.Select(suite => suite!)
				.ToList() ?? [];
		}
	}
}
