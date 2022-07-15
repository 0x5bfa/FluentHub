namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A month of contributions in a user's contribution graph.
    /// </summary>
    public class ContributionCalendarMonth
    {
        /// <summary>
        /// The date of the first day of this month.
        /// </summary>
        public string FirstDay { get; set; }

        /// <summary>
        /// The name of the month.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How many weeks started in this month.
        /// </summary>
        public int TotalWeeks { get; set; }

        /// <summary>
        /// The year the month occurred in.
        /// </summary>
        public int Year { get; set; }
    }
}