namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'base_ref_force_pushed' event on a given pull request.
    /// </summary>
    public class BaseRefForcePushedEvent : QueryableValue<BaseRefForcePushedEvent>
    {
        internal BaseRefForcePushedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the after commit SHA for the 'base_ref_force_pushed' event.
        /// </summary>
        public Commit AfterCommit => this.CreateProperty(x => x.AfterCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the before commit SHA for the 'base_ref_force_pushed' event.
        /// </summary>
        public Commit BeforeCommit => this.CreateProperty(x => x.BeforeCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the BaseRefForcePushedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the fully qualified ref name for the 'base_ref_force_pushed' event.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Model.Ref.Create);

        internal static BaseRefForcePushedEvent Create(Expression expression)
        {
            return new BaseRefForcePushedEvent(expression);
        }
    }
}