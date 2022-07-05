namespace FluentHub.Octokit.Models.Events
{
    public class AutomaticBaseChangeSucceededEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public string NewBase { get; set; }

        public string OldBase { get; set; }
    }
}
