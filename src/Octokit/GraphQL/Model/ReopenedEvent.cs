namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'reopened' event on any `Closable`.
    /// </summary>
    public class ReopenedEvent : QueryableValue<ReopenedEvent>
    {
        internal ReopenedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Object that was reopened.
        /// </summary>
        public IClosable Closable => this.CreateProperty(x => x.Closable, Octokit.GraphQL.Model.Internal.StubIClosable.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the ReopenedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The reason the issue state was changed to open.
        /// </summary>
        public IssueStateReason? StateReason { get; }

        internal static ReopenedEvent Create(Expression expression)
        {
            return new ReopenedEvent(expression);
        }
    }
}