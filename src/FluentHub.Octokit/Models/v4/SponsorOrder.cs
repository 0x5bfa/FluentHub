// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for connections to get sponsor entities for GitHub Sponsors.
	/// </summary>
	public class SponsorOrder
	{
		/// <summary>
		/// The field to order sponsor entities by.
		/// </summary>
		public SponsorOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
