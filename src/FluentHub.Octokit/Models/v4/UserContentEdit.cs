namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An edit on user content
    /// </summary>
    public class UserContentEdit
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
        /// Identifies the date and time when the object was deleted.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was deleted."
        /// <summary>
        public string DeletedAtHumanized { get; set; }

        /// <summary>
        /// The actor who deleted this content
        /// </summary>
        public IActor DeletedBy { get; set; }

        /// <summary>
        /// A summary of the changes for this edit
        /// </summary>
        public string Diff { get; set; }

        /// <summary>
        /// When this content was edited
        /// </summary>
        public DateTimeOffset EditedAt { get; set; }

        /// <summary>
        /// Humanized string of "When this content was edited"
        /// <summary>
        public string EditedAtHumanized { get; set; }

        /// <summary>
        /// The actor who edited this content
        /// </summary>
        public IActor Editor { get; set; }

        public ID Id { get; set; }

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