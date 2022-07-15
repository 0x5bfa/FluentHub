namespace FluentHub.Octokit.Models.v3
{
    public enum NotificationSubjectType
    {
        IssueOpen,

        IssueClosedAsCompleted,

        IssueClosedAsNotPlanned,

        PullRequestOpen,

        PullRequestClosed,

        PullRequestMerged,

        PullRequestDraft,

        Discussion,

        Commit,

        Release,
    }
}
