namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Set of conditions that determine if a ruleset will evaluate
    /// </summary>
    public class RepositoryRuleConditions : QueryableValue<RepositoryRuleConditions>
    {
        internal RepositoryRuleConditions(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Configuration for the ref_name condition
        /// </summary>
        public RefNameConditionTarget RefName => this.CreateProperty(x => x.RefName, Octokit.GraphQL.Model.RefNameConditionTarget.Create);

        /// <summary>
        /// Configuration for the repository_id condition
        /// </summary>
        public RepositoryIdConditionTarget RepositoryId => this.CreateProperty(x => x.RepositoryId, Octokit.GraphQL.Model.RepositoryIdConditionTarget.Create);

        /// <summary>
        /// Configuration for the repository_name condition
        /// </summary>
        public RepositoryNameConditionTarget RepositoryName => this.CreateProperty(x => x.RepositoryName, Octokit.GraphQL.Model.RepositoryNameConditionTarget.Create);

        /// <summary>
        /// Configuration for the repository_property condition
        /// </summary>
        public RepositoryPropertyConditionTarget RepositoryProperty => this.CreateProperty(x => x.RepositoryProperty, Octokit.GraphQL.Model.RepositoryPropertyConditionTarget.Create);

        internal static RepositoryRuleConditions Create(Expression expression)
        {
            return new RepositoryRuleConditions(expression);
        }
    }
}