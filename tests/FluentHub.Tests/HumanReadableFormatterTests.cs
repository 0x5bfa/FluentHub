using System.Globalization;
using FluentHub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class HumanReadableFormatterTests
{
	private static readonly DateTimeOffset ReferenceTime = new(2026, 8, 21, 12, 0, 0, TimeSpan.Zero);

	[TestMethod]
	public void FormatRelativeTime_FormatsPastAndFutureValues()
	{
		Assert.AreEqual("2 hours ago", HumanReadableFormatter.FormatRelativeTime(ReferenceTime.AddHours(-2), ReferenceTime));
		Assert.AreEqual("3 days from now", HumanReadableFormatter.FormatRelativeTime(ReferenceTime.AddDays(3), ReferenceTime));
	}

	[TestMethod]
	public void FormatRelativeTime_FormatsNearValueAsNow()
	{
		Assert.AreEqual("now", HumanReadableFormatter.FormatRelativeTime(ReferenceTime.AddMilliseconds(-500), ReferenceTime));
	}

	[TestMethod]
	public void FormatDuration_UsesLargestMeaningfulUnit()
	{
		Assert.AreEqual("2 hours", HumanReadableFormatter.FormatDuration(TimeSpan.FromMinutes(150)));
	}

	[TestMethod]
	public void FormatMetric_UsesMetricPrefix()
	{
		Assert.AreEqual("999", HumanReadableFormatter.FormatMetric(999, CultureInfo.InvariantCulture));
		Assert.AreEqual("1.2k", HumanReadableFormatter.FormatMetric(1200, CultureInfo.InvariantCulture));
		Assert.AreEqual("1.5M", HumanReadableFormatter.FormatMetric(1_500_000, CultureInfo.InvariantCulture));
	}

	[TestMethod]
	public void FormatQuantity_PluralizesNoun()
	{
		Assert.AreEqual("1 branch", HumanReadableFormatter.FormatQuantity("branch", 1, CultureInfo.InvariantCulture));
		Assert.AreEqual("2 branches", HumanReadableFormatter.FormatQuantity("branch", 2, CultureInfo.InvariantCulture));
	}

	[TestMethod]
	public void FormatFileSize_UsesBinaryUnits()
	{
		Assert.AreEqual("1.5 KB", HumanReadableFormatter.FormatFileSize(1536, CultureInfo.InvariantCulture));
	}
}
