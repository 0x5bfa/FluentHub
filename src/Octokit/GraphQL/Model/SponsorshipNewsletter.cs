namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An update sent to sponsors of a user or organization on GitHub Sponsors.
    /// </summary>
    public class SponsorshipNewsletter : QueryableValue<SponsorshipNewsletter>
    {
        internal SponsorshipNewsletter(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The author of the newsletter.
        /// </summary>
        public User Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The contents of the newsletter, the message the sponsorable wanted to give.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the SponsorshipNewsletter object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Indicates if the newsletter has been made available to sponsors.
        /// </summary>
        public bool IsPublished { get; }

        /// <summary>
        /// The user or organization this newsletter is from.
        /// </summary>
        public ISponsorable Sponsorable => this.CreateProperty(x => x.Sponsorable, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        /// <summary>
        /// The subject of the newsletter, what it's about.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static SponsorshipNewsletter Create(Expression expression)
        {
            return new SponsorshipNewsletter(expression);
        }
    }
}