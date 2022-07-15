namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A request for a user to review a pull request.
    /// </summary>
    public class ReviewRequest
    {
        /// <summary>
        /// Whether this request was created for a code owner
        /// </summary>
        public bool AsCodeOwner { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the pull request associated with this review request.
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// The reviewer that is requested.
        /// </summary>
        public RequestedReviewer RequestedReviewer { get; set; }
    }
}