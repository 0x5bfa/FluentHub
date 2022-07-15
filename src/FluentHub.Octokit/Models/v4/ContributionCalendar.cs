namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A calendar of contributions made on GitHub by a user.
    /// </summary>
    public class ContributionCalendar
    {
        /// <summary>
        /// A list of hex color codes used in this calendar. The darker the color, the more contributions it represents.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Determine if the color set was chosen because it's currently Halloween.
        /// </summary>
        public bool IsHalloween { get; set; }

        /// <summary>
        /// A list of the months of contributions in this calendar.
        /// </summary>
        public List<ContributionCalendarMonth> Months { get; set; }

        /// <summary>
        /// The count of total contributions in the calendar.
        /// </summary>
        public int TotalContributions { get; set; }

        /// <summary>
        /// A list of the weeks of contributions in this calendar.
        /// </summary>
        public List<ContributionCalendarWeek> Weeks { get; set; }
    }
}