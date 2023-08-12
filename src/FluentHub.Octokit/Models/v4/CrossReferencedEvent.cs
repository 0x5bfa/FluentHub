// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a mention made by one issue or pull request to another.
	/// </summary>
	public class CrossReferencedEvent
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

		public ID Id { get; set; }

		/// <summary>
		/// Reference originated in a different repository.
		/// </summary>
		public bool IsCrossRepository { get; set; }

		/// <summary>
		/// Identifies when the reference was made.
		/// </summary>
		public DateTimeOffset ReferencedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the reference was made."
		/// <summary>
		public string ReferencedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP path for this pull request.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Issue or pull request that made the reference.
		/// </summary>
		public ReferencedSubject Source { get; set; }

		/// <summary>
		/// Issue or pull request to which the reference was made.
		/// </summary>
		public ReferencedSubject Target { get; set; }

		/// <summary>
		/// The HTTP URL for this pull request.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Checks if the target will be closed when the source is merged.
		/// </summary>
		public bool WillCloseTarget { get; set; }
	}
}
