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
    /// Metadata for an audit entry with action repo.*
    /// </summary>
    [GraphQLIdentifier("RepositoryAuditEntryData")]
    public interface IRepositoryAuditEntryData : IQueryableValue<IRepositoryAuditEntryData>, IQueryableInterface
    {
        /// <summary>
        /// The repository associated with the action
        /// </summary>
        Repository Repository { get; }

        /// <summary>
        /// The name of the repository
        /// </summary>
        string RepositoryName { get; }

        /// <summary>
        /// The HTTP path for the repository
        /// </summary>
        string RepositoryResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the repository
        /// </summary>
        string RepositoryUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("RepositoryAuditEntryData")]
    internal class StubIRepositoryAuditEntryData : QueryableValue<StubIRepositoryAuditEntryData>, IRepositoryAuditEntryData
    {
        internal StubIRepositoryAuditEntryData(Expression expression) : base(expression)
        {
        }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        public string RepositoryName { get; }

        public string RepositoryResourcePath { get; }

        public string RepositoryUrl { get; }

        internal static StubIRepositoryAuditEntryData Create(Expression expression)
        {
            return new StubIRepositoryAuditEntryData(expression);
        }
    }
}