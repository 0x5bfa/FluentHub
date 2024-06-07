// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A user-curated list of repositories
	/// </summary>
	public class UserList
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
		/// The description of this list
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The Node ID of the UserList object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether or not this list is private
		/// </summary>
		public bool IsPrivate { get; set; }

		/// <summary>
		/// The items associated with this list
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserListItemsConnection Items { get; set; }

		/// <summary>
		/// The date and time at which this list was created or last had items added to it
		/// </summary>
		public DateTimeOffset LastAddedAt { get; set; }

		/// <summary>
		/// Humanized string of "The date and time at which this list was created or last had items added to it"
		/// <summary>
		public string LastAddedAtHumanized { get; set; }

		/// <summary>
		/// The name of this list
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The slug of this list
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The user to which this list belongs
		/// </summary>
		public User User { get; set; }
	}
}
