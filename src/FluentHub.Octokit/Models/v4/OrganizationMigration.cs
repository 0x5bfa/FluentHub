namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A GitHub Enterprise Importer (GEI) organization migration.
    /// </summary>
    public class OrganizationMigration
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// The reason the organization migration failed.
        /// </summary>
        public string FailureReason { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The remaining amount of repos to be migrated.
        /// </summary>
        public int? RemainingRepositoriesCount { get; set; }

        /// <summary>
        /// The name of the source organization to be migrated.
        /// </summary>
        public string SourceOrgName { get; set; }

        /// <summary>
        /// The URL of the source organization to migrate.
        /// </summary>
        public string SourceOrgUrl { get; set; }

        /// <summary>
        /// The migration state.
        /// </summary>
        public OrganizationMigrationState State { get; set; }

        /// <summary>
        /// The name of the target organization.
        /// </summary>
        public string TargetOrgName { get; set; }

        /// <summary>
        /// The total amount of repositories to be migrated.
        /// </summary>
        public int? TotalRepositoriesCount { get; set; }
    }
}