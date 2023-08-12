using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Models.v3
{
	public enum ActivityPayloadType
	{
		CheckRunEvent,

		CheckSuiteEvent,

		CommitComment,

		CreateEvent,

		DeleteEvent,

		ForkEvent,

		IssueCommentEvent,

		IssueEvent,

		PullRequestComment,

		PullRequestEvent,

		PullRequestReviewEvent,

		PushEvent,

		PushWebhookCommit,

		PushWebhookCommitter,

		PushWebhook,

		ReleaseEvent,

		WatchEvent,

		StatusEvent,
	}
}
