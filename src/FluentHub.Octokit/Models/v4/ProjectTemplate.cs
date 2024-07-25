// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// GitHub-provided templates for Projects
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectTemplate
	{
		/// <summary>
		/// Create a board with columns for To do, In progress and Done.
		/// </summary>
		[EnumMember(Value = "BASIC_KANBAN")]
		BasicKanban,

		/// <summary>
		/// Create a board with v2 triggers to automatically move cards across To do, In progress and Done columns.
		/// </summary>
		[EnumMember(Value = "AUTOMATED_KANBAN_V2")]
		AutomatedKanbanV2,

		/// <summary>
		/// Create a board with triggers to automatically move cards across columns with review automation.
		/// </summary>
		[EnumMember(Value = "AUTOMATED_REVIEWS_KANBAN")]
		AutomatedReviewsKanban,

		/// <summary>
		/// Create a board to triage and prioritize bugs with To do, priority, and Done columns.
		/// </summary>
		[EnumMember(Value = "BUG_TRIAGE")]
		BugTriage,
	}
}
