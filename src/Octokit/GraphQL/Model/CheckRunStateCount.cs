namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a count of the state of a check run.
    /// </summary>
    public class CheckRunStateCount : QueryableValue<CheckRunStateCount>
    {
        internal CheckRunStateCount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of check runs with this state.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// The state of a check run.
        /// </summary>
        public CheckRunState State { get; }

        internal static CheckRunStateCount Create(Expression expression)
        {
            return new CheckRunStateCount(expression);
        }
    }
}