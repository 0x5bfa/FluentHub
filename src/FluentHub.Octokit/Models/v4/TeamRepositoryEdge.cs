// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a team repository.
	/// </summary>
	public class TeamRepositoryEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		public Repository Node { get; set; }

		/// <summary>
		/// The permission level the team has on the repository
		/// </summary>
		public RepositoryPermission Permission { get; set; }
	}
}
