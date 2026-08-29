namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A public description of a Marketplace category.
    /// </summary>
    public class MarketplaceCategory : QueryableValue<MarketplaceCategory>
    {
        internal MarketplaceCategory(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The category's description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The technical description of how apps listed in this category work with GitHub.
        /// </summary>
        public string HowItWorks { get; }

        /// <summary>
        /// The Node ID of the MarketplaceCategory object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The category's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// How many Marketplace listings have this as their primary category.
        /// </summary>
        public int PrimaryListingCount { get; }

        /// <summary>
        /// The HTTP path for this Marketplace category.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// How many Marketplace listings have this as their secondary category.
        /// </summary>
        public int SecondaryListingCount { get; }

        /// <summary>
        /// The short name of the category used in its URL.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The HTTP URL for this Marketplace category.
        /// </summary>
        public string Url { get; }

        internal static MarketplaceCategory Create(Expression expression)
        {
            return new MarketplaceCategory(expression);
        }
    }
}