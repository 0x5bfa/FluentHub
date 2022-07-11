namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an Octoshift migration.
    /// </summary>
    public interface IMigration
    {
        /// <summary>
        /// The Octoshift migration flag to continue on error.
        /// </summary>
        bool ContinueOnError { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        string FailureReason { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// The URL for the migration log (expires 1 day after migration completes).
        /// </summary>
        string MigrationLogUrl { get; set; }

        /// <summary>
        /// The Octoshift migration source.
        /// </summary>
        MigrationSource MigrationSource { get; set; }

        /// <summary>
        /// The target repository name.
        /// </summary>
        string RepositoryName { get; set; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        string SourceUrl { get; set; }

        /// <summary>
        /// The Octoshift migration state.
        /// </summary>
        MigrationState State { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIMigration
    {
        

        public bool ContinueOnError { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string FailureReason { get; set; }

        public ID Id { get; set; }

        public string MigrationLogUrl { get; set; }

        public MigrationSource MigrationSource { get; set; }

        public string RepositoryName { get; set; }

        public string SourceUrl { get; set; }

        public MigrationState State { get; set; }
    }
}