namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A placeholder user for attribution of imported data on GitHub.
    /// </summary>
    public class Mannequin : QueryableValue<Mannequin>
    {
        internal Mannequin(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the GitHub App's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The user that has claimed the data attributed to this mannequin.
        /// </summary>
        public User Claimant => this.CreateProperty(x => x.Claimant, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The mannequin's email on the source instance.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The Node ID of the Mannequin object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The username of the actor.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The HTML path to this resource.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        public string Url { get; }

        internal static Mannequin Create(Expression expression)
        {
            return new Mannequin(expression);
        }
    }
}