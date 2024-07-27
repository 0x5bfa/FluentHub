// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// This aggregates issues opened by a user within one repository.
	/// </summary>
	public class IssueContributionsByRepository
	{
		/// <summary>
		/// The issue contributions.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
		public CreatedIssueContributionConnection Contributions { get; set; }

		/// <summary>
		/// The repository in which the issues were opened.
		/// </summary>
		public Repository Repository { get; set; }
	}
}
