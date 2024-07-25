// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A single check step.
	/// </summary>
	public class CheckStep
	{
		/// <summary>
		/// Identifies the date and time when the check step was completed.
		/// </summary>
		public DateTimeOffset? CompletedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the check step was completed."
		/// <summary>
		public string CompletedAtHumanized { get; set; }

		/// <summary>
		/// The conclusion of the check step.
		/// </summary>
		public CheckConclusionState? Conclusion { get; set; }

		/// <summary>
		/// A reference for the check step on the integrator's system.
		/// </summary>
		public string ExternalId { get; set; }

		/// <summary>
		/// The step's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The index of the step in the list of steps of the parent check run.
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Number of seconds to completion.
		/// </summary>
		public int? SecondsToCompletion { get; set; }

		/// <summary>
		/// Identifies the date and time when the check step was started.
		/// </summary>
		public DateTimeOffset? StartedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the check step was started."
		/// <summary>
		public string StartedAtHumanized { get; set; }

		/// <summary>
		/// The current status of the check step.
		/// </summary>
		public CheckStatusState Status { get; set; }
	}
}
