// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of workflow runs can be ordered upon return.
	/// </summary>
	public class WorkflowRunOrder
	{
		/// <summary>
		/// The field by which to order workflows.
		/// </summary>
		public WorkflowRunOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order workflow runs by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
