namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an announcement banner.
    /// </summary>
    [GraphQLIdentifier("AnnouncementBanner")]
    public interface IAnnouncementBanner : IQueryableValue<IAnnouncementBanner>, IQueryableInterface
    {
        /// <summary>
        /// The text of the announcement
        /// </summary>
        string Announcement { get; }

        /// <summary>
        /// The expiration date of the announcement, if any
        /// </summary>
        DateTimeOffset? AnnouncementExpiresAt { get; }

        /// <summary>
        /// Whether the announcement can be dismissed by the user
        /// </summary>
        bool? AnnouncementUserDismissible { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("AnnouncementBanner")]
    internal class StubIAnnouncementBanner : QueryableValue<StubIAnnouncementBanner>, IAnnouncementBanner
    {
        internal StubIAnnouncementBanner(Expression expression) : base(expression)
        {
        }

        public string Announcement { get; }

        public DateTimeOffset? AnnouncementExpiresAt { get; }

        public bool? AnnouncementUserDismissible { get; }

        internal static StubIAnnouncementBanner Create(Expression expression)
        {
            return new StubIAnnouncementBanner(expression);
        }
    }
}