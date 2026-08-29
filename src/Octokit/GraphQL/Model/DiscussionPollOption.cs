namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An option for a discussion poll.
    /// </summary>
    public class DiscussionPollOption : QueryableValue<DiscussionPollOption>
    {
        internal DiscussionPollOption(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the DiscussionPollOption object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The text for this option.
        /// </summary>
        public string Option { get; }

        /// <summary>
        /// The discussion poll that this option belongs to.
        /// </summary>
        public DiscussionPoll Poll => this.CreateProperty(x => x.Poll, Octokit.GraphQL.Model.DiscussionPoll.Create);

        /// <summary>
        /// The total number of votes that have been cast for this option.
        /// </summary>
        public int TotalVoteCount { get; }

        /// <summary>
        /// Indicates if the viewer has voted for this option in the poll.
        /// </summary>
        public bool ViewerHasVoted { get; }

        internal static DiscussionPollOption Create(Expression expression)
        {
            return new DiscussionPollOption(expression);
        }
    }
}