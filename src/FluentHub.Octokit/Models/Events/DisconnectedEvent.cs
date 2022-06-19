namespace FluentHub.Octokit.Models.Events
{
    public class DisconnectedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public bool IsCrossRepository { get; set; }

        public ReferencedSubject Source { get; set; }

        public ReferencedSubject Subject { get; set; }
    }
}
