// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A User who is an outside collaborator of an enterprise through one or more organizations.
	/// </summary>
	public class EnterpriseOutsideCollaboratorEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public User Node { get; set; }

		/// <summary>
		/// The enterprise organization repositories this user is a member of.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for repositories.</param>
		public EnterpriseRepositoryInfoConnection Repositories { get; set; }
	}
}
