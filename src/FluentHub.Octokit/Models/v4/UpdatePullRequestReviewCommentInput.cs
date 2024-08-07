// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of UpdatePullRequestReviewComment
	/// </summary>
	public class UpdatePullRequestReviewCommentInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The Node ID of the comment to modify.
		/// </summary>
		public ID PullRequestReviewCommentId { get; set; }

		/// <summary>
		/// The text of the comment.
		/// </summary>
		public string Body { get; set; }
	}
}
