namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an 'unassigned' event on any assignable object.
    /// </summary>
    public class UnassignedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the assignable associated with the event.
        /// </summary>
        public IAssignable Assignable { get; set; }

        /// <summary>
        /// Identifies the user or mannequin that was unassigned.
        /// </summary>
        public Assignee Assignee { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the subject (user) who was unassigned.
        /// </summary>
        [Obsolete(@"Assignees can now be mannequins. Use the `assignee` field instead. Removal on 2020-01-01 UTC.")]
        public User User { get; set; }
    }
}