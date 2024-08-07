// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of AddPullRequestReview
	/// </summary>
	public class AddPullRequestReviewPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The newly created pull request review.
		/// </summary>
		public PullRequestReview PullRequestReview { get; set; }

		/// <summary>
		/// The edge from the pull request's review connection.
		/// </summary>
		public PullRequestReviewEdge ReviewEdge { get; set; }
	}
}
