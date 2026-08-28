using FluentHub.Core.Application.Models;

namespace FluentHub.Core.Application.Models
{
	public sealed class ContributionCalendarItem
	{
		public string Color { get; set; } = string.Empty;

		public int ContributionCount { get; set; }

		public ContributionLevel ContributionLevel { get; set; }

		public int Weekday { get; set; }

		public bool IsValid { get; set; }
	}
}
