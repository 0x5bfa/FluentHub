// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A single select field inside a project.
	/// </summary>
	public class ProjectV2SingleSelectField
	{
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
		/// The Node ID of the ProjectV2SingleSelectField object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The project field's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Options for the single select field
		/// </summary>
		/// <param name="names">Filter returned options to only those matching these names, case insensitive.</param>
		public List<ProjectV2SingleSelectFieldOption> Options { get; set; }

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
