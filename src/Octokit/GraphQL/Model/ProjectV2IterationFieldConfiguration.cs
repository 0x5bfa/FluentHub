namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Iteration field configuration for a project.
    /// </summary>
    public class ProjectV2IterationFieldConfiguration : QueryableValue<ProjectV2IterationFieldConfiguration>
    {
        internal ProjectV2IterationFieldConfiguration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The iteration's completed iterations
        /// </summary>
        public IQueryableList<ProjectV2IterationFieldIteration> CompletedIterations => this.CreateProperty(x => x.CompletedIterations);

        /// <summary>
        /// The iteration's duration in days
        /// </summary>
        public int Duration { get; }

        /// <summary>
        /// The iteration's iterations
        /// </summary>
        public IQueryableList<ProjectV2IterationFieldIteration> Iterations => this.CreateProperty(x => x.Iterations);

        /// <summary>
        /// The iteration's start day of the week
        /// </summary>
        public int StartDay { get; }

        internal static ProjectV2IterationFieldConfiguration Create(Expression expression)
        {
            return new ProjectV2IterationFieldConfiguration(expression);
        }
    }
}