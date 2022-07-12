namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An emoji reaction to a particular piece of content.
    /// </summary>
    public class Reaction
    {
        /// <summary>
        /// Identifies the emoji reaction.
        /// </summary>
        public ReactionContent Content { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The reactable piece of content
        /// </summary>
        public IReactable Reactable { get; set; }

        /// <summary>
        /// Identifies the user who created this reaction.
        /// </summary>
        public User User { get; set; }
    }
}