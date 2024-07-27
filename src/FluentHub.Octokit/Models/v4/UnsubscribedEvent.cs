// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'unsubscribed' event on a given `Subscribable`.
	/// </summary>
	public class UnsubscribedEvent
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
		/// The Node ID of the UnsubscribedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Object referenced by event.
		/// </summary>
		public ISubscribable Subscribable { get; set; }
	}
}
