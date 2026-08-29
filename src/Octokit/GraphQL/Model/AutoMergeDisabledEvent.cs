namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'auto_merge_disabled' event on a given pull request.
    /// </summary>
    public class AutoMergeDisabledEvent : QueryableValue<AutoMergeDisabledEvent>
    {
        internal AutoMergeDisabledEvent(Expression expression) : base(expression)
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
        /// The user who disabled auto-merge for this Pull Request
        /// </summary>
        public User Disabler => this.CreateProperty(x => x.Disabler, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The Node ID of the AutoMergeDisabledEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The reason auto-merge was disabled
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// The reason_code relating to why auto-merge was disabled
        /// </summary>
        public string ReasonCode { get; }

        internal static AutoMergeDisabledEvent Create(Expression expression)
        {
            return new AutoMergeDisabledEvent(expression);
        }
    }
}