// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub App.
	/// </summary>
	public class App
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
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The description of the app.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The Node ID of the App object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The IP addresses of the app.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for IP allow list entries returned.</param>
		public IpAllowListEntryConnection IpAllowListEntries { get; set; }

		/// <summary>
		/// The hex color code, without the leading '#', for the logo background.
		/// </summary>
		public string LogoBackgroundColor { get; set; }

		/// <summary>
		/// A URL pointing to the app's logo.
		/// </summary>
		/// <param name="size">The size of the resulting image.</param>
		public string LogoUrl { get; set; }

		/// <summary>
		/// The name of the app.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A slug based on the name of the app for use in URLs.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The URL to the app's homepage.
		/// </summary>
		public string Url { get; set; }
	}
}
