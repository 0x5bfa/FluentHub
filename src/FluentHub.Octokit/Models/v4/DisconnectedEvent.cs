// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'disconnected' event on a given issue or pull request.
	/// </summary>
	public class DisconnectedEvent
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
		/// The Node ID of the DisconnectedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Reference originated in a different repository.
		/// </summary>
		public bool IsCrossRepository { get; set; }

		/// <summary>
		/// Issue or pull request from which the issue was disconnected.
		/// </summary>
		public ReferencedSubject Source { get; set; }

		/// <summary>
		/// Issue or pull request which was disconnected.
		/// </summary>
		public ReferencedSubject Subject { get; set; }
	}
}
