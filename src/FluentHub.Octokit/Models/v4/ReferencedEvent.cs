// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'referenced' event on a given `ReferencedSubject`.
	/// </summary>
	public class ReferencedEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Identifies the commit associated with the 'referenced' event.
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// Identifies the repository associated with the 'referenced' event.
		/// </summary>
		public Repository CommitRepository { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the ReferencedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Reference originated in a different repository.
		/// </summary>
		public bool IsCrossRepository { get; set; }

		/// <summary>
		/// Checks if the commit message itself references the subject. Can be false in the case of a commit comment reference.
		/// </summary>
		public bool IsDirectReference { get; set; }

		/// <summary>
		/// Object referenced by event.
		/// </summary>
		public ReferencedSubject Subject { get; set; }
	}
}
