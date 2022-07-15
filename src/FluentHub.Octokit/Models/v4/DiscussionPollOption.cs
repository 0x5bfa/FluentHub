namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An option for a discussion poll.
    /// </summary>
    public class DiscussionPollOption
    {
        public ID Id { get; set; }

        /// <summary>
        /// The text for this option.
        /// </summary>
        public string Option { get; set; }

        /// <summary>
        /// The discussion poll that this option belongs to.
        /// </summary>
        public DiscussionPoll Poll { get; set; }

        /// <summary>
        /// The total number of votes that have been cast for this option.
        /// </summary>
        public int TotalVoteCount { get; set; }

        /// <summary>
        /// Indicates if the viewer has voted for this option in the poll.
        /// </summary>
        public bool ViewerHasVoted { get; set; }
    }
}