// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for connections to get sponsor entities and associated USD amounts for GitHub Sponsors.
	/// </summary>
	public class SponsorAndLifetimeValueOrder
	{
		/// <summary>
		/// The field to order results by.
		/// </summary>
		public SponsorAndLifetimeValueOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
