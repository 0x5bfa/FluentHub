// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'renamed' event on a given issue or pull request
	/// </summary>
	public class RenamedTitleEvent
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
		/// Identifies the current title of the issue or pull request.
		/// </summary>
		public string CurrentTitle { get; set; }

		/// <summary>
		/// The Node ID of the RenamedTitleEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the previous title of the issue or pull request.
		/// </summary>
		public string PreviousTitle { get; set; }

		/// <summary>
		/// Subject that was renamed.
		/// </summary>
		public RenamedTitleSubject Subject { get; set; }
	}
}
