namespace FluentHub.Octokit.Models.Events
{
    public class RemovedFromProjectEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }
    }
}
