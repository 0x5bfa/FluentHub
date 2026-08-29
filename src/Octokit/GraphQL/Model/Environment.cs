namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An environment.
    /// </summary>
    public class Environment : QueryableValue<Environment>
    {
        internal Environment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Environment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of the environment
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The protection rules defined for this environment
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentProtectionRuleConnection ProtectionRules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ProtectionRules(first, after, last, before), Octokit.GraphQL.Model.DeploymentProtectionRuleConnection.Create);

        internal static Environment Create(Expression expression)
        {
            return new Environment(expression);
        }
    }
}