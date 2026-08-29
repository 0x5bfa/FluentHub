namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'head_ref_deleted' event on a given pull request.
    /// </summary>
    public class HeadRefDeletedEvent : QueryableValue<HeadRefDeletedEvent>
    {
        internal HeadRefDeletedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the Ref associated with the `head_ref_deleted` event.
        /// </summary>
        public Ref HeadRef => this.CreateProperty(x => x.HeadRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the name of the Ref associated with the `head_ref_deleted` event.
        /// </summary>
        public string HeadRefName { get; }

        /// <summary>
        /// The Node ID of the HeadRefDeletedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static HeadRefDeletedEvent Create(Expression expression)
        {
            return new HeadRefDeletedEvent(expression);
        }
    }
}