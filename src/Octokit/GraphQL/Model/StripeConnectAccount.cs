namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Stripe Connect account for receiving sponsorship funds from GitHub Sponsors.
    /// </summary>
    public class StripeConnectAccount : QueryableValue<StripeConnectAccount>
    {
        internal StripeConnectAccount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The account number used to identify this Stripe Connect account.
        /// </summary>
        public string AccountId { get; }

        /// <summary>
        /// The name of the country or region of an external account, such as a bank account, tied to the Stripe Connect account. Will only return a value when queried by the maintainer of the associated GitHub Sponsors profile themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string BillingCountryOrRegion { get; }

        /// <summary>
        /// The name of the country or region of the Stripe Connect account. Will only return a value when queried by the maintainer of the associated GitHub Sponsors profile themselves, or by an admin of the sponsorable organization.
        /// </summary>
        public string CountryOrRegion { get; }

        /// <summary>
        /// Whether this Stripe Connect account is currently in use for the associated GitHub Sponsors profile.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// The GitHub Sponsors profile associated with this Stripe Connect account.
        /// </summary>
        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        /// <summary>
        /// The URL to access this Stripe Connect account on Stripe's website.
        /// </summary>
        public string StripeDashboardUrl { get; }

        internal static StripeConnectAccount Create(Expression expression)
        {
            return new StripeConnectAccount(expression);
        }
    }
}