using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class ActivityQueries
	{
		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public ActivityQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public async Task<List<Activity>> GetAllAsync(string login, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Activity.GetReceivedEventsAsync(
					login,
					pageSize: 60,
					page: 1,
					cancellationToken: token),
				cancellationToken);

			Wrappers.ActivityWrapper wrapper = new();
			var activities = wrapper.Wrap(response);

			return activities;
		}

		public Task<ContributionCalendar> GetContributionCalendarAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			if (_cache is null)
				return GetContributionCalendarUncachedAsync(login, cancellationToken);

			var key = CacheKey.ForAccount(
				_gitHub.CachePartition,
				"contribution-calendars-v2",
				login.Trim().ToLowerInvariant());
			return _cache.GetOrCreateAsync(
				key,
				CachePolicies.User,
				GitHubCacheSerializers.ContributionCalendar,
				token => GetContributionCalendarUncachedAsync(login, token),
				cancellationToken);
		}

		private async Task<ContributionCalendar> GetContributionCalendarUncachedAsync(
			string login,
			CancellationToken cancellationToken)
		{
			var query = new Query()
				.User(login)
				.ContributionsCollection(null, null, null)
				.ContributionCalendar
				.Select(x => new ContributionCalendar
				{
					Colors = x.Colors.ToList(),
					TotalContributions = x.TotalContributions,

					Months = x.Months.Select(month => new ContributionCalendarMonth
					{
						FirstDay = month.FirstDay,
						Name = month.Name,
						TotalWeeks = month.TotalWeeks,
						Year = month.Year,
					})
					.ToList(),

					Weeks = x.Weeks.Select(week => new ContributionCalendarWeek
					{
						FirstDay = week.FirstDay,
						ContributionDays = week.ContributionDays.Select(day => new ContributionCalendarDay
						{
							Color = day.Color,
							ContributionCount = day.ContributionCount,
							ContributionLevel = (ContributionLevel)day.ContributionLevel,
							Date = day.Date,
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
