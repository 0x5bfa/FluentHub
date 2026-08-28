// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class Activity
	{
		public ActivityKind Type { get; set; }

		public ActivityDetails Details { get; set; } = new();

		public bool Public { get; set; }

		public Repository? Repository { get; set; }

		public User? Actor { get; set; }

		public Organization? Organization { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Id { get; set; }
	}

	public enum ActivityKind
	{
		Unknown,
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
		ReleaseEvent,
		WatchEvent,
		StatusEvent,
	}

	public sealed class ActivityDetails
	{
		public CreateActivityDetails? CreateEvent { get; set; }

		public DeleteActivityDetails? DeleteEvent { get; set; }

		public ForkActivityDetails? ForkEvent { get; set; }

		public IssueCommentActivityDetails? IssueCommentEvent { get; set; }

		public IssueActivityDetails? IssueEvent { get; set; }

		public PullRequestCommentActivityDetails? PullRequestCommentEvent { get; set; }

		public PullRequestActivityDetails? PullRequestEvent { get; set; }

		public PushActivityDetails? PushEvent { get; set; }

		public ReleaseActivityDetails? ReleaseEvent { get; set; }

		public StarredActivityDetails? StarredEvent { get; set; }
	}

	public sealed class CreateActivityDetails
	{
		public string? Ref { get; set; }

		public string? MasterBranch { get; set; }

		public string? Description { get; set; }
	}

	public sealed class DeleteActivityDetails
	{
		public string? Ref { get; set; }
	}

	public sealed class ForkActivityDetails
	{
		public Repository? Forkee { get; set; }
	}

	public sealed class IssueCommentActivityDetails
	{
		public string? Action { get; set; }

		public Issue? Issue { get; set; }

		public IssueComment? Comment { get; set; }
	}

	public sealed class IssueActivityDetails
	{
		public string? Action { get; set; }

		public Issue? Issue { get; set; }
	}

	public sealed class PullRequestCommentActivityDetails
	{
		public string? Action { get; set; }

		public PullRequest? PullRequest { get; set; }

		public PullRequestReviewComment? Comment { get; set; }
	}

	public sealed class PullRequestActivityDetails
	{
		public string? Action { get; set; }

		public int Number { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public sealed class PushActivityDetails
	{
		public string? Head { get; set; }

		public string? Ref { get; set; }

		public int Size { get; set; }

		public List<ActivityCommit> Commits { get; set; } = [];
	}

	public sealed class ActivityCommit
	{
		public User User { get; set; } = new();

		public string Sha { get; set; } = string.Empty;

		public string Message { get; set; } = string.Empty;
	}

	public sealed class ReleaseActivityDetails
	{
		public string? Action { get; set; }

		public Release? Release { get; set; }

		public User? Sender { get; set; }
	}

	public sealed class StarredActivityDetails
	{
		public string? Action { get; set; }
	}
}
