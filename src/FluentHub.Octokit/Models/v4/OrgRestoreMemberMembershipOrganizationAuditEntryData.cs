// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Metadata for an organization membership for org.restore_member actions
	/// </summary>
	public class OrgRestoreMemberMembershipOrganizationAuditEntryData
	{
		/// <summary>
		/// The Organization associated with the Audit Entry.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// The name of the Organization.
		/// </summary>
		public string OrganizationName { get; set; }

		/// <summary>
		/// The HTTP path for the organization
		/// </summary>
		public string OrganizationResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the organization
		/// </summary>
		public string OrganizationUrl { get; set; }
	}
}
