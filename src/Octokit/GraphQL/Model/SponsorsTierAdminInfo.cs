namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SponsorsTier information only visible to users that can administer the associated Sponsors listing.
    /// </summary>
    public class SponsorsTierAdminInfo : QueryableValue<SponsorsTierAdminInfo>
    {
        internal SponsorsTierAdminInfo(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Indicates whether this tier is still a work in progress by the sponsorable and not yet published to the associated GitHub Sponsors profile. Draft tiers cannot be used for new sponsorships and will not be in use on existing sponsorships. Draft tiers cannot be seen by anyone but the admins of the GitHub Sponsors profile.
        /// </summary>
        public bool IsDraft { get; }

        /// <summary>
        /// Indicates whether this tier is published to the associated GitHub Sponsors profile. Published tiers are visible to anyone who can see the GitHub Sponsors profile, and are available for use in sponsorships if the GitHub Sponsors profile is publicly visible.
        /// </summary>
        public bool IsPublished { get; }

        /// <summary>
        /// Indicates whether this tier has been retired from the associated GitHub Sponsors profile. Retired tiers are no longer shown on the GitHub Sponsors profile and cannot be chosen for new sponsorships. Existing sponsorships may still use retired tiers if the sponsor selected the tier before it was retired.
        /// </summary>
        public bool IsRetired { get; }

        /// <summary>
        /// The sponsorships using this tier.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includePrivate">Whether or not to return private sponsorships using this tier. Defaults to only returning public sponsorships on this tier.</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        public SponsorshipConnection Sponsorships(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.Sponsorships(first, after, last, before, includePrivate, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        internal static SponsorsTierAdminInfo Create(Expression expression)
        {
            return new SponsorsTierAdminInfo(expression);
        }
    }
}