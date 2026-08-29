namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Metadata for a team membership for org.restore_member actions
    /// </summary>
    public class OrgRestoreMemberMembershipTeamAuditEntryData : QueryableValue<OrgRestoreMemberMembershipTeamAuditEntryData>
    {
        internal OrgRestoreMemberMembershipTeamAuditEntryData(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The team associated with the action
        /// </summary>
        public Team Team => this.CreateProperty(x => x.Team, Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        public string TeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this team
        /// </summary>
        public string TeamUrl { get; }

        internal static OrgRestoreMemberMembershipTeamAuditEntryData Create(Expression expression)
        {
            return new OrgRestoreMemberMembershipTeamAuditEntryData(expression);
        }
    }
}