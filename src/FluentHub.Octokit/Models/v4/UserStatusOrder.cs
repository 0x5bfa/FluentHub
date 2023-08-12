// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for user status connections.
	/// </summary>
	public class UserStatusOrder
	{
		/// <summary>
		/// The field to order user statuses by.
		/// </summary>
		public UserStatusOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
