namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A record that is promoted on a GitHub Sponsors profile.
    /// </summary>
    public class SponsorsListingFeaturedItem : QueryableValue<SponsorsListingFeaturedItem>
    {
        internal SponsorsListingFeaturedItem(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Will either be a description from the sponsorable maintainer about why they featured this item, or the item's description itself, such as a user's bio from their GitHub profile page.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The record that is featured on the GitHub Sponsors profile.
        /// </summary>
        public SponsorsListingFeatureableItem Featureable => this.CreateProperty(x => x.Featureable, Octokit.GraphQL.Model.SponsorsListingFeatureableItem.Create);

        /// <summary>
        /// The Node ID of the SponsorsListingFeaturedItem object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The position of this featured item on the GitHub Sponsors profile with a lower position indicating higher precedence. Starts at 1.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// The GitHub Sponsors profile that features this record.
        /// </summary>
        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static SponsorsListingFeaturedItem Create(Expression expression)
        {
            return new SponsorsListingFeaturedItem(expression);
        }
    }
}