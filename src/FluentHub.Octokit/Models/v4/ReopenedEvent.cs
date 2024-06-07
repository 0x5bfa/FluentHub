// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'reopened' event on any `Closable`.
	/// </summary>
	public class ReopenedEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Object that was reopened.
		/// </summary>
		public IClosable Closable { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the ReopenedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The reason the issue state was changed to open.
		/// </summary>
		public IssueStateReason? StateReason { get; set; }
	}
}
