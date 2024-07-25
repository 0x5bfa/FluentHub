// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A placeholder user for attribution of imported data on GitHub.
	/// </summary>
	public class Mannequin
	{
		/// <summary>
		/// A URL pointing to the GitHub App's public avatar.
		/// </summary>
		/// <param name="size">The size of the resulting square image.</param>
		public string AvatarUrl { get; set; }

		/// <summary>
		/// The user that has claimed the data attributed to this mannequin.
		/// </summary>
		public User Claimant { get; set; }

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
		/// The mannequin's email on the source instance.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The Node ID of the Mannequin object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The username of the actor.
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// The HTML path to this resource.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The URL to this resource.
		/// </summary>
		public string Url { get; set; }
	}
}
