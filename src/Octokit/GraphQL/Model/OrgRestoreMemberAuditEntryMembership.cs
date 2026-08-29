namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types of memberships that can be restored for an Organization member.
    /// </summary>
    public class OrgRestoreMemberAuditEntryMembership : QueryableValue<OrgRestoreMemberAuditEntryMembership>, IUnion
    {
        internal OrgRestoreMemberAuditEntryMembership(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Metadata for an organization membership for org.restore_member actions
            /// </summary>
            public Selector<T> OrgRestoreMemberMembershipOrganizationAuditEntryData(Func<OrgRestoreMemberMembershipOrganizationAuditEntryData, T> selector) => default;

            /// <summary>
            /// Metadata for a repository membership for org.restore_member actions
            /// </summary>
            public Selector<T> OrgRestoreMemberMembershipRepositoryAuditEntryData(Func<OrgRestoreMemberMembershipRepositoryAuditEntryData, T> selector) => default;

            /// <summary>
            /// Metadata for a team membership for org.restore_member actions
            /// </summary>
            public Selector<T> OrgRestoreMemberMembershipTeamAuditEntryData(Func<OrgRestoreMemberMembershipTeamAuditEntryData, T> selector) => default;
        }

        internal static OrgRestoreMemberAuditEntryMembership Create(Expression expression)
        {
            return new OrgRestoreMemberAuditEntryMembership(expression);
        }
    }
}