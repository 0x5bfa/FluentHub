namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a GitHub Enterprise Importer (GEI) migration.
    /// </summary>
    [GraphQLIdentifier("Migration")]
    public interface IMigration : IQueryableValue<IMigration>, IQueryableInterface
    {
        /// <summary>
        /// The migration flag to continue on error.
        /// </summary>
        bool ContinueOnError { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        string DatabaseId { get; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        string FailureReason { get; }

        /// <summary>
        /// The Node ID of the Migration object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// The URL for the migration log (expires 1 day after migration completes).
        /// </summary>
        string MigrationLogUrl { get; }

        /// <summary>
        /// The migration source.
        /// </summary>
        MigrationSource MigrationSource { get; }

        /// <summary>
        /// The target repository name.
        /// </summary>
        string RepositoryName { get; }

        /// <summary>
        /// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
        /// </summary>
        string SourceUrl { get; }

        /// <summary>
        /// The migration state.
        /// </summary>
        MigrationState State { get; }

        /// <summary>
        /// The number of warnings encountered for this migration. To review the warnings, check the [Migration Log](https://docs.github.com/migrations/using-github-enterprise-importer/completing-your-migration-with-github-enterprise-importer/accessing-your-migration-logs-for-github-enterprise-importer).
        /// </summary>
        int WarningsCount { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Migration")]
    internal class StubIMigration : QueryableValue<StubIMigration>, IMigration
    {
        internal StubIMigration(Expression expression) : base(expression)
        {
        }

        public bool ContinueOnError { get; }

        public DateTimeOffset CreatedAt { get; }

        public string DatabaseId { get; }

        public string FailureReason { get; }

        public ID Id { get; }

        public string MigrationLogUrl { get; }

        public MigrationSource MigrationSource => this.CreateProperty(x => x.MigrationSource, Octokit.GraphQL.Model.MigrationSource.Create);

        public string RepositoryName { get; }

        public string SourceUrl { get; }

        public MigrationState State { get; }

        public int WarningsCount { get; }

        internal static StubIMigration Create(Expression expression)
        {
            return new StubIMigration(expression);
        }
    }
}