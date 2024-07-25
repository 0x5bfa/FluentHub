// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for discussion poll option connections.
	/// </summary>
	public class DiscussionPollOptionOrder
	{
		/// <summary>
		/// The field to order poll options by.
		/// </summary>
		public DiscussionPollOptionOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
