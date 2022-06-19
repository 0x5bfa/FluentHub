namespace FluentHub.Octokit.Models.Events
{
    public class ReferencedEvent
    {
        public Actor Actor { get; set; }

        public Commit Commit { get; set; }

        public Repository CommitRepository { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public bool IsCrossRepository { get; set; }

        public bool IsDirectReference { get; set; }
    }
}
