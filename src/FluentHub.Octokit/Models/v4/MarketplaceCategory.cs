// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A public description of a Marketplace category.
	/// </summary>
	public class MarketplaceCategory
	{
		/// <summary>
		/// The category's description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The technical description of how apps listed in this category work with GitHub.
		/// </summary>
		public string HowItWorks { get; set; }

		/// <summary>
		/// The Node ID of the MarketplaceCategory object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The category's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// How many Marketplace listings have this as their primary category.
		/// </summary>
		public int PrimaryListingCount { get; set; }

		/// <summary>
		/// The HTTP path for this Marketplace category.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// How many Marketplace listings have this as their secondary category.
		/// </summary>
		public int SecondaryListingCount { get; set; }

		/// <summary>
		/// The short name of the category used in its URL.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// The HTTP URL for this Marketplace category.
		/// </summary>
		public string Url { get; set; }
	}
}
