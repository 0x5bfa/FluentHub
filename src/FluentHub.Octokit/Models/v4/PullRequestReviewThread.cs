// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A threaded list of comments for a given pull request.
	/// </summary>
	public class PullRequestReviewThread
	{
		/// <summary>
		/// A list of pull request comments associated with the thread.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="skip">Skips the first _n_ elements in the list.</param>
		public PullRequestReviewCommentConnection Comments { get; set; }

		/// <summary>
		/// The side of the diff on which this thread was placed.
		/// </summary>
		public DiffSide DiffSide { get; set; }

		/// <summary>
		/// The Node ID of the PullRequestReviewThread object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether or not the thread has been collapsed (resolved)
		/// </summary>
		public bool IsCollapsed { get; set; }

		/// <summary>
		/// Indicates whether this thread was outdated by newer changes.
		/// </summary>
		public bool IsOutdated { get; set; }

		/// <summary>
		/// Whether this thread has been resolved
		/// </summary>
		public bool IsResolved { get; set; }

		/// <summary>
		/// The line in the file to which this thread refers
		/// </summary>
		public int? Line { get; set; }

		/// <summary>
		/// The original line in the file to which this thread refers.
		/// </summary>
		public int? OriginalLine { get; set; }

		/// <summary>
		/// The original start line in the file to which this thread refers (multi-line only).
		/// </summary>
		public int? OriginalStartLine { get; set; }

		/// <summary>
		/// Identifies the file path of this thread.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Identifies the pull request associated with this thread.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// Identifies the repository associated with this thread.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The user who resolved this thread
		/// </summary>
		public User ResolvedBy { get; set; }

		/// <summary>
		/// The side of the diff that the first line of the thread starts on (multi-line only)
		/// </summary>
		public DiffSide? StartDiffSide { get; set; }

		/// <summary>
		/// The start line in the file to which this thread refers (multi-line only)
		/// </summary>
		public int? StartLine { get; set; }

		/// <summary>
		/// The level at which the comments in the corresponding thread are targeted, can be a diff line or a file
		/// </summary>
		public PullRequestReviewThreadSubjectType SubjectType { get; set; }

		/// <summary>
		/// Indicates whether the current viewer can reply to this thread.
		/// </summary>
		public bool ViewerCanReply { get; set; }

		/// <summary>
		/// Whether or not the viewer can resolve this thread
		/// </summary>
		public bool ViewerCanResolve { get; set; }

		/// <summary>
		/// Whether or not the viewer can unresolve this thread
		/// </summary>
		public bool ViewerCanUnresolve { get; set; }
	}
}
