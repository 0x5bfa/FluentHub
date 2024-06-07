// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'labeled' event on a given issue or pull request.
	/// </summary>
	public class LabeledEvent
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
		/// The Node ID of the LabeledEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the label associated with the 'labeled' event.
		/// </summary>
		public Label Label { get; set; }

		/// <summary>
		/// Identifies the `Labelable` associated with the event.
		/// </summary>
		public ILabelable Labelable { get; set; }
	}
}
