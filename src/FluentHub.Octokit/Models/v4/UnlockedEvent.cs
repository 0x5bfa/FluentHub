// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'unlocked' event on a given issue or pull request.
	/// </summary>
	public class UnlockedEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the UnlockedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Object that was unlocked.
		/// </summary>
		public ILockable Lockable { get; set; }
	}
}
