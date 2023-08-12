// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The queue of pull request entries to be merged into a protected branch in a repository.
	/// </summary>
	public class MergeQueue
	{
		/// <summary>
		/// The configuration for this merge queue
		/// </summary>
		public MergeQueueConfiguration Configuration { get; set; }

		/// <summary>
		/// The entries in the queue
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public MergeQueueEntryConnection Entries { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The estimated time in seconds until a newly added entry would be merged
		/// </summary>
		public int? NextEntryEstimatedTimeToMerge { get; set; }

		/// <summary>
		/// The repository this merge queue belongs to
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this merge queue
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this merge queue
		/// </summary>
		public string Url { get; set; }
	}
}
