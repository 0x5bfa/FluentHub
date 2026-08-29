namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Sponsors tier associated with a GitHub Sponsors listing.
    /// </summary>
    public class SponsorsTier : QueryableValue<SponsorsTier>
    {
        internal SponsorsTier(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// SponsorsTier information only visible to users that can administer the associated Sponsors listing.
        /// </summary>
        public SponsorsTierAdminInfo AdminInfo => this.CreateProperty(x => x.AdminInfo, Octokit.GraphQL.Model.SponsorsTierAdminInfo.Create);

        /// <summary>
        /// Get a different tier for this tier's maintainer that is at the same frequency as this tier but with an equal or lesser cost. Returns the published tier with the monthly price closest to this tier's without going over.
        /// </summary>
        public SponsorsTier ClosestLesserValueTier => this.CreateProperty(x => x.ClosestLesserValueTier, Octokit.GraphQL.Model.SponsorsTier.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The description of the tier.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The tier description rendered to HTML
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// The Node ID of the SponsorsTier object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether this tier was chosen at checkout time by the sponsor rather than defined ahead of time by the maintainer who manages the Sponsors listing.
        /// </summary>
        public bool IsCustomAmount { get; }

        /// <summary>
        /// Whether this tier is only for use with one-time sponsorships.
        /// </summary>
        public bool IsOneTime { get; }

        /// <summary>
        /// How much this tier costs per month in cents.
        /// </summary>
        public int MonthlyPriceInCents { get; }

        /// <summary>
        /// How much this tier costs per month in USD.
        /// </summary>
        public int MonthlyPriceInDollars { get; }

        /// <summary>
        /// The name of the tier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The sponsors listing that this tier belongs to.
        /// </summary>
        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static SponsorsTier Create(Expression expression)
        {
            return new SponsorsTier(expression);
        }
    }
}