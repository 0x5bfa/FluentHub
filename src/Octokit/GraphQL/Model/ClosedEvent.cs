namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'closed' event on any `Closable`.
    /// </summary>
    public class ClosedEvent : QueryableValue<ClosedEvent>
    {
        internal ClosedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Object that was closed.
        /// </summary>
        public IClosable Closable => this.CreateProperty(x => x.Closable, Octokit.GraphQL.Model.Internal.StubIClosable.Create);

        /// <summary>
        /// Object which triggered the creation of this event.
        /// </summary>
        public Closer Closer => this.CreateProperty(x => x.Closer, Octokit.GraphQL.Model.Closer.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the ClosedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The HTTP path for this closed event.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The reason the issue state was changed to closed.
        /// </summary>
        public IssueStateReason? StateReason { get; }

        /// <summary>
        /// The HTTP URL for this closed event.
        /// </summary>
        public string Url { get; }

        internal static ClosedEvent Create(Expression expression)
        {
            return new ClosedEvent(expression);
        }
    }
}