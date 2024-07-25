// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Branch protection rules that are enforced on the viewer.
	/// </summary>
	public class RefUpdateRule
	{
		/// <summary>
		/// Can this branch be deleted.
		/// </summary>
		public bool AllowsDeletions { get; set; }

		/// <summary>
		/// Are force pushes allowed on this branch.
		/// </summary>
		public bool AllowsForcePushes { get; set; }

		/// <summary>
		/// Can matching branches be created.
		/// </summary>
		public bool BlocksCreations { get; set; }

		/// <summary>
		/// Identifies the protection rule pattern.
		/// </summary>
		public string Pattern { get; set; }

		/// <summary>
		/// Number of approving reviews required to update matching branches.
		/// </summary>
		public int? RequiredApprovingReviewCount { get; set; }

		/// <summary>
		/// List of required status check contexts that must pass for commits to be accepted to matching branches.
		/// </summary>
		public List<string> RequiredStatusCheckContexts { get; set; }

		/// <summary>
		/// Are reviews from code owners required to update matching branches.
		/// </summary>
		public bool RequiresCodeOwnerReviews { get; set; }

		/// <summary>
		/// Are conversations required to be resolved before merging.
		/// </summary>
		public bool RequiresConversationResolution { get; set; }

		/// <summary>
		/// Are merge commits prohibited from being pushed to this branch.
		/// </summary>
		public bool RequiresLinearHistory { get; set; }

		/// <summary>
		/// Are commits required to be signed.
		/// </summary>
		public bool RequiresSignatures { get; set; }

		/// <summary>
		/// Is the viewer allowed to dismiss reviews.
		/// </summary>
		public bool ViewerAllowedToDismissReviews { get; set; }

		/// <summary>
		/// Can the viewer push to the branch
		/// </summary>
		public bool ViewerCanPush { get; set; }
	}
}
