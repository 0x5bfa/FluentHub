namespace FluentHub.Octokit.Models.Events
{
    public class BaseRefDeletedEvent
    {
        public Actor Actor { get; set; }

        public string BaseRefName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }
    }
}
