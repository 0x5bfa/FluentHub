// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Entries in a MergeQueue
	/// </summary>
	public class MergeQueueEntry
	{
		/// <summary>
		/// The base commit for this entry
		/// </summary>
		public Commit BaseCommit { get; set; }

		/// <summary>
		/// The date and time this entry was added to the merge queue
		/// </summary>
		public DateTimeOffset EnqueuedAt { get; set; }

		/// <summary>
		/// Humanized string of "The date and time this entry was added to the merge queue"
		/// <summary>
		public string EnqueuedAtHumanized { get; set; }

		/// <summary>
		/// The actor that enqueued this entry
		/// </summary>
		public IActor Enqueuer { get; set; }

		/// <summary>
		/// The estimated time in seconds until this entry will be merged
		/// </summary>
		public int? EstimatedTimeToMerge { get; set; }

		/// <summary>
		/// The head commit for this entry
		/// </summary>
		public Commit HeadCommit { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether this pull request should jump the queue
		/// </summary>
		public bool Jump { get; set; }

		/// <summary>
		/// The merge queue that this entry belongs to
		/// </summary>
		public MergeQueue MergeQueue { get; set; }

		/// <summary>
		/// The position of this entry in the queue
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// The pull request that will be added to a merge group
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// Does this pull request need to be deployed on its own
		/// </summary>
		public bool Solo { get; set; }

		/// <summary>
		/// The state of this entry in the queue
		/// </summary>
		public MergeQueueEntryState State { get; set; }
	}
}
