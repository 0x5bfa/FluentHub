namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'base_ref_changed' event on a given issue or pull request.
    /// </summary>
    public class BaseRefChangedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the name of the base ref for the pull request after it was changed.
        /// </summary>
        public string CurrentRefName { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the name of the base ref for the pull request before it was changed.
        /// </summary>
        public string PreviousRefName { get; set; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest { get; set; }
    }
}