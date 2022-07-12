namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An event related to sponsorship activity.
    /// </summary>
    public class SponsorsActivity
    {
        /// <summary>
        /// What action this activity indicates took place.
        /// </summary>
        public SponsorsActivityAction Action { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The tier that the sponsorship used to use, for tier change events.
        /// </summary>
        public SponsorsTier PreviousSponsorsTier { get; set; }

        /// <summary>
        /// The user or organization who triggered this activity and was/is sponsoring the sponsorable.
        /// </summary>
        public Sponsor Sponsor { get; set; }

        /// <summary>
        /// The user or organization that is being sponsored, the maintainer.
        /// </summary>
        public ISponsorable Sponsorable { get; set; }

        /// <summary>
        /// The associated sponsorship tier.
        /// </summary>
        public SponsorsTier SponsorsTier { get; set; }

        /// <summary>
        /// The timestamp of this event.
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }
    }
}