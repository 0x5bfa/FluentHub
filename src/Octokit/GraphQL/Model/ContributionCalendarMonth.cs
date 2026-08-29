namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A month of contributions in a user's contribution graph.
    /// </summary>
    public class ContributionCalendarMonth : QueryableValue<ContributionCalendarMonth>
    {
        internal ContributionCalendarMonth(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The date of the first day of this month.
        /// </summary>
        public string FirstDay { get; }

        /// <summary>
        /// The name of the month.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// How many weeks started in this month.
        /// </summary>
        public int TotalWeeks { get; }

        /// <summary>
        /// The year the month occurred in.
        /// </summary>
        public int Year { get; }

        internal static ContributionCalendarMonth Create(Expression expression)
        {
            return new ContributionCalendarMonth(expression);
        }
    }
}