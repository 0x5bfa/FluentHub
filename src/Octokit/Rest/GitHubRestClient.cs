// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class GitHubRestClient
{
	public GitHubRestClient(GitHubHttpClient transport)
	{
		ArgumentNullException.ThrowIfNull(transport);

		Users = new UsersClient(transport);
		Organizations = new OrganizationsClient(transport);
		Activity = new ActivityClient(transport);
		Notifications = new NotificationsClient(transport);
		Repositories = new RepositoriesClient(transport);
		Search = new SearchClient(transport);
	}

	public UsersClient Users { get; }

	public OrganizationsClient Organizations { get; }

	public ActivityClient Activity { get; }

	public NotificationsClient Notifications { get; }

	public RepositoriesClient Repositories { get; }

	public SearchClient Search { get; }
}
