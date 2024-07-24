// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for repository invitation connections.
	/// </summary>
	public class RepositoryInvitationOrder
	{
		/// <summary>
		/// The field to order repository invitations by.
		/// </summary>
		public RepositoryInvitationOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
