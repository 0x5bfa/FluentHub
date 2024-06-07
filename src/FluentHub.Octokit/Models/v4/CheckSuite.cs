// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A check suite.
	/// </summary>
	public class CheckSuite
	{
		/// <summary>
		/// The GitHub App which created this check suite.
		/// </summary>
		public App App { get; set; }

		/// <summary>
		/// The name of the branch for this check suite.
		/// </summary>
		public Ref Branch { get; set; }

		/// <summary>
		/// The check runs associated with a check suite.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="filterBy">Filters the check runs by this type.</param>
		public CheckRunConnection CheckRuns { get; set; }

		/// <summary>
		/// The commit for this check suite
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// The conclusion of this check suite.
		/// </summary>
		public CheckConclusionState? Conclusion { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The user who triggered the check suite.
		/// </summary>
		public User Creator { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the CheckSuite object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// A list of open pull requests matching the check suite.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
		/// <param name="headRefName">The head ref name to filter the pull requests by.</param>
		/// <param name="labels">A list of label names to filter the pull requests by.</param>
		/// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
		/// <param name="states">A list of states to filter the pull requests by.</param>
		public PullRequestConnection MatchingPullRequests { get; set; }

		/// <summary>
		/// The push that triggered this check suite.
		/// </summary>
		public Push Push { get; set; }

		/// <summary>
		/// The repository associated with this check suite.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this check suite
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The status of this check suite.
		/// </summary>
		public CheckStatusState Status { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this check suite
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The workflow run associated with this check suite.
		/// </summary>
		public WorkflowRun WorkflowRun { get; set; }
	}
}
