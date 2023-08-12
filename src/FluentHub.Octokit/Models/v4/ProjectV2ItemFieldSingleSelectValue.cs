// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The value of a single select field in a Project item.
	/// </summary>
	public class ProjectV2ItemFieldSingleSelectValue
	{
		/// <summary>
		/// The color applied to the selected single-select option.
		/// </summary>
		public ProjectV2SingleSelectFieldOptionColor Color { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The actor who created the item.
		/// </summary>
		public IActor Creator { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// A plain-text description of the selected single-select option, such as what the option means.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The description of the selected single-select option, including HTML tags.
		/// </summary>
		public string DescriptionHTML { get; set; }

		/// <summary>
		/// The project field that contains this value.
		/// </summary>
		public ProjectV2FieldConfiguration Field { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The project item that contains this value.
		/// </summary>
		public ProjectV2Item Item { get; set; }

		/// <summary>
		/// The name of the selected single select option.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The html name of the selected single select option.
		/// </summary>
		public string NameHTML { get; set; }

		/// <summary>
		/// The id of the selected single select option.
		/// </summary>
		public string OptionId { get; set; }

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
