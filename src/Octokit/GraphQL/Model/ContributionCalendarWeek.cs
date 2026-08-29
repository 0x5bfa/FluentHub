namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A week of contributions in a user's contribution graph.
    /// </summary>
    public class ContributionCalendarWeek : QueryableValue<ContributionCalendarWeek>
    {
        internal ContributionCalendarWeek(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The days of contributions in this week.
        /// </summary>
        public IQueryableList<ContributionCalendarDay> ContributionDays => this.CreateProperty(x => x.ContributionDays);

        /// <summary>
        /// The date of the earliest square in this week.
        /// </summary>
        public string FirstDay { get; }

        internal static ContributionCalendarWeek Create(Expression expression)
        {
            return new ContributionCalendarWeek(expression);
        }
    }
}