// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for saved reply connections.
	/// </summary>
	public class SavedReplyOrder
	{
		/// <summary>
		/// The field to order saved replies by.
		/// </summary>
		public SavedReplyOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
