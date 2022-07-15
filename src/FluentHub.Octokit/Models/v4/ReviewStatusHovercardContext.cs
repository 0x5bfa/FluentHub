namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A hovercard context with a message describing the current code review state of the pull
    /// request.
    /// </summary>
    public class ReviewStatusHovercardContext
    {
        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; set; }

        /// <summary>
        /// The current status of the pull request with respect to code review.
        /// </summary>
        public PullRequestReviewDecision? ReviewDecision { get; set; }
    }
}