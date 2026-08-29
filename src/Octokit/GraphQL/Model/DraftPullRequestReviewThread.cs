namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies a review comment thread to be left with a Pull Request Review.
    /// </summary>
    public class DraftPullRequestReviewThread
    {
        /// <summary>
        /// Path to the file being commented on.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The line of the blob to which the thread refers. The end of the line range for multi-line comments.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// The side of the diff on which the line resides. For multi-line comments, this is the side for the end of the line range.
        /// </summary>
        public DiffSide? Side { get; set; }

        /// <summary>
        /// The first line of the range to which the comment refers.
        /// </summary>
        public int? StartLine { get; set; }

        /// <summary>
        /// The side of the diff on which the start line resides.
        /// </summary>
        public DiffSide? StartSide { get; set; }

        /// <summary>
        /// Body of the comment to leave.
        /// </summary>
        public string Body { get; set; }
    }
}