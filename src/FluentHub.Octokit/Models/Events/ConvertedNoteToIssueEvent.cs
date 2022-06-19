namespace FluentHub.Octokit.Models.Events
{
    public class ConvertedNoteToIssueEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public Discussion Discussion { get; set; }

        public string Id { get; set; }
    }
}
