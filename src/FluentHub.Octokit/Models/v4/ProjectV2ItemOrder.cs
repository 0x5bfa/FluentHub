// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for project v2 item connections
	/// </summary>
	public class ProjectV2ItemOrder
	{
		/// <summary>
		/// The field to order the project v2 items by.
		/// </summary>
		public ProjectV2ItemOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
