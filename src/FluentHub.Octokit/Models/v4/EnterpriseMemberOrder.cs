// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for enterprise member connections.
	/// </summary>
	public class EnterpriseMemberOrder
	{
		/// <summary>
		/// The field to order enterprise members by.
		/// </summary>
		public EnterpriseMemberOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
