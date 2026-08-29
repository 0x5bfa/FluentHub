namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'converted_note_to_issue' event on a given issue or pull request.
    /// </summary>
    public class ConvertedNoteToIssueEvent : QueryableValue<ConvertedNoteToIssueEvent>
    {
        internal ConvertedNoteToIssueEvent(Expression expression) : base(expression)
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
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the ConvertedNoteToIssueEvent object
        /// </summary>
        public ID Id { get; }

        internal static ConvertedNoteToIssueEvent Create(Expression expression)
        {
            return new ConvertedNoteToIssueEvent(expression);
        }
    }
}