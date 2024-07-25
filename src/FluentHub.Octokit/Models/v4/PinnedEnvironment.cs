// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a pinned environment on a given repository
	/// </summary>
	public class PinnedEnvironment
	{
		/// <summary>
		/// Identifies the date and time when the pinned environment was created
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the pinned environment was created"
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// Identifies the environment associated.
		/// </summary>
		public Environment Environment { get; set; }

		/// <summary>
		/// The Node ID of the PinnedEnvironment object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the position of the pinned environment.
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// The repository that this environment was pinned to.
		/// </summary>
		public Repository Repository { get; set; }
	}
}
