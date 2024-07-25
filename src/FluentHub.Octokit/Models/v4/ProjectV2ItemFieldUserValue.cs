// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The value of a user field in a Project item.
	/// </summary>
	public class ProjectV2ItemFieldUserValue
	{
		/// <summary>
		/// The field that contains this value.
		/// </summary>
		public ProjectV2FieldConfiguration Field { get; set; }

		/// <summary>
		/// The users for this field
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserConnection Users { get; set; }
	}
}
