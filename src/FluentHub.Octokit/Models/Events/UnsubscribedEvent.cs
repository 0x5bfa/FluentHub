namespace FluentHub.Octokit.Models.Events
{
    public class UnsubscribedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
