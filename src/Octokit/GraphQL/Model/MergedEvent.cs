namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'merged' event on a given pull request.
    /// </summary>
    public class MergedEvent : QueryableValue<MergedEvent>
    {
        internal MergedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the commit associated with the `merge` event.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the MergedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the Ref associated with the `merge` event.
        /// </summary>
        public Ref MergeRef => this.CreateProperty(x => x.MergeRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the name of the Ref associated with the `merge` event.
        /// </summary>
        public string MergeRefName { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The HTTP path for this merged event.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this merged event.
        /// </summary>
        public string Url { get; }

        internal static MergedEvent Create(Expression expression)
        {
            return new MergedEvent(expression);
        }
    }
}