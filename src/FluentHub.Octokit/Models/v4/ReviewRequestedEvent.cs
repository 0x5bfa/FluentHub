// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'review_requested' event on a given pull request.
	/// </summary>
	public class ReviewRequestedEvent
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
		/// The Node ID of the ReviewRequestedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// Identifies the reviewer whose review was requested.
		/// </summary>
		public RequestedReviewer RequestedReviewer { get; set; }
	}
}
