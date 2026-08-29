namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Project progress stats.
    /// </summary>
    public class ProjectProgress : QueryableValue<ProjectProgress>
    {
        internal ProjectProgress(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of done cards.
        /// </summary>
        public int DoneCount { get; }

        /// <summary>
        /// The percentage of done cards.
        /// </summary>
        public double DonePercentage { get; }

        /// <summary>
        /// Whether progress tracking is enabled and cards with purpose exist for this project
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// The number of in-progress cards.
        /// </summary>
        public int InProgressCount { get; }

        /// <summary>
        /// The percentage of in-progress cards.
        /// </summary>
        public double InProgressPercentage { get; }

        /// <summary>
        /// The number of to do cards.
        /// </summary>
        public int TodoCount { get; }

        /// <summary>
        /// The percentage of to do cards.
        /// </summary>
        public double TodoPercentage { get; }

        internal static ProjectProgress Create(Expression expression)
        {
            return new ProjectProgress(expression);
        }
    }
}