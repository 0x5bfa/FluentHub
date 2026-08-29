namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A calendar of contributions made on GitHub by a user.
    /// </summary>
    public class ContributionCalendar : QueryableValue<ContributionCalendar>
    {
        internal ContributionCalendar(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of hex color codes used in this calendar. The darker the color, the more contributions it represents.
        /// </summary>
        public IEnumerable<string> Colors { get; }

        /// <summary>
        /// Determine if the color set was chosen because it's currently Halloween.
        /// </summary>
        public bool IsHalloween { get; }

        /// <summary>
        /// A list of the months of contributions in this calendar.
        /// </summary>
        public IQueryableList<ContributionCalendarMonth> Months => this.CreateProperty(x => x.Months);

        /// <summary>
        /// The count of total contributions in the calendar.
        /// </summary>
        public int TotalContributions { get; }

        /// <summary>
        /// A list of the weeks of contributions in this calendar.
        /// </summary>
        public IQueryableList<ContributionCalendarWeek> Weeks => this.CreateProperty(x => x.Weeks);

        internal static ContributionCalendar Create(Expression expression)
        {
            return new ContributionCalendar(expression);
        }
    }
}