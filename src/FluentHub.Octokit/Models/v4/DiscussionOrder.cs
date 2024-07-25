// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of discussions can be ordered upon return.
	/// </summary>
	public class DiscussionOrder
	{
		/// <summary>
		/// The field by which to order discussions.
		/// </summary>
		public DiscussionOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order discussions by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
