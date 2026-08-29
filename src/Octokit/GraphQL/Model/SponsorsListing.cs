namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Sponsors listing.
    /// </summary>
    public class SponsorsListing : QueryableValue<SponsorsListing>
    {
        internal SponsorsListing(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The current goal the maintainer is trying to reach with GitHub Sponsors, if any.
        /// </summary>
        public SponsorsGoal ActiveGoal => this.CreateProperty(x => x.ActiveGoal, Octokit.GraphQL.Model.SponsorsGoal.Create);

        /// <summary>
        /// The Stripe Connect account currently in use for payouts for this Sponsors listing, if any. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public StripeConnectAccount ActiveStripeConnectAccount => this.CreateProperty(x => x.ActiveStripeConnectAccount, Octokit.GraphQL.Model.StripeConnectAccount.Create);

        /// <summary>
        /// The name of the country or region with the maintainer's bank account or fiscal host. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string BillingCountryOrRegion { get; }

        /// <summary>
        /// The email address used by GitHub to contact the sponsorable about their GitHub Sponsors profile. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string ContactEmailAddress { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The HTTP path for the Sponsors dashboard for this Sponsors listing.
        /// </summary>
        public string DashboardResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the Sponsors dashboard for this Sponsors listing.
        /// </summary>
        public string DashboardUrl { get; }

        /// <summary>
        /// The records featured on the GitHub Sponsors profile.
        /// </summary>
        /// <param name="featureableTypes">The types of featured items to return.</param>
        public IQueryableList<SponsorsListingFeaturedItem> FeaturedItems(Arg<IEnumerable<SponsorsListingFeaturedItemFeatureableType>>? featureableTypes = null) => this.CreateMethodCall(x => x.FeaturedItems(featureableTypes));

        /// <summary>
        /// The fiscal host used for payments, if any. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public Organization FiscalHost => this.CreateProperty(x => x.FiscalHost, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The full description of the listing.
        /// </summary>
        public string FullDescription { get; }

        /// <summary>
        /// The full description of the listing rendered to HTML.
        /// </summary>
        public string FullDescriptionHTML { get; }

        /// <summary>
        /// The Node ID of the SponsorsListing object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether this listing is publicly visible.
        /// </summary>
        public bool IsPublic { get; }

        /// <summary>
        /// The listing's full name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A future date on which this listing is eligible to receive a payout.
        /// </summary>
        public string NextPayoutDate { get; }

        /// <summary>
        /// The name of the country or region where the maintainer resides. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string ResidenceCountryOrRegion { get; }

        /// <summary>
        /// The HTTP path for this Sponsors listing.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The short description of the listing.
        /// </summary>
        public string ShortDescription { get; }

        /// <summary>
        /// The short name of the listing.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The entity this listing represents who can be sponsored on GitHub Sponsors.
        /// </summary>
        public ISponsorable Sponsorable => this.CreateProperty(x => x.Sponsorable, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        /// <summary>
        /// The tiers for this GitHub Sponsors profile.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeUnpublished">Whether to include tiers that aren't published. Only admins of the Sponsors listing can see draft tiers. Only admins of the Sponsors listing and viewers who are currently sponsoring on a retired tier can see those retired tiers. Defaults to including only published tiers, which are visible to anyone who can see the GitHub Sponsors profile.</param>
        /// <param name="orderBy">Ordering options for Sponsors tiers returned from the connection.</param>
        public SponsorsTierConnection Tiers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includeUnpublished = null, Arg<SponsorsTierOrder>? orderBy = null) => this.CreateMethodCall(x => x.Tiers(first, after, last, before, includeUnpublished, orderBy), Octokit.GraphQL.Model.SponsorsTierConnection.Create);

        /// <summary>
        /// The HTTP URL for this Sponsors listing.
        /// </summary>
        public string Url { get; }

        internal static SponsorsListing Create(Expression expression)
        {
            return new SponsorsListing(expression);
        }
    }
}