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
    /// Metadata for an audit entry with action oauth_application.*
    /// </summary>
    [GraphQLIdentifier("OauthApplicationAuditEntryData")]
    public interface IOauthApplicationAuditEntryData : IQueryableValue<IOauthApplicationAuditEntryData>, IQueryableInterface
    {
        /// <summary>
        /// The name of the OAuth application.
        /// </summary>
        string OauthApplicationName { get; }

        /// <summary>
        /// The HTTP path for the OAuth application
        /// </summary>
        string OauthApplicationResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the OAuth application
        /// </summary>
        string OauthApplicationUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("OauthApplicationAuditEntryData")]
    internal class StubIOauthApplicationAuditEntryData : QueryableValue<StubIOauthApplicationAuditEntryData>, IOauthApplicationAuditEntryData
    {
        internal StubIOauthApplicationAuditEntryData(Expression expression) : base(expression)
        {
        }

        public string OauthApplicationName { get; }

        public string OauthApplicationResourcePath { get; }

        public string OauthApplicationUrl { get; }

        internal static StubIOauthApplicationAuditEntryData Create(Expression expression)
        {
            return new StubIOauthApplicationAuditEntryData(expression);
        }
    }
}