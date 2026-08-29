namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A poll for a discussion.
    /// </summary>
    public class DiscussionPoll : QueryableValue<DiscussionPoll>
    {
        internal DiscussionPoll(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The discussion that this poll belongs to.
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        /// <summary>
        /// The Node ID of the DiscussionPoll object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The options for this poll.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the options for the discussion poll.</param>
        public DiscussionPollOptionConnection Options(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<DiscussionPollOptionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Options(first, after, last, before, orderBy), Octokit.GraphQL.Model.DiscussionPollOptionConnection.Create);

        /// <summary>
        /// The question that is being asked by this poll.
        /// </summary>
        public string Question { get; }

        /// <summary>
        /// The total number of votes that have been cast for this poll.
        /// </summary>
        public int TotalVoteCount { get; }

        /// <summary>
        /// Indicates if the viewer has permission to vote in this poll.
        /// </summary>
        public bool ViewerCanVote { get; }

        /// <summary>
        /// Indicates if the viewer has voted for any option in this poll.
        /// </summary>
        public bool ViewerHasVoted { get; }

        internal static DiscussionPoll Create(Expression expression)
        {
            return new DiscussionPoll(expression);
        }
    }
}