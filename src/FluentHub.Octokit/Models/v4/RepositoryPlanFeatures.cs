// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Information about the availability of features and limits for a repository based on its billing plan.
	/// </summary>
	public class RepositoryPlanFeatures
	{
		/// <summary>
		/// Whether reviews can be automatically requested and enforced with a CODEOWNERS file
		/// </summary>
		public bool Codeowners { get; set; }

		/// <summary>
		/// Whether pull requests can be created as or converted to draft
		/// </summary>
		public bool DraftPullRequests { get; set; }

		/// <summary>
		/// Maximum number of users that can be assigned to an issue or pull request
		/// </summary>
		public int MaximumAssignees { get; set; }

		/// <summary>
		/// Maximum number of manually-requested reviews on a pull request
		/// </summary>
		public int MaximumManualReviewRequests { get; set; }

		/// <summary>
		/// Whether teams can be requested to review pull requests
		/// </summary>
		public bool TeamReviewRequests { get; set; }
	}
}
