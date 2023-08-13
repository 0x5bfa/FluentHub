// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'unmarked_as_duplicate' event on a given issue or pull request.
	/// </summary>
	public class UnmarkedAsDuplicateEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// The authoritative issue or pull request which has been duplicated by another.
		/// </summary>
		public IssueOrPullRequest Canonical { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The issue or pull request which has been marked as a duplicate of another.
		/// </summary>
		public IssueOrPullRequest Duplicate { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Canonical and duplicate belong to different repositories.
		/// </summary>
		public bool IsCrossRepository { get; set; }
	}
}
