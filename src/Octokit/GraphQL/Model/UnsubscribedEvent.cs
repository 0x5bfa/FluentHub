namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'unsubscribed' event on a given `Subscribable`.
    /// </summary>
    public class UnsubscribedEvent : QueryableValue<UnsubscribedEvent>
    {
        internal UnsubscribedEvent(Expression expression) : base(expression)
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
        /// The Node ID of the UnsubscribedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public ISubscribable Subscribable => this.CreateProperty(x => x.Subscribable, Octokit.GraphQL.Model.Internal.StubISubscribable.Create);

        internal static UnsubscribedEvent Create(Expression expression)
        {
            return new UnsubscribedEvent(expression);
        }
    }
}