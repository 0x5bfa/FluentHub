namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'user_blocked' event on a given user.
    /// </summary>
    public class UserBlockedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Number of days that the user was blocked for.
        /// </summary>
        public UserBlockDuration BlockDuration { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The user who was blocked.
        /// </summary>
        public User Subject { get; set; }
    }
}