// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A label for categorizing Issues, Pull Requests, Milestones, or Discussions with a given Repository.
	/// </summary>
	public class Label
	{
		/// <summary>
		/// Identifies the label color.
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// Identifies the date and time when the label was created.
		/// </summary>
		public DateTimeOffset? CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the label was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// A brief description of this label.
		/// </summary>
		public string Description { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Indicates whether or not this is a default label.
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// A list of issues associated with this label.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="filterBy">Filtering options for issues returned from the connection.</param>
		/// <param name="labels">A list of label names to filter the pull requests by.</param>
		/// <param name="orderBy">Ordering options for issues returned from the connection.</param>
		/// <param name="states">A list of states to filter the issues by.</param>
		public IssueConnection Issues { get; set; }

		/// <summary>
		/// Identifies the label name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A list of pull requests associated with this label.
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
		public PullRequestConnection PullRequests { get; set; }

		/// <summary>
		/// The repository associated with this label.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this label.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the date and time when the label was last updated.
		/// </summary>
		public DateTimeOffset? UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the label was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this label.
		/// </summary>
		public string Url { get; set; }
	}
}
