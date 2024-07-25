// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An enterprise owner in the context of an organization that is part of the enterprise.
	/// </summary>
	public class OrganizationEnterpriseOwnerEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public User Node { get; set; }

		/// <summary>
		/// The role of the owner with respect to the organization.
		/// </summary>
		public RoleInOrganization OrganizationRole { get; set; }
	}
}
