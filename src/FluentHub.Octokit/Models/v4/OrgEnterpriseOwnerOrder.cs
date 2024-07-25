// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for an organization's enterprise owner connections.
	/// </summary>
	public class OrgEnterpriseOwnerOrder
	{
		/// <summary>
		/// The field to order enterprise owners by.
		/// </summary>
		public OrgEnterpriseOwnerOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
