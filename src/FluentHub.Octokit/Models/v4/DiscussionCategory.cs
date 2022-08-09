namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A category for discussions in a repository.
    /// </summary>
    public class DiscussionCategory
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// A description of this category.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An emoji representing this category.
        /// </summary>
        public string Emoji { get; set; }

        /// <summary>
        /// This category's emoji rendered as HTML.
        /// </summary>
        public string EmojiHTML { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether or not discussions in this category support choosing an answer with the markDiscussionCommentAsAnswer mutation.
        /// </summary>
        public bool IsAnswerable { get; set; }

        /// <summary>
        /// The name of this category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }
    }
}