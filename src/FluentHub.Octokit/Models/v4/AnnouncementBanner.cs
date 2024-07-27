// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents an announcement banner.
	/// </summary>
	public interface IAnnouncementBanner
	{
		/// <summary>
		/// The text of the announcement
		/// </summary>
		string Announcement { get; set; }

		/// <summary>
		/// The date the announcement was created
		/// </summary>
		DateTimeOffset? AnnouncementCreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "The date the announcement was created"
		/// <summary>
		string AnnouncementCreatedAtHumanized { get; set; }

		/// <summary>
		/// The expiration date of the announcement, if any
		/// </summary>
		DateTimeOffset? AnnouncementExpiresAt { get; set; }

		/// <summary>
		/// Humanized string of "The expiration date of the announcement, if any"
		/// <summary>
		string AnnouncementExpiresAtHumanized { get; set; }

		/// <summary>
		/// Whether the announcement can be dismissed by the user
		/// </summary>
		bool? AnnouncementUserDismissible { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class AnnouncementBanner : IAnnouncementBanner
	{
		public string Announcement { get; set; }

		public DateTimeOffset? AnnouncementCreatedAt { get; set; }

		public string AnnouncementCreatedAtHumanized { get; set; }

		public DateTimeOffset? AnnouncementExpiresAt { get; set; }

		public string AnnouncementExpiresAtHumanized { get; set; }

		public bool? AnnouncementUserDismissible { get; set; }
	}
}

