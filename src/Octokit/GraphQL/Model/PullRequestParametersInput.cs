namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
    /// </summary>
    public class PullRequestParametersInput
    {
        /// <summary>
        /// New, reviewable commits pushed will dismiss previous pull request review approvals.
        /// </summary>
        public bool DismissStaleReviewsOnPush { get; set; }

        /// <summary>
        /// Require an approving review in pull requests that modify files that have a designated code owner.
        /// </summary>
        public bool RequireCodeOwnerReview { get; set; }

        /// <summary>
        /// Whether the most recent reviewable push must be approved by someone other than the person who pushed it.
        /// </summary>
        public bool RequireLastPushApproval { get; set; }

        /// <summary>
        /// The number of approving reviews that are required before a pull request can be merged.
        /// </summary>
        public int RequiredApprovingReviewCount { get; set; }

        /// <summary>
        /// All conversations on code must be resolved before a pull request can be merged.
        /// </summary>
        public bool RequiredReviewThreadResolution { get; set; }
    }
}