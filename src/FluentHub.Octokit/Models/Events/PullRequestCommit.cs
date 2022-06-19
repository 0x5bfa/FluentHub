namespace FluentHub.Octokit.Models.Events
{
    public class PullRequestCommit
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public Commit Commit { get; set; }
    }
}
