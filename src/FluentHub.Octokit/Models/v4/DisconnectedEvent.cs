namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'disconnected' event on a given issue or pull request.
    /// </summary>
    public class DisconnectedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Reference originated in a different repository.
        /// </summary>
        public bool IsCrossRepository { get; set; }

        /// <summary>
        /// Issue or pull request from which the issue was disconnected.
        /// </summary>
        public ReferencedSubject Source { get; set; }

        /// <summary>
        /// Issue or pull request which was disconnected.
        /// </summary>
        public ReferencedSubject Subject { get; set; }
    }
}