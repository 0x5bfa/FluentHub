namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The user's description of what they're currently doing.
    /// </summary>
    public class UserStatus : QueryableValue<UserStatus>
    {
        internal UserStatus(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// An emoji summarizing the user's status.
        /// </summary>
        public string Emoji { get; }

        /// <summary>
        /// The status emoji as HTML.
        /// </summary>
        public string EmojiHTML { get; }

        /// <summary>
        /// If set, the status will not be shown after this date.
        /// </summary>
        public DateTimeOffset? ExpiresAt { get; }

        /// <summary>
        /// The Node ID of the UserStatus object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether this status indicates the user is not fully available on GitHub.
        /// </summary>
        public bool IndicatesLimitedAvailability { get; }

        /// <summary>
        /// A brief message describing what the user is doing.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The organization whose members can see this status. If null, this status is publicly visible.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The user who has this status.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static UserStatus Create(Expression expression)
        {
            return new UserStatus(expression);
        }
    }
}