// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an author of a reaction.
	/// </summary>
	public class ReactorEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The author of the reaction.
		/// </summary>
		public Reactor Node { get; set; }

		/// <summary>
		/// The moment when the user made the reaction.
		/// </summary>
		public DateTimeOffset ReactedAt { get; set; }

		/// <summary>
		/// Humanized string of "The moment when the user made the reaction."
		/// <summary>
		public string ReactedAtHumanized { get; set; }
	}
}
