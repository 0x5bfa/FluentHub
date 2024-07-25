// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The review status of a pull request.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestReviewDecision
	{
		/// <summary>
		/// Changes have been requested on the pull request.
		/// </summary>
		[EnumMember(Value = "CHANGES_REQUESTED")]
		ChangesRequested,

		/// <summary>
		/// The pull request has received an approving review.
		/// </summary>
		[EnumMember(Value = "APPROVED")]
		Approved,

		/// <summary>
		/// A review is required before the pull request can be merged.
		/// </summary>
		[EnumMember(Value = "REVIEW_REQUIRED")]
		ReviewRequired,
	}
}
