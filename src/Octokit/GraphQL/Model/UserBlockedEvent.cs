namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'user_blocked' event on a given user.
    /// </summary>
    public class UserBlockedEvent : QueryableValue<UserBlockedEvent>
    {
        internal UserBlockedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Number of days that the user was blocked for.
        /// </summary>
        public UserBlockDuration BlockDuration { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the UserBlockedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The user who was blocked.
        /// </summary>
        public User Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.User.Create);

        internal static UserBlockedEvent Create(Expression expression)
        {
            return new UserBlockedEvent(expression);
        }
    }
}