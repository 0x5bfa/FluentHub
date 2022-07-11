namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entities that can be sponsored through GitHub Sponsors
    /// </summary>
    public interface ISponsorable
    {
        /// <summary>
        /// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
        /// </summary>
        int EstimatedNextSponsorsPayoutInCents { get; set; }

        /// <summary>
        /// True if this user/organization has a GitHub Sponsors listing.
        /// </summary>
        bool HasSponsorsListing { get; set; }

        /// <summary>
        /// Check if the given account is sponsoring this user/organization.
        /// </summary>
        /// <param name="accountLogin">The target account's login.</param>
        bool IsSponsoredBy { get; set; }

        /// <summary>
        /// True if the viewer is sponsored by this user/organization.
        /// </summary>
        bool IsSponsoringViewer { get; set; }

        /// <summary>
        /// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
        /// </summary>
        int MonthlyEstimatedSponsorsIncomeInCents { get; set; }

        /// <summary>
        /// List of users and organizations this entity is sponsoring.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the users and organizations returned from the connection.</param>
        SponsorConnection Sponsoring { get; set; }

        /// <summary>
        /// List of sponsors for this user or organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsors returned from the connection.</param>
        /// <param name="tierId">If given, will filter for sponsors at the given tier. Will only return sponsors whose tier the viewer is permitted to see.</param>
        SponsorConnection Sponsors { get; set; }

        /// <summary>
        /// Events involving this sponsorable, such as new sponsorships.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for activity returned from the connection.</param>
        /// <param name="period">Filter activities returned to only those that occurred in a given time range.</param>
        SponsorsActivityConnection SponsorsActivities { get; set; }

        /// <summary>
        /// The GitHub Sponsors listing for this user or organization.
        /// </summary>
        SponsorsListing SponsorsListing { get; set; }

        /// <summary>
        /// The sponsorship from the viewer to this user/organization; that is, the sponsorship where you're the sponsor. Only returns a sponsorship if it is active.
        /// </summary>
        Sponsorship SponsorshipForViewerAsSponsor { get; set; }

        /// <summary>
        /// The sponsorship from this user/organization to the viewer; that is, the sponsorship you're receiving. Only returns a sponsorship if it is active.
        /// </summary>
        Sponsorship SponsorshipForViewerAsSponsorable { get; set; }

        /// <summary>
        /// List of sponsorship updates sent from this sponsorable to sponsors.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorship updates returned from the connection.</param>
        SponsorshipNewsletterConnection SponsorshipNewsletters { get; set; }

        /// <summary>
        /// This object's sponsorships as the maintainer.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includePrivate">Whether or not to include private sponsorships in the result set</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        SponsorshipConnection SponsorshipsAsMaintainer { get; set; }

        /// <summary>
        /// This object's sponsorships as the sponsor.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        SponsorshipConnection SponsorshipsAsSponsor { get; set; }

        /// <summary>
        /// Whether or not the viewer is able to sponsor this user/organization.
        /// </summary>
        bool ViewerCanSponsor { get; set; }

        /// <summary>
        /// True if the viewer is sponsoring this user/organization.
        /// </summary>
        bool ViewerIsSponsoring { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubISponsorable
    {
        

        public int EstimatedNextSponsorsPayoutInCents { get; set; }

        public bool HasSponsorsListing { get; set; }

        public bool IsSponsoredBy { get; set; }

        public bool IsSponsoringViewer { get; set; }

        public int MonthlyEstimatedSponsorsIncomeInCents { get; set; }

        public SponsorConnection Sponsoring { get; set; }

        public SponsorConnection Sponsors { get; set; }

        public SponsorsActivityConnection SponsorsActivities { get; set; }

        public SponsorsListing SponsorsListing { get; set; }

        public Sponsorship SponsorshipForViewerAsSponsor { get; set; }

        public Sponsorship SponsorshipForViewerAsSponsorable { get; set; }

        public SponsorshipNewsletterConnection SponsorshipNewsletters { get; set; }

        public SponsorshipConnection SponsorshipsAsMaintainer { get; set; }

        public SponsorshipConnection SponsorshipsAsSponsor { get; set; }

        public bool ViewerCanSponsor { get; set; }

        public bool ViewerIsSponsoring { get; set; }
    }
}