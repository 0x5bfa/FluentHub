// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Project field values
	/// </summary>
	public class ProjectV2ItemFieldValue
	{
		/// <summary>
		/// The value of a date field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldDateValue ProjectV2ItemFieldDateValue { get; set; }

		/// <summary>
		/// The value of an iteration field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldIterationValue ProjectV2ItemFieldIterationValue { get; set; }

		/// <summary>
		/// The value of the labels field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldLabelValue ProjectV2ItemFieldLabelValue { get; set; }

		/// <summary>
		/// The value of a milestone field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldMilestoneValue ProjectV2ItemFieldMilestoneValue { get; set; }

		/// <summary>
		/// The value of a number field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldNumberValue ProjectV2ItemFieldNumberValue { get; set; }

		/// <summary>
		/// The value of a pull request field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldPullRequestValue ProjectV2ItemFieldPullRequestValue { get; set; }

		/// <summary>
		/// The value of a repository field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldRepositoryValue ProjectV2ItemFieldRepositoryValue { get; set; }

		/// <summary>
		/// The value of a reviewers field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldReviewerValue ProjectV2ItemFieldReviewerValue { get; set; }

		/// <summary>
		/// The value of a single select field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldSingleSelectValue ProjectV2ItemFieldSingleSelectValue { get; set; }

		/// <summary>
		/// The value of a text field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldTextValue ProjectV2ItemFieldTextValue { get; set; }

		/// <summary>
		/// The value of a user field in a Project item.
		/// </summary>
		public ProjectV2ItemFieldUserValue ProjectV2ItemFieldUserValue { get; set; }
	}
}
