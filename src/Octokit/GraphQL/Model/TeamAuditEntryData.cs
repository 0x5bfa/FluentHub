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
    /// Metadata for an audit entry with action team.*
    /// </summary>
    [GraphQLIdentifier("TeamAuditEntryData")]
    public interface ITeamAuditEntryData : IQueryableValue<ITeamAuditEntryData>, IQueryableInterface
    {
        /// <summary>
        /// The team associated with the action
        /// </summary>
        Team Team { get; }

        /// <summary>
        /// The name of the team
        /// </summary>
        string TeamName { get; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        string TeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this team
        /// </summary>
        string TeamUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("TeamAuditEntryData")]
    internal class StubITeamAuditEntryData : QueryableValue<StubITeamAuditEntryData>, ITeamAuditEntryData
    {
        internal StubITeamAuditEntryData(Expression expression) : base(expression)
        {
        }

        public Team Team => this.CreateProperty(x => x.Team, Octokit.GraphQL.Model.Team.Create);

        public string TeamName { get; }

        public string TeamResourcePath { get; }

        public string TeamUrl { get; }

        internal static StubITeamAuditEntryData Create(Expression expression)
        {
            return new StubITeamAuditEntryData(expression);
        }
    }
}