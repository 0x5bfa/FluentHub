namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'locked' event on a given issue or pull request.
    /// </summary>
    public class LockedEvent
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
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Reason that the conversation was locked (optional).
        /// </summary>
        public LockReason? LockReason { get; set; }

        /// <summary>
        /// Object that was locked.
        /// </summary>
        public ILockable Lockable { get; set; }
    }
}