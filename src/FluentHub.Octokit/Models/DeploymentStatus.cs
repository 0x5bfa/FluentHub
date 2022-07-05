namespace FluentHub.Octokit.Models
{
    public class DeploymentStatus
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public Actor Creator { get; set; }

        public Deployment Deployment { get; set; }

        public string Description { get; set; }

        public string EnvironmentUrl { get; set; }

        public string Id { get; set; }

        public string LogUrl { get; set; }

        //public DeploymentStatusState State { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
