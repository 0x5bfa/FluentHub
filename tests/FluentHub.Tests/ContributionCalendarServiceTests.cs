using FluentHub.Core.Application;
using FluentHub.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class ContributionCalendarServiceTests
{
	[TestMethod]
	public void CreateItemsFlattensWeeksAndPadsTheFirstWeek()
	{
		var calendar = new ContributionCalendar
		{
			Weeks =
			[
				new ContributionCalendarWeek
				{
					ContributionDays =
					[
						new ContributionCalendarDay
						{
							Color = "#123456",
							ContributionCount = 4,
							ContributionLevel = ContributionLevel.SecondQuartile,
							Weekday = 2,
						},
					],
				},
			],
		};

		var items = ContributionCalendarService.CreateItems(calendar);

		Assert.HasCount(3, items);
		Assert.AreEqual(0, items[0].Weekday);
		Assert.AreEqual(1, items[1].Weekday);
		Assert.IsFalse(items[0].IsValid);
		Assert.IsFalse(items[1].IsValid);
		Assert.IsTrue(items[2].IsValid);
		Assert.AreEqual(4, items[2].ContributionCount);
	}
}
