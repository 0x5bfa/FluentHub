namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for repository migrations.
    /// </summary>
    public class RepositoryMigrationOrder
    {
        /// <summary>
        /// The field to order repository migrations by.
        /// </summary>
        public RepositoryMigrationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public RepositoryMigrationOrderDirection Direction { get; set; }
    }
}