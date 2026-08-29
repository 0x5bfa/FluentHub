namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Parameters to be used for the repository_name condition
    /// </summary>
    public class RepositoryNameConditionTarget : QueryableValue<RepositoryNameConditionTarget>
    {
        internal RepositoryNameConditionTarget(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Array of repository names or patterns to exclude. The condition will not pass if any of these patterns match.
        /// </summary>
        public IEnumerable<string> Exclude { get; }

        /// <summary>
        /// Array of repository names or patterns to include. One of these patterns must match for the condition to pass. Also accepts `~ALL` to include all repositories.
        /// </summary>
        public IEnumerable<string> Include { get; }

        /// <summary>
        /// Target changes that match these patterns will be prevented except by those with bypass permissions.
        /// </summary>
        public bool Protected { get; }

        internal static RepositoryNameConditionTarget Create(Expression expression)
        {
            return new RepositoryNameConditionTarget(expression);
        }
    }
}