// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a user who is a member of a team.
	/// </summary>
	public class TeamMemberEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The HTTP path to the organization's member access page.
		/// </summary>
		public string MemberAccessResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL to the organization's member access page.
		/// </summary>
		public string MemberAccessUrl { get; set; }

		public User Node { get; set; }

		/// <summary>
		/// The role the member has on the team.
		/// </summary>
		public TeamMemberRole Role { get; set; }
	}
}
