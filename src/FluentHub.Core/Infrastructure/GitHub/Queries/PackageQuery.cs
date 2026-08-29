// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries;

internal static class PackageQuery
{
	public const string Selection = """
		edges {
		  node {
		    id name packageType
		    latestVersion { version }
		    repository { name owner { avatarUrl(size: 500) login } }
		    statistics { downloadsTotalCount }
		  }
		}
		pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
		""";

	public const string NodesSelection = """
		nodes {
		  id name packageType
		  latestVersion { version }
		  repository { name owner { avatarUrl(size: 500) login } }
		  statistics { downloadsTotalCount }
		}
		""";

	public static void WriteFilters(
		Utf8JsonWriter writer,
		PageRequest page,
		IEnumerable<string>? names,
		PackageOrder? orderBy,
		PackageType? packageType,
		ID? repositoryId)
	{
		GraphQLInputWriter.WritePage(writer, page);
		GraphQLInputWriter.WriteOptionalStrings(writer, "names", names);
		if (orderBy is not null)
		{
			writer.WriteStartObject("orderBy");
			if (orderBy.Field is not null)
				writer.WriteString("field", "CREATED_AT");
			if (orderBy.Direction is not null)
				writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
			writer.WriteEndObject();
		}
		if (packageType is not null)
			writer.WriteString("packageType", ToGraphQL(packageType.Value));
		GraphQLInputWriter.WriteOptionalId(writer, "repositoryId", repositoryId);
	}

	public static PageResult<Package> ToPage(PackageConnection connection)
		=> new(
			connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [],
			connection.PageInfo);

	private static string ToGraphQL(PackageType type)
		=> type switch
		{
			PackageType.Debian => "DEBIAN",
			PackageType.Maven => "MAVEN",
			PackageType.Npm => "NPM",
			PackageType.Nuget => "NUGET",
			PackageType.Pypi => "PYPI",
			PackageType.Rubygems => "RUBYGEMS",
			PackageType.Docker => "DOCKER",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown package type."),
		};
}
