// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'added_to_merge_queue' event on a given pull request.
	/// </summary>
	public class AddedToMergeQueueEvent
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
		/// The user who added this Pull Request to the merge queue
		/// </summary>
		public User Enqueuer { get; set; }

		/// <summary>
		/// The Node ID of the AddedToMergeQueueEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The merge queue where this pull request was added to.
		/// </summary>
		public MergeQueue MergeQueue { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }
	}
}
