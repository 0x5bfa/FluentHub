// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a comment on an Gist.
	/// </summary>
	public class GistComment
	{
		/// <summary>
		/// The actor who authored the comment.
		/// </summary>
		public IActor Author { get; set; }

		/// <summary>
		/// Author's association with the gist.
		/// </summary>
		public CommentAuthorAssociation AuthorAssociation { get; set; }

		/// <summary>
		/// Identifies the comment body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The body rendered to HTML.
		/// </summary>
		public string BodyHTML { get; set; }

		/// <summary>
		/// The body rendered to text.
		/// </summary>
		public string BodyText { get; set; }

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

		/// <summary>
		/// The associated gist.
		/// </summary>
		public Gist Gist { get; set; }

		/// <summary>
		/// The Node ID of the GistComment object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Check if this comment was edited and includes an edit with the creation data
		/// </summary>
		public bool IncludesCreatedEdit { get; set; }

		/// <summary>
		/// Returns whether or not a comment has been minimized.
		/// </summary>
		public bool IsMinimized { get; set; }

		/// <summary>
		/// The moment the editor made the last edit
		/// </summary>
		public DateTimeOffset? LastEditedAt { get; set; }

		/// <summary>
		/// Humanized string of "The moment the editor made the last edit"
		/// <summary>
		public string LastEditedAtHumanized { get; set; }

		/// <summary>
		/// Returns why the comment was minimized. One of `abuse`, `off-topic`, `outdated`, `resolved`, `duplicate` and `spam`. Note that the case and formatting of these values differs from the inputs to the `MinimizeComment` mutation.
		/// </summary>
		public string MinimizedReason { get; set; }

		/// <summary>
		/// Identifies when the comment was published at.
		/// </summary>
		public DateTimeOffset? PublishedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the comment was published at."
		/// <summary>
		public string PublishedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

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
		/// Check if the current viewer can minimize this object.
		/// </summary>
		public bool ViewerCanMinimize { get; set; }

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
