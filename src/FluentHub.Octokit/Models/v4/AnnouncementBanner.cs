namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an announcement banner.
    /// </summary>
    public interface IAnnouncementBanner
    {
        /// <summary>
        /// The text of the announcement
        /// </summary>
        string Announcement { get; set; }

        /// <summary>
        /// The expiration date of the announcement, if any
        /// </summary>
        DateTimeOffset? AnnouncementExpiresAt { get; set; }

        /// <summary>
        /// Humanized string of "The expiration date of the announcement, if any"
        /// <summary>
        string AnnouncementExpiresAtHumanized { get; set; }

        /// <summary>
        /// Whether the announcement can be dismissed by the user
        /// </summary>
        bool? AnnouncementUserDismissible { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class AnnouncementBanner : IAnnouncementBanner
    {
        public string Announcement { get; set; }

        public DateTimeOffset? AnnouncementExpiresAt { get; set; }

        public string AnnouncementExpiresAtHumanized { get; set; }

        public bool? AnnouncementUserDismissible { get; set; }
    }
}