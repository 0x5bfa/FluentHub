namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'labeled' event on a given issue or pull request.
    /// </summary>
    public class LabeledEvent : QueryableValue<LabeledEvent>
    {
        internal LabeledEvent(Expression expression) : base(expression)
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
        /// The Node ID of the LabeledEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the label associated with the 'labeled' event.
        /// </summary>
        public Label Label => this.CreateProperty(x => x.Label, Octokit.GraphQL.Model.Label.Create);

        /// <summary>
        /// Identifies the `Labelable` associated with the event.
        /// </summary>
        public ILabelable Labelable => this.CreateProperty(x => x.Labelable, Octokit.GraphQL.Model.Internal.StubILabelable.Create);

        internal static LabeledEvent Create(Expression expression)
        {
            return new LabeledEvent(expression);
        }
    }
}