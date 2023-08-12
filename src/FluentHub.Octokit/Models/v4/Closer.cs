// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The object which triggered a `ClosedEvent`.
	/// </summary>
	public class Closer
	{
		/// <summary>
		/// Represents a Git commit.
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// A repository pull request.
		/// </summary>
		public PullRequest PullRequest { get; set; }
	}
}
