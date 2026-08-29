// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Infrastructure.GitHub.Queries;

internal static class ProjectV2Query
{
	public const string Selection = """
		projectsV2(first: $first, after: $after, last: $last, before: $before) {
		  edges {
		    node {
		      closed closedAt createdAt id number public readme resourcePath
		      shortDescription title updatedAt url viewerCanUpdate
		    }
		  }
		  pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
		}
		""";

	public static PageResult<ProjectV2> ToPage(ProjectV2Connection connection)
		=> new(
			connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [],
			connection.PageInfo);
}
