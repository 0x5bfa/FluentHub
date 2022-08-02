namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'head_ref_deleted' event on a given pull request.
    /// </summary>
    public class HeadRefDeletedEvent
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
        /// Identifies the Ref associated with the `head_ref_deleted` event.
        /// </summary>
        public Ref HeadRef { get; set; }

        /// <summary>
        /// Identifies the name of the Ref associated with the `head_ref_deleted` event.
        /// </summary>
        public string HeadRefName { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest { get; set; }
    }
}