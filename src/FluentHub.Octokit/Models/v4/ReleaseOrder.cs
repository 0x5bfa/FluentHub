// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of releases can be ordered upon return.
	/// </summary>
	public class ReleaseOrder
	{
		/// <summary>
		/// The field in which to order releases by.
		/// </summary>
		public ReleaseOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order releases by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
