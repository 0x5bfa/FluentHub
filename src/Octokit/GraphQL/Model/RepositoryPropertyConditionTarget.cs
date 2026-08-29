namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Parameters to be used for the repository_property condition
    /// </summary>
    public class RepositoryPropertyConditionTarget : QueryableValue<RepositoryPropertyConditionTarget>
    {
        internal RepositoryPropertyConditionTarget(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Array of repository properties that must not match.
        /// </summary>
        public IQueryableList<PropertyTargetDefinition> Exclude => this.CreateProperty(x => x.Exclude);

        /// <summary>
        /// Array of repository properties that must match
        /// </summary>
        public IQueryableList<PropertyTargetDefinition> Include => this.CreateProperty(x => x.Include);

        internal static RepositoryPropertyConditionTarget Create(Expression expression)
        {
            return new RepositoryPropertyConditionTarget(expression);
        }
    }
}