// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'head_ref_deleted' event on a given pull request.
	/// </summary>
	public class HeadRefDeletedEvent
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
		/// Identifies the Ref associated with the `head_ref_deleted` event.
		/// </summary>
		public Ref HeadRef { get; set; }

		/// <summary>
		/// Identifies the name of the Ref associated with the `head_ref_deleted` event.
		/// </summary>
		public string HeadRefName { get; set; }

		/// <summary>
		/// The Node ID of the HeadRefDeletedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// PullRequest referenced by event.
		/// </summary>
		public PullRequest PullRequest { get; set; }
	}
}
