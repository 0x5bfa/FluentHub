namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a user that's made a reaction.
    /// </summary>
    public class ReactingUserEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        public User Node { get; set; }

        /// <summary>
        /// The moment when the user made the reaction.
        /// </summary>
        public DateTimeOffset ReactedAt { get; set; }
    }
}