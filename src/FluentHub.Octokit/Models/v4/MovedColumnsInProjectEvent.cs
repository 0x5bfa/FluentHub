// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a 'moved_columns_in_project' event on a given issue or pull request.
	/// </summary>
	public class MovedColumnsInProjectEvent
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
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the MovedColumnsInProjectEvent object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Column name the issue or pull request was moved from.
		/// </summary>
		public string PreviousProjectColumnName { get; set; }

		/// <summary>
		/// Project referenced by event.
		/// </summary>
		public Project Project { get; set; }

		/// <summary>
		/// Project card referenced by this project event.
		/// </summary>
		public ProjectCard ProjectCard { get; set; }

		/// <summary>
		/// Column name the issue or pull request was moved to.
		/// </summary>
		public string ProjectColumnName { get; set; }
	}
}
