namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information about the availability of features and limits for a repository based on its billing plan.
    /// </summary>
    public class RepositoryPlanFeatures : QueryableValue<RepositoryPlanFeatures>
    {
        internal RepositoryPlanFeatures(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Whether reviews can be automatically requested and enforced with a CODEOWNERS file
        /// </summary>
        public bool Codeowners { get; }

        /// <summary>
        /// Whether pull requests can be created as or converted to draft
        /// </summary>
        public bool DraftPullRequests { get; }

        /// <summary>
        /// Maximum number of users that can be assigned to an issue or pull request
        /// </summary>
        public int MaximumAssignees { get; }

        /// <summary>
        /// Maximum number of manually-requested reviews on a pull request
        /// </summary>
        public int MaximumManualReviewRequests { get; }

        /// <summary>
        /// Whether teams can be requested to review pull requests
        /// </summary>
        public bool TeamReviewRequests { get; }

        internal static RepositoryPlanFeatures Create(Expression expression)
        {
            return new RepositoryPlanFeatures(expression);
        }
    }
}