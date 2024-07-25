// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of projects can be ordered upon return.
	/// </summary>
	public class ProjectV2Order
	{
		/// <summary>
		/// The field in which to order projects by.
		/// </summary>
		public ProjectV2OrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order projects by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
