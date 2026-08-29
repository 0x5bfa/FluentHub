namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Enterprise Importer (GEI) repository migration.
    /// </summary>
    public class RepositoryMigration : QueryableValue<RepositoryMigration>
    {
        internal RepositoryMigration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The migration flag to continue on error.
        /// </summary>
        public bool ContinueOnError { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public string DatabaseId { get; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        public string FailureReason { get; }

        /// <summary>
        /// The Node ID of the RepositoryMigration object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The URL for the migration log (expires 1 day after migration completes).
        /// </summary>
        public string MigrationLogUrl { get; }

        /// <summary>
        /// The migration source.
        /// </summary>
        public MigrationSource MigrationSource => this.CreateProperty(x => x.MigrationSource, Octokit.GraphQL.Model.MigrationSource.Create);

        /// <summary>
        /// The target repository name.
        /// </summary>
        public string RepositoryName { get; }

        /// <summary>
        /// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
        /// </summary>
        public string SourceUrl { get; }

        /// <summary>
        /// The migration state.
        /// </summary>
        public MigrationState State { get; }

        /// <summary>
        /// The number of warnings encountered for this migration. To review the warnings, check the [Migration Log](https://docs.github.com/migrations/using-github-enterprise-importer/completing-your-migration-with-github-enterprise-importer/accessing-your-migration-logs-for-github-enterprise-importer).
        /// </summary>
        public int WarningsCount { get; }

        internal static RepositoryMigration Create(Expression expression)
        {
            return new RepositoryMigration(expression);
        }
    }
}