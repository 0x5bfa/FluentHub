namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Parameters to be used for the required_deployments rule
    /// </summary>
    public class RequiredDeploymentsParameters
    {
        /// <summary>
        /// The environments that must be successfully deployed to before branches can be merged.
        /// </summary>
        public List<string> RequiredDeploymentEnvironments { get; set; }
    }
}