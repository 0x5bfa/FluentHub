// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an auto-merge request for a pull request
	/// </summary>
	public class AutoMergeRequest
	{
		/// <summary>
		/// The email address of the author of this auto-merge request.
		/// </summary>
		public string AuthorEmail { get; set; }

		/// <summary>
		/// The commit message of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging.
		/// </summary>
		public string CommitBody { get; set; }

		/// <summary>
		/// The commit title of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging
		/// </summary>
		public string CommitHeadline { get; set; }

		/// <summary>
		/// When was this auto-merge request was enabled.
		/// </summary>
		public DateTimeOffset? EnabledAt { get; set; }

		/// <summary>
		/// Humanized string of "When was this auto-merge request was enabled."
		/// <summary>
		public string EnabledAtHumanized { get; set; }

		/// <summary>
		/// The actor who created the auto-merge request.
		/// </summary>
		public IActor EnabledBy { get; set; }

		/// <summary>
		/// The merge method of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging.
		/// </summary>
		public PullRequestMergeMethod MergeMethod { get; set; }

		/// <summary>
		/// The pull request that this auto-merge request is set against.
		/// </summary>
		public PullRequest PullRequest { get; set; }
	}
}
