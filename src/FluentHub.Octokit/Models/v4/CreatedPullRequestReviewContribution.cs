namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the contribution a user made by leaving a review on a pull request.
    /// </summary>
    public class CreatedPullRequestReviewContribution
    {
        /// <summary>
        /// Whether this contribution is associated with a record you do not have access to. For
        /// example, your own 'first issue' contribution may have been made on a repository you can no
        /// longer access.
        /// </summary>
        public bool IsRestricted { get; set; }

        /// <summary>
        /// When this contribution was made.
        /// </summary>
        public DateTimeOffset OccurredAt { get; set; }

        /// <summary>
        /// Humanized string of "When this contribution was made."
        /// <summary>
        public string OccurredAtHumanized { get; set; }

        /// <summary>
        /// The pull request the user reviewed.
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// The review the user left on the pull request.
        /// </summary>
        public PullRequestReview PullRequestReview { get; set; }

        /// <summary>
        /// The repository containing the pull request that the user reviewed.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// The HTTP path for this contribution.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this contribution.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The user who made this contribution.
        /// </summary>
        public User User { get; set; }
    }
}