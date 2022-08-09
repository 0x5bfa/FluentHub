namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'head_ref_force_pushed' event on a given pull request.
    /// </summary>
    public class HeadRefForcePushedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the after commit SHA for the 'head_ref_force_pushed' event.
        /// </summary>
        public Commit AfterCommit { get; set; }

        /// <summary>
        /// Identifies the before commit SHA for the 'head_ref_force_pushed' event.
        /// </summary>
        public Commit BeforeCommit { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// Identifies the fully qualified ref name for the 'head_ref_force_pushed' event.
        /// </summary>
        public Ref Ref { get; set; }
    }
}