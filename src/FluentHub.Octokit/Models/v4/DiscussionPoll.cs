// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A poll for a discussion.
	/// </summary>
	public class DiscussionPoll
	{
		/// <summary>
		/// The discussion that this poll belongs to.
		/// </summary>
		public Discussion Discussion { get; set; }

		/// <summary>
		/// The Node ID of the DiscussionPoll object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The options for this poll.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">How to order the options for the discussion poll.</param>
		public DiscussionPollOptionConnection Options { get; set; }

		/// <summary>
		/// The question that is being asked by this poll.
		/// </summary>
		public string Question { get; set; }

		/// <summary>
		/// The total number of votes that have been cast for this poll.
		/// </summary>
		public int TotalVoteCount { get; set; }

		/// <summary>
		/// Indicates if the viewer has permission to vote in this poll.
		/// </summary>
		public bool ViewerCanVote { get; set; }

		/// <summary>
		/// Indicates if the viewer has voted for any option in this poll.
		/// </summary>
		public bool ViewerHasVoted { get; set; }
	}
}
