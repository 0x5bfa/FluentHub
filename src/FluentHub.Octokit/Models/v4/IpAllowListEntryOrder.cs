// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for IP allow list entry connections.
	/// </summary>
	public class IpAllowListEntryOrder
	{
		/// <summary>
		/// The field to order IP allow list entries by.
		/// </summary>
		public IpAllowListEntryOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
