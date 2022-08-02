namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a starred repository.
    /// </summary>
    public class StarredRepositoryEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        public Repository Node { get; set; }

        /// <summary>
        /// Identifies when the item was starred.
        /// </summary>
        public DateTimeOffset StarredAt { get; set; }
    }
}