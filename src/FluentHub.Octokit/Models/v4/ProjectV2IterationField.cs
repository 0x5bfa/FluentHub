// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An iteration field inside a project.
	/// </summary>
	public class ProjectV2IterationField
	{
		/// <summary>
		/// Iteration configuration settings
		/// </summary>
		public ProjectV2IterationFieldConfiguration Configuration { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The field's type.
		/// </summary>
		public ProjectV2FieldType DataType { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the ProjectV2IterationField object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The project field's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The project that contains this field.
		/// </summary>
		public ProjectV2 Project { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
