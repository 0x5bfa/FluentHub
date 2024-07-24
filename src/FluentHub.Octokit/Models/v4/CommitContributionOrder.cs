// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for commit contribution connections.
	/// </summary>
	public class CommitContributionOrder
	{
		/// <summary>
		/// The field by which to order commit contributions.
		/// </summary>
		public CommitContributionOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
