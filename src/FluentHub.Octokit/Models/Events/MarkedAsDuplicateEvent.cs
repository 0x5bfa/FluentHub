namespace FluentHub.Octokit.Models.Events
{
    public class MarkedAsDuplicateEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public IssueOrPullRequest Duplicate { get; set; }

        public bool IsCrossRepository { get; set; }
    }
}
