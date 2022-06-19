namespace FluentHub.Octokit.Models.Events
{
    public class AutoMergeDisabledEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public PullRequest PullRequest { get; set; }

        public string Reason { get; set; }

        public string ReasonCode { get; set; }
    }
}
