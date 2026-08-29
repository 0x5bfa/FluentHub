namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies a review comment to be left with a Pull Request Review.
    /// </summary>
    public class DraftPullRequestReviewComment
    {
        /// <summary>
        /// Path to the file being commented on.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Position in the file to leave a comment on.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Body of the comment to leave.
        /// </summary>
        public string Body { get; set; }
    }
}