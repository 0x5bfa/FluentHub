namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Parameters to be used for the repository_id condition
    /// </summary>
    public class RepositoryIdConditionTarget : QueryableValue<RepositoryIdConditionTarget>
    {
        internal RepositoryIdConditionTarget(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// One of these repo IDs must match the repo.
        /// </summary>
        public IEnumerable<ID> RepositoryIds { get; }

        internal static RepositoryIdConditionTarget Create(Expression expression)
        {
            return new RepositoryIdConditionTarget(expression);
        }
    }
}