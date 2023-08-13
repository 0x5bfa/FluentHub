// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'review_dismissed' event on a given issue or pull request.
	/// </summary>
	public class ReviewDismissedEvent
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
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// Identifies the optional message associated with the 'review_dismissed' event.
		/// </summary>
		public string DismissalMessage { get; set; }

		/// <summary>
		/// Identifies the optional message associated with the event, rendered to HTML.
		/// </summary>
		public string DismissalMessageHTML { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Identifies the previous state of the review with the 'review_dismissed' event.
		/// </summary>
		public PullRequestReviewState PreviousReviewState { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// Identifies the commit which caused the review to become stale.
		/// </summary>
		public PullRequestCommit PullRequestCommit { get; set; }

		/// <summary>
		/// The HTTP path for this review dismissed event.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the review associated with the 'review_dismissed' event.
		/// </summary>
		public PullRequestReview Review { get; set; }

		/// <summary>
		/// The HTTP URL for this review dismissed event.
		/// </summary>
		public string Url { get; set; }
	}
}
