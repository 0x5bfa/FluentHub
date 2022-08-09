namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A threaded list of comments for a given pull request.
    /// </summary>
    public class PullRequestThread
    {
        /// <summary>
        /// A list of pull request comments associated with the thread.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="skip">Skips the first _n_ elements in the list.</param>
        public PullRequestReviewCommentConnection Comments { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether or not the thread has been collapsed (resolved)
        /// </summary>
        public bool IsCollapsed { get; set; }

        /// <summary>
        /// Indicates whether this thread was outdated by newer changes.
        /// </summary>
        public bool IsOutdated { get; set; }

        /// <summary>
        /// Whether this thread has been resolved
        /// </summary>
        public bool IsResolved { get; set; }

        /// <summary>
        /// Identifies the pull request associated with this thread.
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// Identifies the repository associated with this thread.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// The user who resolved this thread
        /// </summary>
        public User ResolvedBy { get; set; }

        /// <summary>
        /// Indicates whether the current viewer can reply to this thread.
        /// </summary>
        public bool ViewerCanReply { get; set; }

        /// <summary>
        /// Whether or not the viewer can resolve this thread
        /// </summary>
        public bool ViewerCanResolve { get; set; }

        /// <summary>
        /// Whether or not the viewer can unresolve this thread
        /// </summary>
        public bool ViewerCanUnresolve { get; set; }
    }
}