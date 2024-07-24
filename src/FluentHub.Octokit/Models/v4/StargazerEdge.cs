// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a user that's starred a repository.
	/// </summary>
	public class StargazerEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		public User Node { get; set; }

		/// <summary>
		/// Identifies when the item was starred.
		/// </summary>
		public DateTimeOffset StarredAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the item was starred."
		/// <summary>
		public string StarredAtHumanized { get; set; }
	}
}
