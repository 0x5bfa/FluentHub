namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A discussion in a repository.
    /// </summary>
    public class Discussion
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; set; }

        /// <summary>
        /// The comment chosen as this discussion's answer, if any.
        /// </summary>
        public DiscussionComment Answer { get; set; }

        /// <summary>
        /// The time when a user chose this discussion's answer, if answered.
        /// </summary>
        public DateTimeOffset? AnswerChosenAt { get; set; }

        /// <summary>
        /// The user who chose this discussion's answer, if answered.
        /// </summary>
        public IActor AnswerChosenBy { get; set; }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author { get; set; }

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; set; }

        /// <summary>
        /// The main text of the discussion post.
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
        /// The category for this discussion.
        /// </summary>
        public DiscussionCategory Category { get; set; }

        /// <summary>
        /// The replies to the discussion.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DiscussionCommentConnection Comments { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

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
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        public LabelConnection Labels { get; set; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; set; }

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// The number identifying this discussion within the repository.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The poll associated with this discussion, if one exists.
        /// </summary>
        public DiscussionPoll Poll { get; set; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; set; }

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
        /// The path for this discussion.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The title of this discussion.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Number of upvotes that this subject has received.
        /// </summary>
        public int UpvoteCount { get; set; }

        /// <summary>
        /// The URL for this discussion.
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
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; set; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; set; }

        /// <summary>
        /// Whether or not the current user can add or remove an upvote on this subject.
        /// </summary>
        public bool ViewerCanUpvote { get; set; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; set; }

        /// <summary>
        /// Whether or not the current user has already upvoted this subject.
        /// </summary>
        public bool ViewerHasUpvoted { get; set; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; set; }
    }
}