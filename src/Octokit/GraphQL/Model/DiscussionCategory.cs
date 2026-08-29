namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A category for discussions in a repository.
    /// </summary>
    public class DiscussionCategory : QueryableValue<DiscussionCategory>
    {
        internal DiscussionCategory(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// A description of this category.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// An emoji representing this category.
        /// </summary>
        public string Emoji { get; }

        /// <summary>
        /// This category's emoji rendered as HTML.
        /// </summary>
        public string EmojiHTML { get; }

        /// <summary>
        /// The Node ID of the DiscussionCategory object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not discussions in this category support choosing an answer with the markDiscussionCommentAsAnswer mutation.
        /// </summary>
        public bool IsAnswerable { get; }

        /// <summary>
        /// The name of this category.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The slug of this category.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static DiscussionCategory Create(Expression expression)
        {
            return new DiscussionCategory(expression);
        }
    }
}