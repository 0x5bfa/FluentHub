namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'disconnected' event on a given issue or pull request.
    /// </summary>
    public class DisconnectedEvent : QueryableValue<DisconnectedEvent>
    {
        internal DisconnectedEvent(Expression expression) : base(expression)
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
        /// The Node ID of the DisconnectedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Reference originated in a different repository.
        /// </summary>
        public bool IsCrossRepository { get; }

        /// <summary>
        /// Issue or pull request from which the issue was disconnected.
        /// </summary>
        public ReferencedSubject Source => this.CreateProperty(x => x.Source, Octokit.GraphQL.Model.ReferencedSubject.Create);

        /// <summary>
        /// Issue or pull request which was disconnected.
        /// </summary>
        public ReferencedSubject Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.ReferencedSubject.Create);

        internal static DisconnectedEvent Create(Expression expression)
        {
            return new DisconnectedEvent(expression);
        }
    }
}