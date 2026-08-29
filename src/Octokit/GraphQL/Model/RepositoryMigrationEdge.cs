namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a repository migration.
    /// </summary>
    public class RepositoryMigrationEdge : QueryableValue<RepositoryMigrationEdge>
    {
        internal RepositoryMigrationEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public RepositoryMigration Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.RepositoryMigration.Create);

        internal static RepositoryMigrationEdge Create(Expression expression)
        {
            return new RepositoryMigrationEdge(expression);
        }
    }
}