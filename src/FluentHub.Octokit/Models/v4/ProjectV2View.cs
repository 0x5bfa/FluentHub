// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A view within a ProjectV2.
	/// </summary>
	public class ProjectV2View
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
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The view's visible fields.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConfigurationConnection Fields { get; set; }

		/// <summary>
		/// The project view's filter.
		/// </summary>
		public string Filter { get; set; }

		/// <summary>
		/// The view's group-by field.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConnection GroupBy { get; set; }

		/// <summary>
		/// The view's group-by field.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConfigurationConnection GroupByFields { get; set; }

		/// <summary>
		/// The Node ID of the ProjectV2View object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The project view's layout.
		/// </summary>
		public ProjectV2ViewLayout Layout { get; set; }

		/// <summary>
		/// The project view's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The project view's number.
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// The project that contains this view.
		/// </summary>
		public ProjectV2 Project { get; set; }

		/// <summary>
		/// The view's sort-by config.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public ProjectV2SortByConnection SortBy { get; set; }

		/// <summary>
		/// The view's sort-by config.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public ProjectV2SortByFieldConnection SortByFields { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The view's vertical-group-by field.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConnection VerticalGroupBy { get; set; }

		/// <summary>
		/// The view's vertical-group-by field.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConfigurationConnection VerticalGroupByFields { get; set; }

		/// <summary>
		/// The view's visible fields.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
		public ProjectV2FieldConnection VisibleFields { get; set; }
	}
}
