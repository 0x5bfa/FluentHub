// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Infrastructure.GitHub.Clients
{
	public interface IFluentHubGitHubClient
	{
		OrganizationApiClient Organizations { get; }

		RepositoryApiClient Repositories { get; }

		UserApiClient Users { get; }

		SearchApiClient Searches { get; }

		MutationApiClient Mutations { get; }
	}
}
