namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a count of the state of a status context.
    /// </summary>
    public class StatusContextStateCount : QueryableValue<StatusContextStateCount>
    {
        internal StatusContextStateCount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of statuses with this state.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// The state of a status context.
        /// </summary>
        public StatusState State { get; }

        internal static StatusContextStateCount Create(Expression expression)
        {
            return new StatusContextStateCount(expression);
        }
    }
}