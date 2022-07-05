namespace FluentHub.Octokit.Models.Events
{
    public class TransferredEvent
    {
        public Actor Actor { get; set; }

        public Repository FromRepository { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
