namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Saved Reply is text a user can use to reply quickly.
    /// </summary>
    public class SavedReply : QueryableValue<SavedReply>
    {
        internal SavedReply(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the saved reply.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The saved reply body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the SavedReply object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The title of the saved reply.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The user that saved this reply.
        /// </summary>
        public IActor User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        internal static SavedReply Create(Expression expression)
        {
            return new SavedReply(expression);
        }
    }
}