namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Stripe Connect account for receiving sponsorship funds from GitHub Sponsors.
    /// </summary>
    public class StripeConnectAccount
    {
        /// <summary>
        /// The account number used to identify this Stripe Connect account.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// The name of the country or region of an external account, such as a bank account, tied to the Stripe Connect account. Will only return a value when queried by the maintainer of the associated GitHub Sponsors profile themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string BillingCountryOrRegion { get; set; }

        /// <summary>
        /// The name of the country or region of the Stripe Connect account. Will only return a value when queried by the maintainer of the associated GitHub Sponsors profile themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string CountryOrRegion { get; set; }

        /// <summary>
        /// Whether this Stripe Connect account is currently in use for the associated GitHub Sponsors profile.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The GitHub Sponsors profile associated with this Stripe Connect account.
        /// </summary>
        public SponsorsListing SponsorsListing { get; set; }

        /// <summary>
        /// The URL to access this Stripe Connect account on Stripe's website.
        /// </summary>
        public string StripeDashboardUrl { get; set; }
    }
}