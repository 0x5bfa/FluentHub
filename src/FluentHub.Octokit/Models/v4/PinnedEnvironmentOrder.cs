// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for pinned environments
	/// </summary>
	public class PinnedEnvironmentOrder
	{
		/// <summary>
		/// The field to order pinned environments by.
		/// </summary>
		public PinnedEnvironmentOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order pinned environments by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
