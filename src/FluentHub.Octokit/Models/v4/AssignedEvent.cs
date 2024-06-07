// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an 'assigned' event on any assignable object.
	/// </summary>
	public class AssignedEvent
	{
		/// <summary>
		/// Identifies the actor who performed the event.
		/// </summary>
		public IActor Actor { get; set; }

		/// <summary>
		/// Identifies the assignable associated with the event.
		/// </summary>
		public IAssignable Assignable { get; set; }

		/// <summary>
		/// Identifies the user or mannequin that was assigned.
		/// </summary>
		public Assignee Assignee { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the AssignedEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the user who was assigned.
		/// </summary>
		[Obsolete(@"Assignees can now be mannequins. Use the `assignee` field instead. Removal on 2020-01-01 UTC.")]
		public User User { get; set; }
	}
}
