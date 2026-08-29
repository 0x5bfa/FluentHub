// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class OrganizationsClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public Task<IReadOnlyList<GitHubOrganization>> GetForAuthenticatedAsync(
		CancellationToken cancellationToken = default)
		=> GetAllPagesAsync(
			page => $"user/orgs?per_page=100&page={page}",
			GitHubRestJsonContext.Default.ListGitHubOrganization,
			cancellationToken);
}
