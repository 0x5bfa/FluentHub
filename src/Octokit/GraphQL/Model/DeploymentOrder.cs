namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for deployment connections
    /// </summary>
    public class DeploymentOrder
    {
        /// <summary>
        /// The field to order deployments by.
        /// </summary>
        public DeploymentOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}