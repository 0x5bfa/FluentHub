// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a commit comment thread part of a pull request.
	/// </summary>
	public class PullRequestCommitCommentThread
	{
		/// <summary>
		/// The comments that exist in this thread.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public CommitCommentConnection Comments { get; set; }

		/// <summary>
		/// The commit the comments were made on.
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// The Node ID of the PullRequestCommitCommentThread object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The file the comments were made on.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// The position in the diff for the commit that the comment was made on.
		/// </summary>
		public int? Position { get; set; }

		/// <summary>
		/// The pull request this commit comment thread belongs to
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// The repository associated with this node.
		/// </summary>
		public Repository Repository { get; set; }
	}
}
