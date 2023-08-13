// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents the latest point in the pull request timeline for which the viewer has seen the pull request's commits.
	/// </summary>
	public class PullRequestRevisionMarker
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The last commit the viewer has seen.
		/// </summary>
		public Commit LastSeenCommit { get; set; }

		/// <summary>
		/// The pull request to which the marker belongs.
		/// </summary>
		public PullRequest PullRequest { get; set; }
	}
}
