// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.Rest;

public sealed class GitHubUser
{
	public string Login { get; init; } = string.Empty;

	public string? AvatarUrl { get; init; }

	public string? Bio { get; init; }

	public string? Location { get; init; }

	public string? Name { get; init; }
}

public sealed class GitHubOrganization
{
	public string Login { get; init; } = string.Empty;

	public string? AvatarUrl { get; init; }

	public string? Description { get; init; }
}

public sealed class PageOptions
{
	public int StartPage { get; init; } = 1;

	public int PageCount { get; init; } = 1;

	public int PageSize { get; init; } = 30;

	internal void Validate()
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(StartPage, 1);
		ArgumentOutOfRangeException.ThrowIfLessThan(PageCount, 1);
		ArgumentOutOfRangeException.ThrowIfLessThan(PageSize, 1);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(PageSize, 100);
	}
}
