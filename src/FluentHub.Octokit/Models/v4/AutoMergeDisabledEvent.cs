// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'auto_merge_disabled' event on a given pull request.
	/// </summary>
	public class AutoMergeDisabledEvent
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
		/// The user who disabled auto-merge for this Pull Request
		/// </summary>
		public User Disabler { get; set; }

		/// <summary>
		/// The Node ID of the AutoMergeDisabledEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// PullRequest referenced by event
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// The reason auto-merge was disabled
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// The reason_code relating to why auto-merge was disabled
		/// </summary>
		public string ReasonCode { get; set; }
	}
}
