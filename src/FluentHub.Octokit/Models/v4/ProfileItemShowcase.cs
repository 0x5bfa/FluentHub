// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A curatable list of repositories relating to a repository owner, which defaults to showing the most popular repositories they own.
	/// </summary>
	public class ProfileItemShowcase
	{
		/// <summary>
		/// Whether or not the owner has pinned any repositories or gists.
		/// </summary>
		public bool HasPinnedItems { get; set; }

		/// <summary>
		/// The repositories and gists in the showcase. If the profile owner has any pinned items, those will be returned. Otherwise, the profile owner's popular repositories will be returned.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public PinnableItemConnection Items { get; set; }
	}
}
