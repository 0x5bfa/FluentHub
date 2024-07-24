// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An update sent to sponsors of a user or organization on GitHub Sponsors.
	/// </summary>
	public class SponsorshipNewsletter
	{
		/// <summary>
		/// The author of the newsletter.
		/// </summary>
		public User Author { get; set; }

		/// <summary>
		/// The contents of the newsletter, the message the sponsorable wanted to give.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the SponsorshipNewsletter object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Indicates if the newsletter has been made available to sponsors.
		/// </summary>
		public bool IsPublished { get; set; }

		/// <summary>
		/// The user or organization this newsletter is from.
		/// </summary>
		public ISponsorable Sponsorable { get; set; }

		/// <summary>
		/// The subject of the newsletter, what it's about.
		/// </summary>
		public string Subject { get; set; }

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
