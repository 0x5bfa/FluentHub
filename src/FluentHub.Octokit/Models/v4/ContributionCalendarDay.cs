// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a single day of contributions on GitHub by a user.
	/// </summary>
	public class ContributionCalendarDay
	{
		/// <summary>
		/// The hex color code that represents how many contributions were made on this day compared to others in the calendar.
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// How many contributions were made by the user on this day.
		/// </summary>
		public int ContributionCount { get; set; }

		/// <summary>
		/// Indication of contributions, relative to other days. Can be used to indicate which color to represent this day on a calendar.
		/// </summary>
		public ContributionLevel ContributionLevel { get; set; }

		/// <summary>
		/// The day this square represents.
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// A number representing which day of the week this square represents, e.g., 1 is Monday.
		/// </summary>
		public int Weekday { get; set; }
	}
}
