namespace FluentHub.Octokit.Models.Events
{
    public class BaseRefChangedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string CurrentRefName { get; set; }

        public string Id { get; set; }

        public string PreviousRefName { get; set; }
    }
}
