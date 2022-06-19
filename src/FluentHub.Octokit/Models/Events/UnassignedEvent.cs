namespace FluentHub.Octokit.Models.Events
{
    public class UnassignedEvent
    {
        public Actor Actor { get; set; }

        public Assignee Assignee { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
