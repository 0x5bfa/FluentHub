// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.Rest;

public sealed class GitHubActivityEvent
{
	public string Type { get; init; } = string.Empty;

	public bool Public { get; init; }

	public GitHubUser? Actor { get; init; }

	public GitHubActivityRepository? Repo { get; init; }

	public GitHubOrganization? Org { get; init; }

	public DateTimeOffset CreatedAt { get; init; }

	public string Id { get; init; } = string.Empty;

	public GitHubActivityPayload? Payload { get; init; }
}

public sealed class GitHubActivityRepository
{
	public string Name { get; init; } = string.Empty;
}

public sealed class GitHubActivityPayload
{
	public string? Action { get; init; }

	public string? Description { get; init; }

	public string? MasterBranch { get; init; }

	public string? Ref { get; init; }

	public string? Head { get; init; }

	public int Size { get; init; }

	public List<GitHubActivityCommit>? Commits { get; init; }

	public GitHubRepository? Forkee { get; init; }

	public GitHubActivityComment? Comment { get; init; }

	public GitHubActivityIssue? Issue { get; init; }

	public GitHubActivityPullRequest? PullRequest { get; init; }

	public GitHubActivityRelease? Release { get; init; }
}

public sealed class GitHubActivityCommit
{
	public string? Sha { get; init; }

	public string? Message { get; init; }

	public GitHubActivityCommitAuthor? Author { get; init; }
}

public sealed class GitHubActivityCommitAuthor
{
	public string? Name { get; init; }
}

public sealed class GitHubActivityComment
{
	public string? Body { get; init; }
}

public sealed class GitHubActivityIssue
{
	public DateTimeOffset? ClosedAt { get; init; }

	public int Number { get; init; }

	public string? Title { get; init; }

	public DateTimeOffset? UpdatedAt { get; init; }
}

public sealed class GitHubActivityPullRequest
{
	public DateTimeOffset? ClosedAt { get; init; }

	public int Number { get; init; }

	public string? Title { get; init; }

	public DateTimeOffset UpdatedAt { get; init; }

	public bool Draft { get; init; }

	public bool Merged { get; init; }
}

public sealed class GitHubActivityRelease
{
	public string? Name { get; init; }

	public string? Body { get; init; }
}
