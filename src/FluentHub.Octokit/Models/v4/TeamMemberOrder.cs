// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for team member connections
	/// </summary>
	public class TeamMemberOrder
	{
		/// <summary>
		/// The field to order team members by.
		/// </summary>
		public TeamMemberOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
