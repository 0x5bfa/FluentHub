// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for project v2 field connections
	/// </summary>
	public class ProjectV2FieldOrder
	{
		/// <summary>
		/// The field to order the project v2 fields by.
		/// </summary>
		public ProjectV2FieldOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
