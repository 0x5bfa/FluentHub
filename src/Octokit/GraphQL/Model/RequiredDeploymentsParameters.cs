namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Choose which environments must be successfully deployed to before refs can be pushed into a ref that matches this rule.
    /// </summary>
    public class RequiredDeploymentsParameters : QueryableValue<RequiredDeploymentsParameters>
    {
        internal RequiredDeploymentsParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The environments that must be successfully deployed to before branches can be merged.
        /// </summary>
        public IEnumerable<string> RequiredDeploymentEnvironments { get; }

        internal static RequiredDeploymentsParameters Create(Expression expression)
        {
            return new RequiredDeploymentsParameters(expression);
        }
    }
}