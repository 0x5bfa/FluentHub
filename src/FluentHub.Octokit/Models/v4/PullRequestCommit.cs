// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git commit part of a pull request.
	/// </summary>
	public class PullRequestCommit
	{
		/// <summary>
		/// The Git commit object
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// The Node ID of the PullRequestCommit object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The pull request this commit belongs to
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// The HTTP path for this pull request commit
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this pull request commit
		/// </summary>
		public string Url { get; set; }
	}
}
