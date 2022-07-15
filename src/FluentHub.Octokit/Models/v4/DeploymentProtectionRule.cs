namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A protection rule.
    /// </summary>
    public class DeploymentProtectionRule
    {
        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The teams or users that can review the deployment
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentReviewerConnection Reviewers { get; set; }

        /// <summary>
        /// The timeout in minutes for this protection rule.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// The type of protection rule.
        /// </summary>
        public DeploymentProtectionRuleType Type { get; set; }
    }
}