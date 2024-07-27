// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for GitHub Sponsors activity connections.
	/// </summary>
	public class SponsorsActivityOrder
	{
		/// <summary>
		/// The field to order activity by.
		/// </summary>
		public SponsorsActivityOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
