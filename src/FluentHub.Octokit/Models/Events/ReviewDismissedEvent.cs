namespace FluentHub.Octokit.Models.Events
{
    public class ReviewDismissedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string DismissalMessage { get; set; }

        public string DismissalMessageHTML { get; set; }

        public string Id { get; set; }

        //public PullRequestReviewState PreviousReviewState { get; set; }

        public PullRequest PullRequest { get; set; }

        public PullRequestCommit PullRequestCommit { get; set; }

        public string ResourcePath { get; set; }

        public PullRequestReview Review { get; set; }

        public string Url { get; set; }
    }
}
