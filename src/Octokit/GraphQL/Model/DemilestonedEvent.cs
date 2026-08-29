namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'demilestoned' event on a given issue or pull request.
    /// </summary>
    public class DemilestonedEvent : QueryableValue<DemilestonedEvent>
    {
        internal DemilestonedEvent(Expression expression) : base(expression)
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
        /// The Node ID of the DemilestonedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the milestone title associated with the 'demilestoned' event.
        /// </summary>
        public string MilestoneTitle { get; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public MilestoneItem Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.MilestoneItem.Create);

        internal static DemilestonedEvent Create(Expression expression)
        {
            return new DemilestonedEvent(expression);
        }
    }
}