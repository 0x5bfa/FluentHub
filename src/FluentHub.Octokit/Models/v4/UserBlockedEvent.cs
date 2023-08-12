// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'user_blocked' event on a given user.
	/// </summary>
	public class UserBlockedEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Number of days that the user was blocked for.
		/// </summary>
		public UserBlockDuration BlockDuration { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The user who was blocked.
		/// </summary>
		public User Subject { get; set; }
	}
}
