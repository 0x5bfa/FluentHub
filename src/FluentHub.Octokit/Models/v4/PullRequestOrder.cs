// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of issues can be ordered upon return.
	/// </summary>
	public class PullRequestOrder
	{
		/// <summary>
		/// The field in which to order pull requests by.
		/// </summary>
		public PullRequestOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order pull requests by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
