// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Infrastructure.GitHub.Queries;

internal static class PinnedRepositoryQuery
{
	public const string Fields = """
		fragment PinnedRepositoryFields on Repository {
		  name nameWithOwner description stargazerCount forkCount
		  isFork isInOrganization viewerHasStarred updatedAt
		  licenseInfo { name }
		  issues(states: OPEN) { totalCount }
		  pullRequests(states: OPEN) { totalCount }
		  owner { avatarUrl(size: 500) id login }
		  primaryLanguage { name color }
		}
		""";

	public const string Nodes = """
		nodes { ... on Repository { ...PinnedRepositoryFields } }
		""";

	public static List<Repository> ToList(IEnumerable<Repository?> items)
	{
		var repositories = items.Where(item => item is not null).Select(item => item!).ToList();
		foreach (var repository in repositories)
		{
			if (repository.UpdatedAt != default)
				repository.UpdatedAtHumanized = repository.UpdatedAt.ToRelativeTime();
		}
		return repositories;
	}
}
