namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Enterprise Importer (GEI) organization migration.
    /// </summary>
    public class OrganizationMigration : QueryableValue<OrganizationMigration>
    {
        internal OrganizationMigration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public string DatabaseId { get; }

        /// <summary>
        /// The reason the organization migration failed.
        /// </summary>
        public string FailureReason { get; }

        /// <summary>
        /// The Node ID of the OrganizationMigration object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The remaining amount of repos to be migrated.
        /// </summary>
        public int? RemainingRepositoriesCount { get; }

        /// <summary>
        /// The name of the source organization to be migrated.
        /// </summary>
        public string SourceOrgName { get; }

        /// <summary>
        /// The URL of the source organization to migrate.
        /// </summary>
        public string SourceOrgUrl { get; }

        /// <summary>
        /// The migration state.
        /// </summary>
        public OrganizationMigrationState State { get; }

        /// <summary>
        /// The name of the target organization.
        /// </summary>
        public string TargetOrgName { get; }

        /// <summary>
        /// The total amount of repositories to be migrated.
        /// </summary>
        public int? TotalRepositoriesCount { get; }

        internal static OrganizationMigration Create(Expression expression)
        {
            return new OrganizationMigration(expression);
        }
    }
}