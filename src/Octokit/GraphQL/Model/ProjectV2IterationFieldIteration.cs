namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Iteration field iteration settings for a project.
    /// </summary>
    public class ProjectV2IterationFieldIteration : QueryableValue<ProjectV2IterationFieldIteration>
    {
        internal ProjectV2IterationFieldIteration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The iteration's duration in days
        /// </summary>
        public int Duration { get; }

        /// <summary>
        /// The iteration's ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The iteration's start date
        /// </summary>
        public string StartDate { get; }

        /// <summary>
        /// The iteration's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The iteration's html title.
        /// </summary>
        public string TitleHTML { get; }

        internal static ProjectV2IterationFieldIteration Create(Expression expression)
        {
            return new ProjectV2IterationFieldIteration(expression);
        }
    }
}