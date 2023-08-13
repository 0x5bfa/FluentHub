// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of issues can be ordered upon return.
	/// </summary>
	public class IssueOrder
	{
		/// <summary>
		/// The field in which to order issues by.
		/// </summary>
		public IssueOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order issues by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
