namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An Octoshift repository migration.
    /// </summary>
    public class RepositoryMigration
    {
        /// <summary>
        /// The Octoshift migration flag to continue on error.
        /// </summary>
        public bool ContinueOnError { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        public string FailureReason { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The URL for the migration log (expires 1 day after migration completes).
        /// </summary>
        public string MigrationLogUrl { get; set; }

        /// <summary>
        /// The Octoshift migration source.
        /// </summary>
        public MigrationSource MigrationSource { get; set; }

        /// <summary>
        /// The target repository name.
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// The Octoshift migration state.
        /// </summary>
        public MigrationState State { get; set; }
    }
}