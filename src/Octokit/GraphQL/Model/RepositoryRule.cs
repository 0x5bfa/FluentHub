namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository rule.
    /// </summary>
    public class RepositoryRule : QueryableValue<RepositoryRule>
    {
        internal RepositoryRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the RepositoryRule object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The parameters for this rule.
        /// </summary>
        public RuleParameters Parameters => this.CreateProperty(x => x.Parameters, Octokit.GraphQL.Model.RuleParameters.Create);

        /// <summary>
        /// The repository ruleset associated with this rule configuration
        /// </summary>
        public RepositoryRuleset RepositoryRuleset => this.CreateProperty(x => x.RepositoryRuleset, Octokit.GraphQL.Model.RepositoryRuleset.Create);

        /// <summary>
        /// The type of rule.
        /// </summary>
        public RepositoryRuleType Type { get; }

        internal static RepositoryRule Create(Expression expression)
        {
            return new RepositoryRule(expression);
        }
    }
}