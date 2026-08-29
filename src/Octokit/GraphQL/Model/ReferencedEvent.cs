namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'referenced' event on a given `ReferencedSubject`.
    /// </summary>
    public class ReferencedEvent : QueryableValue<ReferencedEvent>
    {
        internal ReferencedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the commit associated with the 'referenced' event.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the repository associated with the 'referenced' event.
        /// </summary>
        public Repository CommitRepository => this.CreateProperty(x => x.CommitRepository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the ReferencedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Reference originated in a different repository.
        /// </summary>
        public bool IsCrossRepository { get; }

        /// <summary>
        /// Checks if the commit message itself references the subject. Can be false in the case of a commit comment reference.
        /// </summary>
        public bool IsDirectReference { get; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public ReferencedSubject Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.ReferencedSubject.Create);

        internal static ReferencedEvent Create(Expression expression)
        {
            return new ReferencedEvent(expression);
        }
    }
}