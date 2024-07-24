// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A record that is promoted on a GitHub Sponsors profile.
	/// </summary>
	public class SponsorsListingFeaturedItem
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Will either be a description from the sponsorable maintainer about why they featured this item, or the item's description itself, such as a user's bio from their GitHub profile page.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The record that is featured on the GitHub Sponsors profile.
		/// </summary>
		public SponsorsListingFeatureableItem Featureable { get; set; }

		/// <summary>
		/// The Node ID of the SponsorsListingFeaturedItem object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The position of this featured item on the GitHub Sponsors profile with a lower position indicating higher precedence. Starts at 1.
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// The GitHub Sponsors profile that features this record.
		/// </summary>
		public SponsorsListing SponsorsListing { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
