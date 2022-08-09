namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types of memberships that can be restored for an Organization member.
    /// </summary>
    public class OrgRestoreMemberAuditEntryMembership
    {
        /// <summary>
        /// Metadata for an organization membership for org.restore_member actions
        /// </summary>
        public OrgRestoreMemberMembershipOrganizationAuditEntryData OrgRestoreMemberMembershipOrganizationAuditEntryData { get; set; }

        /// <summary>
        /// Metadata for a repository membership for org.restore_member actions
        /// </summary>
        public OrgRestoreMemberMembershipRepositoryAuditEntryData OrgRestoreMemberMembershipRepositoryAuditEntryData { get; set; }

        /// <summary>
        /// Metadata for a team membership for org.restore_member actions
        /// </summary>
        public OrgRestoreMemberMembershipTeamAuditEntryData OrgRestoreMemberMembershipTeamAuditEntryData { get; set; }
    }
}