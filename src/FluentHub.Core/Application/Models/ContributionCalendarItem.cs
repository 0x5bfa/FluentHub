using System.Globalization;

namespace FluentHub.Core.Application.Models
{
	public sealed class ContributionCalendarItem
	{
		public int ContributionCount { get; set; }

		public ContributionLevel ContributionLevel { get; set; }

		public string Date { get; set; } = string.Empty;

		public int Weekday { get; set; }

		public bool IsValid { get; set; }

		public string Description
		{
			get
			{
				if (!IsValid)
					return string.Empty;

				var contributionText = ContributionCount switch
				{
					0 => "No contributions",
					1 => "1 contribution",
					_ => $"{ContributionCount} contributions",
				};

				return DateOnly.TryParseExact(
					Date,
					"yyyy-MM-dd",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out var date)
					? $"{contributionText} on {date.ToString("D", CultureInfo.CurrentCulture)}"
					: contributionText;
			}
		}
	}
}
