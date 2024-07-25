// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for organization connections.
	/// </summary>
	public class OrganizationOrder
	{
		/// <summary>
		/// The field to order organizations by.
		/// </summary>
		public OrganizationOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
