namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'transferred' event on a given issue or pull request.
    /// </summary>
    public class TransferredEvent : QueryableValue<TransferredEvent>
    {
        internal TransferredEvent(Expression expression) : base(expression)
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
        /// The repository this came from
        /// </summary>
        public Repository FromRepository => this.CreateProperty(x => x.FromRepository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The Node ID of the TransferredEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the issue associated with the event.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octokit.GraphQL.Model.Issue.Create);

        internal static TransferredEvent Create(Expression expression)
        {
            return new TransferredEvent(expression);
        }
    }
}