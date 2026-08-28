using FluentHub.Core.Application.Models;

namespace FluentHub.Core.Application
{
	public static class ContributionCalendarService
	{
		public static IReadOnlyList<ContributionCalendarItem> CreateItems(ContributionCalendar calendar)
		{
			ArgumentNullException.ThrowIfNull(calendar);

			var items = calendar.Weeks
				.SelectMany(week => week.ContributionDays)
				.Select(day => new ContributionCalendarItem
				{
					ContributionCount = day.ContributionCount,
					ContributionLevel = day.ContributionLevel,
					Date = day.Date,
					Weekday = day.Weekday,
					IsValid = true,
				})
				.ToList();

			if (items.FirstOrDefault() is not { } firstDay)
				return items;

			if (firstDay.Weekday is < 0 or > 6)
				throw new ArgumentOutOfRangeException(nameof(calendar), "The first contribution weekday must be between 0 and 6.");

			for (var weekday = firstDay.Weekday - 1; weekday >= 0; weekday--)
			{
				items.Insert(0, new ContributionCalendarItem
				{
					ContributionLevel = ContributionLevel.None,
					Weekday = weekday,
					IsValid = false,
				});
			}

			return items;
		}
	}
}
