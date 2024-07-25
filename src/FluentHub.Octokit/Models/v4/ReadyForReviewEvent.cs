// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'ready_for_review' event on a given pull request.
	/// </summary>
	public class ReadyForReviewEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the ReadyForReviewEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// The HTTP path for this ready for review event.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this ready for review event.
		/// </summary>
		public string Url { get; set; }
	}
}
