namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'connected' event on a given issue or pull request.
    /// </summary>
    public class ConnectedEvent
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
        /// Reference originated in a different repository.
        /// </summary>
        public bool IsCrossRepository { get; set; }

        /// <summary>
        /// Issue or pull request that made the reference.
        /// </summary>
        public ReferencedSubject Source { get; set; }

        /// <summary>
        /// Issue or pull request which was connected.
        /// </summary>
        public ReferencedSubject Subject { get; set; }
    }
}