using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Activity
    {
        // Payloads
        //public global::Octokit.CheckRunEventPayload CheckRunEventPayload { get; set; }
        //public global::Octokit.CheckSuiteEventPayload CheckSuiteEventPayload { get; set; }
        //public global::Octokit.CommitCommentPayload CommitCommentPayload { get; set; }
        //public global::Octokit.CreateEventPayload CreateEventPayload { get; set; }
        //public global::Octokit.DeleteEventPayload DeleteEventPayload { get; set; }
        //public global::Octokit.ForkEventPayload ForkEventPayload { get; set; }
        //public global::Octokit.IssueCommentPayload IssueCommentPayload { get; set; }
        //public global::Octokit.IssueEventPayload IssueEventPayload { get; set; }
        //public global::Octokit.PullRequestEventPayload PullRequestEventPayload { get; set; }
        //public global::Octokit.PullRequestReviewEventPayload PullRequestReviewEventPayload { get; set; }
        //public global::Octokit.PullRequestCommentPayload PullRequestCommentPayload { get; set; }
        //public global::Octokit.PushEventPayload PushEventPayload { get; set; }
        //public global::Octokit.ReleaseEventPayload ReleaseEventPayload { get; set; }
        //public global::Octokit.StatusEventPayload StatusEventPayload { get; set; }
        //public global::Octokit.StarredEventPayload StarredEventPayload { get; set; }
        //public global::Octokit.ActivityPayload ActivityPayload { get; set; }

        public object Payload { get; set; }

        public string PayloadType { get; set; }

        public global::Octokit.User Actor { get; set; }
        public global::Octokit.Repository Repository{ get; set; }
        public global::Octokit.Organization Organization { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}