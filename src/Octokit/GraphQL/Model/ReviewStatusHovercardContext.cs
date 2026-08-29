namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A hovercard context with a message describing the current code review state of the pull
    /// request.
    /// </summary>
    public class ReviewStatusHovercardContext : QueryableValue<ReviewStatusHovercardContext>
    {
        internal ReviewStatusHovercardContext(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; }

        /// <summary>
        /// The current status of the pull request with respect to code review.
        /// </summary>
        public PullRequestReviewDecision? ReviewDecision { get; }

        internal static ReviewStatusHovercardContext Create(Expression expression)
        {
            return new ReviewStatusHovercardContext(expression);
        }
    }
}