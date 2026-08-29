namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A protection rule.
    /// </summary>
    public class DeploymentProtectionRule : QueryableValue<DeploymentProtectionRule>
    {
        internal DeploymentProtectionRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// Whether deployments to this environment can be approved by the user who created the deployment.
        /// </summary>
        public bool? PreventSelfReview { get; }

        /// <summary>
        /// The teams or users that can review the deployment
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentReviewerConnection Reviewers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Reviewers(first, after, last, before), Octokit.GraphQL.Model.DeploymentReviewerConnection.Create);

        /// <summary>
        /// The timeout in minutes for this protection rule.
        /// </summary>
        public int Timeout { get; }

        /// <summary>
        /// The type of protection rule.
        /// </summary>
        public DeploymentProtectionRuleType Type { get; }

        internal static DeploymentProtectionRule Create(Expression expression)
        {
            return new DeploymentProtectionRule(expression);
        }
    }
}