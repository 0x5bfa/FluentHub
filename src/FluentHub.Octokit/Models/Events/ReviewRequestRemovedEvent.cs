namespace FluentHub.Octokit.Models.Events
{
    public class ReviewRequestRemovedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public RequestedReviewer RequestedReviewer { get; set; }
    }
}
