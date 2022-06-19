namespace FluentHub.Octokit.Models.Events
{
    public class ReopenedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public IssueStateReason StateReason { get; set; }
    }
}
