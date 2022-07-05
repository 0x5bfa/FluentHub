namespace FluentHub.Octokit.Models
{
    public class Deployment
    {
        public Commit Commit { get; set; }

        public string CommitOid { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public Actor Creator { get; set; }

        public int? DatabaseId { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }

        public ID Id { get; set; }

        public string LatestEnvironment { get; set; }

        //public DeploymentStatus LatestStatus { get; set; }

        public string OriginalEnvironment { get; set; }

        public string Payload { get; set; }

        public Ref Ref { get; set; }

        public Repository Repository { get; set; }

        //public DeploymentState? State { get; set; }

        //public DeploymentStatusConnection Statuses { get; set; }

        public string Task { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
