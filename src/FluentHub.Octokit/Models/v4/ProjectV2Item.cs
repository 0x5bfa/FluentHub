// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An item within a Project.
	/// </summary>
	public class ProjectV2Item
	{
		/// <summary>
		/// The content of the referenced draft issue, issue, or pull request
		/// </summary>
		public ProjectV2ItemContent Content { get; set; }

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
		/// The field value of the first project field which matches the 'name' argument that is set on the item.
		/// </summary>
		/// <param name="name">The name of the field to return the field value of</param>
		public ProjectV2ItemFieldValue FieldValueByName { get; set; }

		/// <summary>
		/// The field values that are set on the item.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for project v2 item field values returned from the connection</param>
		public ProjectV2ItemFieldValueConnection FieldValues { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether the item is archived.
		/// </summary>
		public bool IsArchived { get; set; }

		/// <summary>
		/// The project that contains this item.
		/// </summary>
		public ProjectV2 Project { get; set; }

		/// <summary>
		/// The type of the item.
		/// </summary>
		public ProjectV2ItemType Type { get; set; }

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
