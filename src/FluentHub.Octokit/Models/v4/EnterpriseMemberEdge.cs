// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A User who is a member of an enterprise through one or more organizations.
	/// </summary>
	public class EnterpriseMemberEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public EnterpriseMember Node { get; set; }
	}
}
