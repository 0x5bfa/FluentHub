namespace FluentHub.Octokit.Models.Events
{
    public class UnlabeledEvent
    {
        public Actor Actor { get; set; }

        public Label Label { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
