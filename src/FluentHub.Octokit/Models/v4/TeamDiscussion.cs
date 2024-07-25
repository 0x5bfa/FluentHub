// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A team discussion.
	/// </summary>
	public class TeamDiscussion
	{
		/// <summary>
		/// The actor who authored the comment.
		/// </summary>
		public IActor Author { get; set; }

		/// <summary>
		/// Author's association with the discussion's team.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public CommentAuthorAssociation AuthorAssociation { get; set; }

		/// <summary>
		/// The body as Markdown.
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
		/// Identifies the discussion body hash.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public string BodyVersion { get; set; }

		/// <summary>
		/// A list of comments on this discussion.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="fromComment">When provided, filters the connection such that results begin with the comment with this number.</param>
		/// <param name="orderBy">Order for connection</param>
		public TeamDiscussionCommentConnection Comments { get; set; }

		/// <summary>
		/// The HTTP path for discussion comments
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public string CommentsResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for discussion comments
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public string CommentsUrl { get; set; }

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
		/// The Node ID of the TeamDiscussion object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Check if this comment was edited and includes an edit with the creation data
		/// </summary>
		public bool IncludesCreatedEdit { get; set; }

		/// <summary>
		/// Whether or not the discussion is pinned.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public bool IsPinned { get; set; }

		/// <summary>
		/// Whether or not the discussion is only visible to team members and organization owners.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public bool IsPrivate { get; set; }

		/// <summary>
		/// The moment the editor made the last edit
		/// </summary>
		public DateTimeOffset? LastEditedAt { get; set; }

		/// <summary>
		/// Humanized string of "The moment the editor made the last edit"
		/// <summary>
		public string LastEditedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the discussion within its team.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public int Number { get; set; }

		/// <summary>
		/// Identifies when the comment was published at.
		/// </summary>
		public DateTimeOffset? PublishedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies when the comment was published at."
		/// <summary>
		public string PublishedAtHumanized { get; set; }

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
		/// The HTTP path for this discussion
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public string ResourcePath { get; set; }

		/// <summary>
		/// The team that defines the context of this discussion.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public Team Team { get; set; }

		/// <summary>
		/// The title of the discussion
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public string Title { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this discussion
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
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
		/// Whether or not the current viewer can pin this discussion.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public bool ViewerCanPin { get; set; }

		/// <summary>
		/// Can user react to this subject
		/// </summary>
		public bool ViewerCanReact { get; set; }

		/// <summary>
		/// Check if the viewer is able to change their subscription status for the repository.
		/// </summary>
		public bool ViewerCanSubscribe { get; set; }

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

		/// <summary>
		/// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
		/// </summary>
		public SubscriptionState? ViewerSubscription { get; set; }
	}
}
