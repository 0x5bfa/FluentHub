namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A week of contributions in a user's contribution graph.
    /// </summary>
    public class ContributionCalendarWeek
    {
        /// <summary>
        /// The days of contributions in this week.
        /// </summary>
        public List<ContributionCalendarDay> ContributionDays { get; set; }

        /// <summary>
        /// The date of the earliest square in this week.
        /// </summary>
        public string FirstDay { get; set; }
    }
}