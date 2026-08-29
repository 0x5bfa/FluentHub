namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A sponsorship relationship between a sponsor and a maintainer
    /// </summary>
    public class Sponsorship : QueryableValue<Sponsorship>
    {
        internal Sponsorship(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the Sponsorship object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the sponsorship is active. False implies the sponsor is a past sponsor of the maintainer, while true implies they are a current sponsor.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// Whether this sponsorship represents a one-time payment versus a recurring sponsorship.
        /// </summary>
        public bool IsOneTimePayment { get; }

        /// <summary>
        /// Whether the sponsor has chosen to receive sponsorship update emails sent from the sponsorable. Only returns a non-null value when the viewer has permission to know this.
        /// </summary>
        public bool? IsSponsorOptedIntoEmail { get; }

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        [Obsolete(@"`Sponsorship.maintainer` will be removed. Use `Sponsorship.sponsorable` instead. Removal on 2020-04-01 UTC.")]
        public User Maintainer => this.CreateProperty(x => x.Maintainer, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The platform that was most recently used to pay for the sponsorship.
        /// </summary>
        public SponsorshipPaymentSource? PaymentSource { get; }

        /// <summary>
        /// The privacy level for this sponsorship.
        /// </summary>
        public SponsorshipPrivacy PrivacyLevel { get; }

        /// <summary>
        /// The user that is sponsoring. Returns null if the sponsorship is private or if sponsor is not a user.
        /// </summary>
        [Obsolete(@"`Sponsorship.sponsor` will be removed. Use `Sponsorship.sponsorEntity` instead. Removal on 2020-10-01 UTC.")]
        public User Sponsor => this.CreateProperty(x => x.Sponsor, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The user or organization that is sponsoring, if you have permission to view them.
        /// </summary>
        public Sponsor SponsorEntity => this.CreateProperty(x => x.SponsorEntity, Octokit.GraphQL.Model.Sponsor.Create);

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        public ISponsorable Sponsorable => this.CreateProperty(x => x.Sponsorable, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        /// <summary>
        /// The associated sponsorship tier
        /// </summary>
        public SponsorsTier Tier => this.CreateProperty(x => x.Tier, Octokit.GraphQL.Model.SponsorsTier.Create);

        /// <summary>
        /// Identifies the date and time when the current tier was chosen for this sponsorship.
        /// </summary>
        public DateTimeOffset? TierSelectedAt { get; }

        internal static Sponsorship Create(Expression expression)
        {
            return new Sponsorship(expression);
        }
    }
}