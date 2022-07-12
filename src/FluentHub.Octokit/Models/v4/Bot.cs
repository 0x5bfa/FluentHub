namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A special type of user which takes actions on behalf of GitHub Apps.
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// A URL pointing to the GitHub App's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The username of the actor.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The HTTP path for this bot
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this bot
        /// </summary>
        public string Url { get; set; }
    }
}