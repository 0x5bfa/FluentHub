// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class Notification
	{
		public long Id { get; set; }

		public Repository? Repository { get; set; }

		public NotificationSubject Subject { get; set; } = new();

		public string? Reason { get; set; }

		public bool Unread { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public DateTimeOffset LastReadAt { get; set; }

		public string? LastReadAtHumanized { get; set; }

		public string? Url { get; set; }
	}

	public sealed class NotificationSubject
	{
		public NotificationSubjectType Type { get; set; }

		public string? TypeHumanized { get; set; }

		public int Number { get; set; }

		public string? Title { get; set; }
	}

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
