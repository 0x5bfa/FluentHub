// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
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
