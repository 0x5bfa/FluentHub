// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which star connections can be ordered.
	/// </summary>
	public class StarOrder
	{
		/// <summary>
		/// The field in which to order nodes by.
		/// </summary>
		public StarOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order nodes.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
