// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A suggestion to review a pull request based on a user's commit history and review comments.
	/// </summary>
	public class SuggestedReviewer
	{
		/// <summary>
		/// Is this suggestion based on past commits?
		/// </summary>
		public bool IsAuthor { get; set; }

		/// <summary>
		/// Is this suggestion based on past review comments?
		/// </summary>
		public bool IsCommenter { get; set; }

		/// <summary>
		/// Identifies the user suggested to review the pull request.
		/// </summary>
		public User Reviewer { get; set; }
	}
}
