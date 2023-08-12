// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for connections to get sponsorable entities for GitHub Sponsors.
	/// </summary>
	public class SponsorableOrder
	{
		/// <summary>
		/// The field to order sponsorable entities by.
		/// </summary>
		public SponsorableOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
