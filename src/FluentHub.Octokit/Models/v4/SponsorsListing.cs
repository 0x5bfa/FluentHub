namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A GitHub Sponsors listing.
    /// </summary>
    public class SponsorsListing
    {
        /// <summary>
        /// The current goal the maintainer is trying to reach with GitHub Sponsors, if any.
        /// </summary>
        public SponsorsGoal ActiveGoal { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// The full description of the listing.
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// The full description of the listing rendered to HTML.
        /// </summary>
        public string FullDescriptionHTML { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether this listing is publicly visible.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// The listing's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A future date on which this listing is eligible to receive a payout.
        /// </summary>
        public string NextPayoutDate { get; set; }

        /// <summary>
        /// The short description of the listing.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The short name of the listing.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// The entity this listing represents who can be sponsored on GitHub Sponsors.
        /// </summary>
        public ISponsorable Sponsorable { get; set; }

        /// <summary>
        /// The published tiers for this GitHub Sponsors listing.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for Sponsors tiers returned from the connection.</param>
        public SponsorsTierConnection Tiers { get; set; }
    }
}