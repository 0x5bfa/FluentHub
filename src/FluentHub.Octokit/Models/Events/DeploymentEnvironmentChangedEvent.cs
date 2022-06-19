namespace FluentHub.Octokit.Models.Events
{
    public class DeploymentEnvironmentChangedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public DeploymentStatus DeploymentStatus { get; set; }
    }
}
