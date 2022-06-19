namespace FluentHub.Octokit.Models.Events
{
    public class UserBlockedEvent
    {
        public Actor Actor { get; set; }

        public UserBlockDuration BlockDuration { get; set; }

        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
