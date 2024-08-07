// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of UpdatePullRequestReviewComment
	/// </summary>
	public class UpdatePullRequestReviewCommentPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The updated comment.
		/// </summary>
		public PullRequestReviewComment PullRequestReviewComment { get; set; }
	}
}
