// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Metadata for a team membership for org.restore_member actions
	/// </summary>
	public class OrgRestoreMemberMembershipTeamAuditEntryData
	{
		/// <summary>
		/// The team associated with the action
		/// </summary>
		public Team Team { get; set; }

		/// <summary>
		/// The name of the team
		/// </summary>
		public string TeamName { get; set; }

		/// <summary>
		/// The HTTP path for this team
		/// </summary>
		public string TeamResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this team
		/// </summary>
		public string TeamUrl { get; set; }
	}
}
