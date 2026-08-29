namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'renamed' event on a given issue or pull request
    /// </summary>
    public class RenamedTitleEvent : QueryableValue<RenamedTitleEvent>
    {
        internal RenamedTitleEvent(Expression expression) : base(expression)
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
        /// Identifies the current title of the issue or pull request.
        /// </summary>
        public string CurrentTitle { get; }

        /// <summary>
        /// The Node ID of the RenamedTitleEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the previous title of the issue or pull request.
        /// </summary>
        public string PreviousTitle { get; }

        /// <summary>
        /// Subject that was renamed.
        /// </summary>
        public RenamedTitleSubject Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.RenamedTitleSubject.Create);

        internal static RenamedTitleEvent Create(Expression expression)
        {
            return new RenamedTitleEvent(expression);
        }
    }
}