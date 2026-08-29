namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An emoji reaction to a particular piece of content.
    /// </summary>
    public class Reaction : QueryableValue<Reaction>
    {
        internal Reaction(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the emoji reaction.
        /// </summary>
        public ReactionContent Content { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Reaction object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The reactable piece of content
        /// </summary>
        public IReactable Reactable => this.CreateProperty(x => x.Reactable, Octokit.GraphQL.Model.Internal.StubIReactable.Create);

        /// <summary>
        /// Identifies the user who created this reaction.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static Reaction Create(Expression expression)
        {
            return new Reaction(expression);
        }
    }
}