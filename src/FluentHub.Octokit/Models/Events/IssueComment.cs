namespace FluentHub.Octokit.Models.Events
{
    public class IssueComment
    {
        public Actor Author { get; set; }

        public CommentAuthorAssociation AuthorAssociation { get; set; }

        public string Body { get; set; }

        public string BodyHTML { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastEditedAt { get; set; }

        public string MinimizedReason { get; set; }

        public bool IsMinimized { get; set; }

        public List<Reaction> Reactions { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Url { get; set; }

        public bool ViewerCanDelete { get; set; }

        public bool ViewerCanMinimize { get; set; }

        public bool ViewerCanReact { get; set; }

        public bool ViewerCanUpdate { get; set; }

        public bool ViewerDidAuthor { get; set; }
    }
}
