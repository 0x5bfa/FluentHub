// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'removed_from_merge_queue' event on a given pull request.
	/// </summary>
	public class RemovedFromMergeQueueEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Identifies the before commit SHA for the 'removed_from_merge_queue' event.
		/// </summary>
		public Commit BeforeCommit { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The user who removed this Pull Request from the merge queue
		/// </summary>
		public User Enqueuer { get; set; }

		/// <summary>
		/// The Node ID of the RemovedFromMergeQueueEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The merge queue where this pull request was removed from.
		/// </summary>
		public MergeQueue MergeQueue { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// The reason this pull request was removed from the queue.
		/// </summary>
		public string Reason { get; set; }
	}
}
