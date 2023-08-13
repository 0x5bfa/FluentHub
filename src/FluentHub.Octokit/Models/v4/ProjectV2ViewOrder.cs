// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for project v2 view connections
	/// </summary>
	public class ProjectV2ViewOrder
	{
		/// <summary>
		/// The field to order the project v2 views by.
		/// </summary>
		public ProjectV2ViewOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
