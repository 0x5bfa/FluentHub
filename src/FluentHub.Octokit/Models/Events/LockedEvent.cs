namespace FluentHub.Octokit.Models.Events
{
    public class LockedEvent
    {
        public Actor Actor { get; set; }

        public OctokitGraphQLModel.LockReason? LockReason { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
