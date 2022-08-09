namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A sponsorship relationship between a sponsor and a maintainer
    /// </summary>
    public class Sponsorship
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether this sponsorship represents a one-time payment versus a recurring sponsorship.
        /// </summary>
        public bool IsOneTimePayment { get; set; }

        /// <summary>
        /// Check if the sponsor has chosen to receive sponsorship update emails sent from the sponsorable. Only returns a non-null value when the viewer has permission to know this.
        /// </summary>
        public bool? IsSponsorOptedIntoEmail { get; set; }

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        [Obsolete(@"`Sponsorship.maintainer` will be removed. Use `Sponsorship.sponsorable` instead. Removal on 2020-04-01 UTC.")]
        public User Maintainer { get; set; }

        /// <summary>
        /// The privacy level for this sponsorship.
        /// </summary>
        public SponsorshipPrivacy PrivacyLevel { get; set; }

        /// <summary>
        /// The user that is sponsoring. Returns null if the sponsorship is private or if sponsor is not a user.
        /// </summary>
        [Obsolete(@"`Sponsorship.sponsor` will be removed. Use `Sponsorship.sponsorEntity` instead. Removal on 2020-10-01 UTC.")]
        public User Sponsor { get; set; }

        /// <summary>
        /// The user or organization that is sponsoring, if you have permission to view them.
        /// </summary>
        public Sponsor SponsorEntity { get; set; }

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        public ISponsorable Sponsorable { get; set; }

        /// <summary>
        /// The associated sponsorship tier
        /// </summary>
        public SponsorsTier Tier { get; set; }

        /// <summary>
        /// Identifies the date and time when the current tier was chosen for this sponsorship.
        /// </summary>
        public DateTimeOffset? TierSelectedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the current tier was chosen for this sponsorship."
        /// <summary>
        public string TierSelectedAtHumanized { get; set; }
    }
}