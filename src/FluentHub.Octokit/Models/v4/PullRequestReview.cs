// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A review object for a given pull request.
	/// </summary>
	public class PullRequestReview
	{
		/// <summary>
		/// The actor who authored the comment.
		/// </summary>
		public IActor Author { get; set; }

		/// <summary>
		/// Author's association with the subject of the comment.
		/// </summary>
		public CommentAuthorAssociation AuthorAssociation { get; set; }

		/// <summary>
		/// Indicates whether the author of this review has push access to the repository.
		/// </summary>
		public bool AuthorCanPushToRepository { get; set; }

		/// <summary>
		/// Identifies the pull request review body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The body rendered to HTML.
		/// </summary>
		public string BodyHTML { get; set; }

		/// <summary>
		/// The body of this review rendered as plain text.
		/// </summary>
		public string BodyText { get; set; }

		/// <summary>
		/// A list of review comments for the current pull request review.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public PullRequestReviewCommentConnection Comments { get; set; }

		/// <summary>
		/// Identifies the commit associated with this pull request review.
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Check if this comment was created via an email reply.
		/// </summary>
		public bool CreatedViaEmail { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The actor who edited the comment.
		/// </summary>
		public IActor Editor { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Check if this comment was edited and includes an edit with the creation data
		/// </summary>
		public bool IncludesCreatedEdit { get; set; }

		/// <summary>
		/// The moment the editor made the last edit
		/// </summary>
		public DateTimeOffset? LastEditedAt { get; set; }

		/// <summary>
		/// Humanized string of "The moment the editor made the last edit"
		/// <summary>
		public string LastEditedAtHumanized { get; set; }

		/// <summary>
		/// A list of teams that this review was made on behalf of.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public TeamConnection OnBehalfOf { get; set; }

		/// <summary>
		/// Identifies when the comment was published at.
		/// </summary>
		public DateTimeOffset? PublishedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the comment was published at."
		/// <summary>
		public string PublishedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the pull request associated with this pull request review.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// A list of reactions grouped by content left on the subject.
		/// </summary>
		public List<ReactionGroup> ReactionGroups { get; set; }

		/// <summary>
		/// A list of Reactions left on the Issue.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="content">Allows filtering Reactions by emoji.</param>
		/// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
		public ReactionConnection Reactions { get; set; }

		/// <summary>
		/// The repository associated with this node.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path permalink for this PullRequestReview.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the current state of the pull request review.
		/// </summary>
		public PullRequestReviewState State { get; set; }

		/// <summary>
		/// Identifies when the Pull Request Review was submitted
		/// </summary>
		public DateTimeOffset? SubmittedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the Pull Request Review was submitted"
		/// <summary>
		public string SubmittedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL permalink for this PullRequestReview.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// A list of edits to this content.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserContentEditConnection UserContentEdits { get; set; }

		/// <summary>
		/// Check if the current viewer can delete this object.
		/// </summary>
		public bool ViewerCanDelete { get; set; }

		/// <summary>
		/// Can user react to this subject
		/// </summary>
		public bool ViewerCanReact { get; set; }

		/// <summary>
		/// Check if the current viewer can update this object.
		/// </summary>
		public bool ViewerCanUpdate { get; set; }

		/// <summary>
		/// Reasons why the current viewer can not update this comment.
		/// </summary>
		public List<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }

		/// <summary>
		/// Did the viewer author this comment.
		/// </summary>
		public bool ViewerDidAuthor { get; set; }
	}
}
