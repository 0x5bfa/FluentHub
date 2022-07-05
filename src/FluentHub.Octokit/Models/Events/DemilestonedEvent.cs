namespace FluentHub.Octokit.Models.Events
{
    public class DemilestonedEvent
    {
        public Actor Actor { get; set; }

        public string MilestoneTitle { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
