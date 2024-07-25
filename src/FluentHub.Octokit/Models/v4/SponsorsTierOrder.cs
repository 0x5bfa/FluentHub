// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Sponsors tiers connections.
	/// </summary>
	public class SponsorsTierOrder
	{
		/// <summary>
		/// The field to order tiers by.
		/// </summary>
		public SponsorsTierOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
