// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Specifies a review comment to be left with a Pull Request Review.
	/// </summary>
	public class DraftPullRequestReviewComment
	{
		/// <summary>
		/// Path to the file being commented on.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Position in the file to leave a comment on.
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// Body of the comment to leave.
		/// </summary>
		public string Body { get; set; }
	}
}
