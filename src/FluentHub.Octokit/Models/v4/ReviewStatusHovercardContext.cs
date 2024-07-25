// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A hovercard context with a message describing the current code review state of the pull
	/// request.
	/// </summary>
	public class ReviewStatusHovercardContext
	{
		/// <summary>
		/// A string describing this context
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// An octicon to accompany this context
		/// </summary>
		public string Octicon { get; set; }

		/// <summary>
		/// The current status of the pull request with respect to code review.
		/// </summary>
		public PullRequestReviewDecision? ReviewDecision { get; set; }
	}
}
