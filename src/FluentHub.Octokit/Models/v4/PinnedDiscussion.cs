namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Pinned Discussion is a discussion pinned to a repository's index page.
    /// </summary>
    public class PinnedDiscussion
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The discussion that was pinned.
        /// </summary>
        public Discussion Discussion { get; set; }

        /// <summary>
        /// Color stops of the chosen gradient
        /// </summary>
        public List<string> GradientStopColors { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Background texture pattern
        /// </summary>
        public PinnedDiscussionPattern Pattern { get; set; }

        /// <summary>
        /// The actor that pinned this discussion.
        /// </summary>
        public IActor PinnedBy { get; set; }

        /// <summary>
        /// Preconfigured background gradient option
        /// </summary>
        public PinnedDiscussionGradient? PreconfiguredGradient { get; set; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}