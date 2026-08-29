namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'marked_as_duplicate' event on a given issue or pull request.
    /// </summary>
    public class MarkedAsDuplicateEvent : QueryableValue<MarkedAsDuplicateEvent>
    {
        internal MarkedAsDuplicateEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The authoritative issue or pull request which has been duplicated by another.
        /// </summary>
        public IssueOrPullRequest Canonical => this.CreateProperty(x => x.Canonical, Octokit.GraphQL.Model.IssueOrPullRequest.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The issue or pull request which has been marked as a duplicate of another.
        /// </summary>
        public IssueOrPullRequest Duplicate => this.CreateProperty(x => x.Duplicate, Octokit.GraphQL.Model.IssueOrPullRequest.Create);

        /// <summary>
        /// The Node ID of the MarkedAsDuplicateEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Canonical and duplicate belong to different repositories.
        /// </summary>
        public bool IsCrossRepository { get; }

        internal static MarkedAsDuplicateEvent Create(Expression expression)
        {
            return new MarkedAsDuplicateEvent(expression);
        }
    }
}