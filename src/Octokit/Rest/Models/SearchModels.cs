// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.Rest;

public sealed class SearchResponse<T>
{
	public int TotalCount { get; init; }

	public bool IncompleteResults { get; init; }

	public List<T> Items { get; init; } = [];
}

public sealed class GitHubCodeSearchItem
{
	public string? Name { get; init; }

	public string? Path { get; init; }

	public GitHubRepository? Repository { get; init; }
}

public sealed class GitHubIssueSearchItem
{
	public DateTimeOffset? ClosedAt { get; init; }

	public DateTimeOffset CreatedAt { get; init; }

	public string? Title { get; init; }

	public int Number { get; init; }

	public GitHubUser? User { get; init; }

	public int Comments { get; init; }

	public List<GitHubLabel> Labels { get; init; } = [];
}

public sealed class GitHubLabel
{
	public string? Color { get; init; }

	public string? Name { get; init; }
}
