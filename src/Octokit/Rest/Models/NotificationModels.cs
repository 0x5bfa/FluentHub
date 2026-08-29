// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.Rest;

public sealed class NotificationRequest
{
	public bool All { get; init; }

	public bool Participating { get; init; }

	public DateTimeOffset? Since { get; init; }

	public DateTimeOffset? Before { get; init; }
}

public sealed class GitHubNotification
{
	public string Id { get; init; } = string.Empty;

	public bool Unread { get; init; }

	public string? Reason { get; init; }

	public DateTimeOffset UpdatedAt { get; init; }

	public DateTimeOffset? LastReadAt { get; init; }

	public string? Url { get; init; }

	public GitHubNotificationSubject? Subject { get; init; }

	public GitHubRepository? Repository { get; init; }
}

public sealed class GitHubNotificationSubject
{
	public string? Title { get; init; }

	public string? Url { get; init; }

	public string? Type { get; init; }
}
