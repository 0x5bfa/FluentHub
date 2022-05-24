using FluentHub.Octokit.Models.ActivityPayloads;

namespace FluentHub.Octokit.Models
{
    public class Activity
    {
        // Payloads
        public CheckRunEventPayload CheckRunEventPayload { get; set; }
        public CheckSuiteEventPayload CheckSuiteEventPayload { get; set; }
        public CommitCommentPayload CommitCommentPayload { get; set; }
        public CreateEventPayload CreateEventPayload { get; set; }
        public DeleteEventPayload DeleteEventPayload { get; set; }
        public ForkEventPayload ForkEventPayload { get; set; }
        public IssueCommentPayload IssueCommentPayload { get; set; }
        public IssueEventPayload IssueEventPayload { get; set; }
        public PullRequestEventPayload PullRequestEventPayload { get; set; }
        public PullRequestReviewEventPayload PullRequestReviewEventPayload { get; set; }
        public PullRequestCommentPayload PullRequestCommentPayload { get; set; }
        public PushEventPayload PushEventPayload { get; set; }
        public ReleaseEventPayload ReleaseEventPayload { get; set; }
        public StatusEventPayload StatusEventPayload { get; set; }
        public StarredEventPayload StarredEventPayload { get; set; }

        public object Payload { get; set; }

        public string PayloadType { get; set; }

        public User Actor { get; set; }
        public Repository Repository{ get; set; }
        public Organization Organization { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
