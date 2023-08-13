// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The user's description of what they're currently doing.
	/// </summary>
	public class UserStatus
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
		/// An emoji summarizing the user's status.
		/// </summary>
		public string Emoji { get; set; }

		/// <summary>
		/// The status emoji as HTML.
		/// </summary>
		public string EmojiHTML { get; set; }

		/// <summary>
		/// If set, the status will not be shown after this date.
		/// </summary>
		public DateTimeOffset? ExpiresAt { get; set; }

		/// <summary>
		/// Humanized string of "If set, the status will not be shown after this date."
		/// <summary>
		public string ExpiresAtHumanized { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether this status indicates the user is not fully available on GitHub.
		/// </summary>
		public bool IndicatesLimitedAvailability { get; set; }

		/// <summary>
		/// A brief message describing what the user is doing.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The organization whose members can see this status. If null, this status is publicly visible.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The user who has this status.
		/// </summary>
		public User User { get; set; }
	}
}
