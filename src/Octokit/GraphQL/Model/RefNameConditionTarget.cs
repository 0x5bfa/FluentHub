namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Parameters to be used for the ref_name condition
    /// </summary>
    public class RefNameConditionTarget : QueryableValue<RefNameConditionTarget>
    {
        internal RefNameConditionTarget(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Array of ref names or patterns to exclude. The condition will not pass if any of these patterns match.
        /// </summary>
        public IEnumerable<string> Exclude { get; }

        /// <summary>
        /// Array of ref names or patterns to include. One of these patterns must match for the condition to pass. Also accepts `~DEFAULT_BRANCH` to include the default branch or `~ALL` to include all branches.
        /// </summary>
        public IEnumerable<string> Include { get; }

        internal static RefNameConditionTarget Create(Expression expression)
        {
            return new RefNameConditionTarget(expression);
        }
    }
}