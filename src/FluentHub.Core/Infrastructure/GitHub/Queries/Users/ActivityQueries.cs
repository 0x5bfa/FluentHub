using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class ActivityQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public ActivityQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Activity>> GetAllAsync(string login, CancellationToken cancellationToken = default)
		{
			OctokitV3.ApiOptions options = new()
			{
				PageCount = 1,
				PageSize = 60,
				StartPage = 1
			};

			var response = await _gitHub.RunRestAsync(
				client => client.Activity.Events.GetAllUserReceived(login, options),
				cancellationToken);

			Wrappers.ActivityWrapper wrapper = new();
			var activities = wrapper.Wrap(response);

			return activities;
		}

		public async Task<ContributionCalendar> GetContributionCalendarAsync(string login, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.User(login)
				.ContributionsCollection(null, null, null)
				.ContributionCalendar
				.Select(x => new ContributionCalendar
				{
					TotalContributions = x.TotalContributions,

					Weeks = x.Weeks.Select(week => new ContributionCalendarWeek
					{
						ContributionDays = week.ContributionDays.Select(day => new ContributionCalendarDay
						{
							Color = day.Color,
							ContributionCount = day.ContributionCount,
							ContributionLevel = (ContributionLevel)day.ContributionLevel,
							Weekday = day.Weekday,
						})
						.ToList(),
					})
					.ToList(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
