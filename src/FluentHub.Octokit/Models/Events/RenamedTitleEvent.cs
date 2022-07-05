namespace FluentHub.Octokit.Models.Events
{
    public class RenamedTitleEvent
    {
        public Actor Actor { get; set; }

        public string CurrentTitle { get; set; }
        public string PreviousTitle { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
