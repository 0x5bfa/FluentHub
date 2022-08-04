namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'closed' event on any `Closable`.
    /// </summary>
    public class ClosedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Object that was closed.
        /// </summary>
        public IClosable Closable { get; set; }

        /// <summary>
        /// Object which triggered the creation of this event.
        /// </summary>
        public Closer Closer { get; set; }

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
        /// The HTTP path for this closed event.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The reason the issue state was changed to closed.
        /// </summary>
        public IssueStateReason? StateReason { get; set; }

        /// <summary>
        /// The HTTP URL for this closed event.
        /// </summary>
        public string Url { get; set; }
    }
}