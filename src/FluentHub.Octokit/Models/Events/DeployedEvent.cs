namespace FluentHub.Octokit.Models.Events
{
    public class DeployedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public Deployment Deployment { get; set; }

        public string Id { get; set; }

        public PullRequest PullRequest { get; set; }

        public Ref Ref { get; set; }
    }
}
