// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class MergedCalendarDays
	{
		public string Color { get; set; }

		public int ContributionCount { get; set; }

		public ContributionLevel ContributionLevel { get; set; }

		public int Weekday { get; set; }

		public bool IsVaild { get; set; }
	}
}
