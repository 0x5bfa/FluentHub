// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which team discussion connections can be ordered.
	/// </summary>
	public class TeamDiscussionOrder
	{
		/// <summary>
		/// The field by which to order nodes.
		/// </summary>
		public TeamDiscussionOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order nodes.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
