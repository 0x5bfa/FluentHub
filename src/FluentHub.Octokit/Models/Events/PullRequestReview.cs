namespace FluentHub.Octokit.Models.Events
{
    public class PullRequestReview
    {
        public Actor Actor { get; set; }

        public CommentAuthorAssociation AuthorAssociation { get; set; }

        public bool AuthorCanPushToRepository { get; set; }

        public string Body { get; set; }

        public string BodyHTML { get; set; }

        public string BodyText { get; set; }

        public Commit Commit { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        //public PullRequestReviewCommentConnection Comments { get; set; }

        public bool CreatedViaEmail { get; set; }

        public Actor Editor { get; set; }

        public string Id { get; set; }

        public bool IncludesCreatedEdit { get; set; }

        public DateTimeOffset? LastEditedAt { get; set; }
        public string LastEditedAtHumanized { get; set; }

        //public TeamConnection OnBehalfOf { get; set; };

        public DateTimeOffset? PublishedAt { get; set; }
        public string PublishedAtHumanized { get; set; }

        public PullRequest PullRequest { get; set; }

        //public IQueryableList<ReactionGroup> ReactionGroups { get; set; }

        //public ReactionConnection Reactions { get; set; }

        public Repository Repository { get; set; }

        public string ResourcePath { get; set; }

        //public PullRequestReviewState State { get; set; }

        public DateTimeOffset? SubmittedAt { get; set; }
        public string SubmittedAtHumanized { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }

        public string Url { get; set; }

        //public UserContentEditConnection UserContentEdits { get; set; }

        public bool ViewerCanDelete { get; set; }

        public bool ViewerCanReact { get; set; }

        public bool ViewerCanUpdate { get; set; }

        //public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }

        public bool ViewerDidAuthor { get; set; }
    }
}
