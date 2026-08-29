// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;

namespace Octokit.Rest;

public sealed class GitHubRepository
{
	public string Name { get; init; } = string.Empty;

	public string? Description { get; init; }

	public int ForksCount { get; init; }

	public int StargazersCount { get; init; }

	public int OpenIssuesCount { get; init; }

	public DateTimeOffset UpdatedAt { get; init; }

	public GitHubUser? Owner { get; init; }
}

public sealed class GitHubRepositoryIdentity
{
	public string Name { get; init; } = string.Empty;

	public string FullName { get; init; } = string.Empty;

	public GitHubUser? Owner { get; init; }
}

public sealed class GitHubCommit
{
	public List<GitHubFileChange>? Files { get; init; }
}

public sealed class GitHubFileChange
{
	public int Additions { get; init; }

	public int Changes { get; init; }

	public int Deletions { get; init; }

	public string? BlobUrl { get; init; }

	public string? ContentsUrl { get; init; }

	public string? Filename { get; init; }

	public string? Patch { get; init; }

	public string? PreviousFilename { get; init; }

	public string? RawUrl { get; init; }

	public string? Sha { get; init; }

	public string? Status { get; init; }
}

public sealed class GitReferenceName
{
	public string Name { get; init; } = string.Empty;
}

public sealed class GitHubReadme
{
	public string? Content { get; init; }

	public string? Encoding { get; init; }
}

public sealed class CreateForkOptions
{
	public string? Organization { get; init; }

	public required string Name { get; init; }

	public bool DefaultBranchOnly { get; init; }
}

public sealed class UpdateRepositoryRequest
{
	public required string Description { get; init; }
}

public sealed class RepositoryIssueType
{
	public string Name { get; init; } = string.Empty;
}
