namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Pinned Issue is a issue pinned to a repository's index page.
    /// </summary>
    public class PinnedIssue
    {
        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The issue that was pinned.
        /// </summary>
        public Issue Issue { get; set; }

        /// <summary>
        /// The actor that pinned this issue.
        /// </summary>
        public IActor PinnedBy { get; set; }

        /// <summary>
        /// The repository that this issue was pinned to.
        /// </summary>
        public Repository Repository { get; set; }
    }
}