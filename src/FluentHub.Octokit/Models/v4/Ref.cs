// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git reference.
	/// </summary>
	public class Ref
	{
		/// <summary>
		/// A list of pull requests with this ref as the head ref.
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
		public PullRequestConnection AssociatedPullRequests { get; set; }

		/// <summary>
		/// Branch protection rules for this ref
		/// </summary>
		public BranchProtectionRule BranchProtectionRule { get; set; }

		/// <summary>
		/// Compares the current ref as a base ref to another head ref, if the comparison can be made.
		/// </summary>
		/// <param name="headRef">The head ref to compare against.</param>
		public Comparison Compare { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The ref name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The ref's prefix, such as `refs/heads/` or `refs/tags/`.
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// Branch protection rules that are viewable by non-admins
		/// </summary>
		public RefUpdateRule RefUpdateRule { get; set; }

		/// <summary>
		/// The repository the ref belongs to.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The object the ref points to. Returns null when object does not exist.
		/// </summary>
		public IGitObject Target { get; set; }
	}
}
