namespace FluentHub.Octokit.Models.Events
{
    public class CrossReferencedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public bool IsCrossRepository { get; set; }

        public DateTimeOffset ReferencedAt { get; set; }
        public string ReferencedAtHumanized { get; set; }

        public ReferencedSubject Source { get; set; }

        public ReferencedSubject Target { get; set; }

        public string Url { get; set; }

        public bool WillCloseTarget { get; set; }
    }
}
