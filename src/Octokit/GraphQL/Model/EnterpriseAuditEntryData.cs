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
    /// Metadata for an audit entry containing enterprise account information.
    /// </summary>
    [GraphQLIdentifier("EnterpriseAuditEntryData")]
    public interface IEnterpriseAuditEntryData : IQueryableValue<IEnterpriseAuditEntryData>, IQueryableInterface
    {
        /// <summary>
        /// The HTTP path for this enterprise.
        /// </summary>
        string EnterpriseResourcePath { get; }

        /// <summary>
        /// The slug of the enterprise.
        /// </summary>
        string EnterpriseSlug { get; }

        /// <summary>
        /// The HTTP URL for this enterprise.
        /// </summary>
        string EnterpriseUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("EnterpriseAuditEntryData")]
    internal class StubIEnterpriseAuditEntryData : QueryableValue<StubIEnterpriseAuditEntryData>, IEnterpriseAuditEntryData
    {
        internal StubIEnterpriseAuditEntryData(Expression expression) : base(expression)
        {
        }

        public string EnterpriseResourcePath { get; }

        public string EnterpriseSlug { get; }

        public string EnterpriseUrl { get; }

        internal static StubIEnterpriseAuditEntryData Create(Expression expression)
        {
            return new StubIEnterpriseAuditEntryData(expression);
        }
    }
}