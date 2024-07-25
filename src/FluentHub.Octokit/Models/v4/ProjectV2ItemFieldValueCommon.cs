// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Common fields across different project field value types
	/// </summary>
	public interface IProjectV2ItemFieldValueCommon
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The actor who created the item.
		/// </summary>
		IActor Creator { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		int? DatabaseId { get; set; }

		/// <summary>
		/// The project field that contains this value.
		/// </summary>
		ProjectV2FieldConfiguration Field { get; set; }

		/// <summary>
		/// The Node ID of the ProjectV2ItemFieldValueCommon object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// The project item that contains this value.
		/// </summary>
		ProjectV2Item Item { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		string UpdatedAtHumanized { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class ProjectV2ItemFieldValueCommon : IProjectV2ItemFieldValueCommon
	{
		public DateTimeOffset CreatedAt { get; set; }

		public string CreatedAtHumanized { get; set; }

		public IActor Creator { get; set; }

		public int? DatabaseId { get; set; }

		public ProjectV2FieldConfiguration Field { get; set; }

		public ID Id { get; set; }

		public ProjectV2Item Item { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		public string UpdatedAtHumanized { get; set; }
	}
}

