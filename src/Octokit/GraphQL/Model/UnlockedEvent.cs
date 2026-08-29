namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'unlocked' event on a given issue or pull request.
    /// </summary>
    public class UnlockedEvent : QueryableValue<UnlockedEvent>
    {
        internal UnlockedEvent(Expression expression) : base(expression)
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
        /// The Node ID of the UnlockedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Object that was unlocked.
        /// </summary>
        public ILockable Lockable => this.CreateProperty(x => x.Lockable, Octokit.GraphQL.Model.Internal.StubILockable.Create);

        internal static UnlockedEvent Create(Expression expression)
        {
            return new UnlockedEvent(expression);
        }
    }
}