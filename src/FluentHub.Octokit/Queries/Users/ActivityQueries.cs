namespace FluentHub.Octokit.Queries.Users
{
	public class ActivityQueries
	{
		public async Task<List<Activity>> GetAllAsync(string login)
		{
			OctokitV3.ApiOptions options = new()
			{
				PageCount = 1,
				PageSize = 60,
				StartPage = 1
			};

			var response = await App.Client.Activity.Events.GetAllUserReceived(login, options);

			Wrappers.ActivityWrapper wrapper = new();
			var activities = wrapper.Wrap(response);

			return activities;
		}

		public async Task<ContributionCalendar> GetContributionCalendarAsync(string login)
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

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
