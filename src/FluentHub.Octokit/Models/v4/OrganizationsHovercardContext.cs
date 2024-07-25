// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An organization list hovercard context
	/// </summary>
	public class OrganizationsHovercardContext
	{
		/// <summary>
		/// A string describing this context
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// An octicon to accompany this context
		/// </summary>
		public string Octicon { get; set; }

		/// <summary>
		/// Organizations this user is a member of that are relevant
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the User's organizations.</param>
		public OrganizationConnection RelevantOrganizations { get; set; }

		/// <summary>
		/// The total number of organizations this user is in
		/// </summary>
		public int TotalOrganizationCount { get; set; }
	}
}
