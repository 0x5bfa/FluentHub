// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The value of a milestone field in a Project item.
	/// </summary>
	public class ProjectV2ItemFieldMilestoneValue
	{
		/// <summary>
		/// The field that contains this value.
		/// </summary>
		public ProjectV2FieldConfiguration Field { get; set; }

		/// <summary>
		/// Milestone value of a field
		/// </summary>
		public Milestone Milestone { get; set; }
	}
}
