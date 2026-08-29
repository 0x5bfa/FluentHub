namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Enterprise Importer (GEI) migration source.
    /// </summary>
    public class MigrationSource : QueryableValue<MigrationSource>
    {
        internal MigrationSource(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the MigrationSource object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The migration source name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The migration source type.
        /// </summary>
        public MigrationSourceType Type { get; }

        /// <summary>
        /// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
        /// </summary>
        public string Url { get; }

        internal static MigrationSource Create(Expression expression)
        {
            return new MigrationSource(expression);
        }
    }
}