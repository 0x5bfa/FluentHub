namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A special type of user which takes actions on behalf of GitHub Apps.
    /// </summary>
    public class Bot : QueryableValue<Bot>
    {
        internal Bot(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the GitHub App's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Bot object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The username of the actor.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The HTTP path for this bot
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this bot
        /// </summary>
        public string Url { get; }

        internal static Bot Create(Expression expression)
        {
            return new Bot(expression);
        }
    }
}