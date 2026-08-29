// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class ActivityClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public async Task<IReadOnlyList<GitHubActivityEvent>> GetReceivedEventsAsync(
		string login,
		int pageSize = 60,
		int page = 1,
		CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(pageSize, 100);
		ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);

		return await Transport.GetAsync(
			$"users/{Segment(login, nameof(login))}/received_events?per_page={pageSize}&page={page}",
			GitHubRestJsonContext.Default.ListGitHubActivityEvent,
			cancellationToken).ConfigureAwait(false);
	}
}
