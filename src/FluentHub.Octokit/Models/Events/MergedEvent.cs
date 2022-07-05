namespace FluentHub.Octokit.Models.Events
{
    public class MergedEvent
    {
        public Actor Actor { get; set; }

        public Commit Commit { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public Ref MergeRef { get; set; }

        public string MergeRefName { get; set; }

        public string Url { get; set; }
    }
}
