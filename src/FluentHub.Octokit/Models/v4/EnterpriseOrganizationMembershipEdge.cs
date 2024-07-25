// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An enterprise organization that a user is a member of.
	/// </summary>
	public class EnterpriseOrganizationMembershipEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public Organization Node { get; set; }

		/// <summary>
		/// The role of the user in the enterprise membership.
		/// </summary>
		public EnterpriseUserAccountMembershipRole Role { get; set; }
	}
}
