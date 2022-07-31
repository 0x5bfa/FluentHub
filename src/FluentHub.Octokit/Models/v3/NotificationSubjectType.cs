namespace FluentHub.Octokit.Models.v3
{
    public enum NotificationSubjectType
    {
        Issue,

        IssueOpen,

        IssueClosedAsCompleted,

        IssueClosedAsNotPlanned,

        PullRequest,

        PullRequestOpen,

        PullRequestClosed,

        PullRequestMerged,

        PullRequestDraft,

        Discussion,

        Commit,

        Release,
    }
}
