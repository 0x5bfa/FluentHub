namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'convert_to_draft' event on a given pull request.
    /// </summary>
    public class ConvertToDraftEvent : QueryableValue<ConvertToDraftEvent>
    {
        internal ConvertToDraftEvent(Expression expression) : base(expression)
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
        /// The Node ID of the ConvertToDraftEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The HTTP path for this convert to draft event.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this convert to draft event.
        /// </summary>
        public string Url { get; }

        internal static ConvertToDraftEvent Create(Expression expression)
        {
            return new ConvertToDraftEvent(expression);
        }
    }
}