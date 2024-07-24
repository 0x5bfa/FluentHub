// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// This aggregates commits made by a user within one repository.
	/// </summary>
	public class CommitContributionsByRepository
	{
		/// <summary>
		/// The commit contributions, each representing a day.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for commit contributions returned from the connection.</param>
		public CreatedCommitContributionConnection Contributions { get; set; }

		/// <summary>
		/// The repository in which the commits were made.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for the user's commits to the repository in this time range.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the user's commits to the repository in this time range.
		/// </summary>
		public string Url { get; set; }
	}
}
