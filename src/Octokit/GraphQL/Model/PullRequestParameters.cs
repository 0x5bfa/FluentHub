namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
    /// </summary>
    public class PullRequestParameters : QueryableValue<PullRequestParameters>
    {
        internal PullRequestParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// New, reviewable commits pushed will dismiss previous pull request review approvals.
        /// </summary>
        public bool DismissStaleReviewsOnPush { get; }

        /// <summary>
        /// Require an approving review in pull requests that modify files that have a designated code owner.
        /// </summary>
        public bool RequireCodeOwnerReview { get; }

        /// <summary>
        /// Whether the most recent reviewable push must be approved by someone other than the person who pushed it.
        /// </summary>
        public bool RequireLastPushApproval { get; }

        /// <summary>
        /// The number of approving reviews that are required before a pull request can be merged.
        /// </summary>
        public int RequiredApprovingReviewCount { get; }

        /// <summary>
        /// All conversations on code must be resolved before a pull request can be merged.
        /// </summary>
        public bool RequiredReviewThreadResolution { get; }

        internal static PullRequestParameters Create(Expression expression)
        {
            return new PullRequestParameters(expression);
        }
    }
}