// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class UsersClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public Task<GitHubUser> GetAuthenticatedAsync(CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			"user",
			GitHubRestJsonContext.Default.GitHubUser,
			cancellationToken);
}
