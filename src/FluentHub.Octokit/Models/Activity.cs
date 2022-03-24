using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Activity
    {
        // Booleans
        public bool IsCheckRunEvent { get; set; }
        public bool IsCheckSuiteEvent { get; set; }
        public bool IsCommitComment { get; set; }
        public bool IsCreateEvent { get; set; }
        public bool IsDeleteEvent { get; set; }
        public bool IsForkEvent { get; set; }
        public bool IsIssueComment { get; set; }
        public bool IsIssueEvent { get; set; }
        public bool IsPullRequestEvent { get; set; }
        public bool IsPullRequestReviewEvent { get; set; }
        public bool IsPushEvent { get; set; }
        public bool IsReleaseEvent { get; set; }
        public bool IsStatusEvent { get; set; }
        public bool IsWatchEvent { get; set; }
        public bool IsUnknown { get; set; }

        // Payloads
        public global::Octokit.CheckRunEventPayload CheckRunEventPayload { get; set; }
        public global::Octokit.CheckSuiteEventPayload CheckSuiteEventPayload { get; set; }
        public global::Octokit.CommitCommentPayload CommitCommentPayload { get; set; }
        public global::Octokit.CreateEventPayload CreateEventPayload { get; set; }
        public global::Octokit.DeleteEventPayload DeleteEventPayload { get; set; }
        public global::Octokit.ForkEventPayload ForkEventPayload { get; set; }
        public global::Octokit.IssueCommentPayload IssueCommentPayload { get; set; }
        public global::Octokit.IssueEventPayload IssueEventPayload { get; set; }
        public global::Octokit.PullRequestEventPayload PullRequestEventPayload { get; set; }
        public global::Octokit.PullRequestReviewEventPayload PullRequestReviewEventPayload { get; set; }
        public global::Octokit.PullRequestCommentPayload PullRequestCommentPayload { get; set; }
        public global::Octokit.PushEventPayload PushEventPayload { get; set; }
        public global::Octokit.ReleaseEventPayload ReleaseEventPayload { get; set; }
        public global::Octokit.StatusEventPayload StatusEventPayload { get; set; }
        public global::Octokit.StarredEventPayload StarredEventPayload { get; set; }
        public global::Octokit.ActivityPayload ActivityPayload { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
        public global::Octokit.User Actor { get; set; }
        public global::Octokit.Repository Repository{ get; set; }
        public global::Octokit.Organization Organization { get; set; }
    }
}