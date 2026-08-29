// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;

namespace FluentHub.Core.Infrastructure.GitHub.Queries;

internal static class RepositoryListQuery
{
	public const string Fields = """
		fragment RepositoryListFields on Repository {
		  name description stargazerCount forkCount hasSponsorshipsEnabled id
		  isArchived isFork isPrivate isInOrganization isMirror isTemplate pushedAt viewerHasStarred updatedAt
		  licenseInfo { name }
		  issues(states: OPEN) { totalCount }
		  pullRequests(states: OPEN) { totalCount }
		  owner { avatarUrl(size: 500) id login }
		  primaryLanguage { name color }
		}
		""";

	public const string Connection = """
		edges { node { ...RepositoryListFields } }
		pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
		""";

	public const string LanguageConnection = """
		nodes { primaryLanguage { name } }
		pageInfo { endCursor hasNextPage }
		""";

	public static void WriteRepositoryFilters(
		Utf8JsonWriter writer,
		PageRequest page,
		IEnumerable<RepositoryAffiliation?>? affiliations,
		bool? isArchived,
		bool? isFork,
		bool? isLocked,
		RepositoryOrder? orderBy,
		IEnumerable<RepositoryAffiliation?>? ownerAffiliations,
		RepositoryPrivacy? privacy)
	{
		Serialization.GraphQLInputWriter.WritePage(writer, page);
		WriteAffiliations(writer, "affiliations", affiliations);
		Serialization.GraphQLInputWriter.WriteOptionalBoolean(writer, "isArchived", isArchived);
		Serialization.GraphQLInputWriter.WriteOptionalBoolean(writer, "isFork", isFork);
		Serialization.GraphQLInputWriter.WriteOptionalBoolean(writer, "isLocked", isLocked);
		if (orderBy is not null)
		{
			writer.WriteStartObject("orderBy");
			writer.WriteString("field", ToGraphQL(orderBy.Field));
			writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
			writer.WriteEndObject();
		}
		WriteAffiliations(writer, "ownerAffiliations", ownerAffiliations);
		if (privacy is not null)
			writer.WriteString("privacy", privacy == RepositoryPrivacy.Public ? "PUBLIC" : "PRIVATE");
	}

	public static PageResult<Repository> ToPage(RepositoryConnection connection)
	{
		var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
		foreach (var repository in items)
			repository.UpdatedAtHumanized = repository.UpdatedAt.ToRelativeTime();
		return new(items, connection.PageInfo);
	}

	public static PageResult<Repository> ToPage(StarredRepositoryConnection connection)
	{
		var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node).ToList() ?? [];
		foreach (var repository in items)
			repository.UpdatedAtHumanized = repository.UpdatedAt.ToRelativeTime();
		return new(items, connection.PageInfo);
	}

	public static void AddLanguages(ISet<string> languages, IEnumerable<Repository?>? repositories)
	{
		foreach (var language in repositories?.Select(repository => repository?.PrimaryLanguage?.Name)
			.Where(name => !string.IsNullOrWhiteSpace(name)) ?? [])
		{
			languages.Add(language!);
		}
	}

	private static void WriteAffiliations(
		Utf8JsonWriter writer,
		string propertyName,
		IEnumerable<RepositoryAffiliation?>? affiliations)
	{
		if (affiliations is null)
			return;
		writer.WriteStartArray(propertyName);
		foreach (var affiliation in affiliations)
		{
			if (affiliation is not null)
				writer.WriteStringValue(ToGraphQL(affiliation.Value));
		}
		writer.WriteEndArray();
	}

	private static string ToGraphQL(RepositoryAffiliation affiliation)
		=> affiliation switch
		{
			RepositoryAffiliation.Owner => "OWNER",
			RepositoryAffiliation.Collaborator => "COLLABORATOR",
			RepositoryAffiliation.OrganizationMember => "ORGANIZATION_MEMBER",
			_ => throw new ArgumentOutOfRangeException(nameof(affiliation), affiliation, "Unknown repository affiliation."),
		};

	private static string ToGraphQL(RepositoryOrderField field)
		=> field switch
		{
			RepositoryOrderField.CreatedAt => "CREATED_AT",
			RepositoryOrderField.UpdatedAt => "UPDATED_AT",
			RepositoryOrderField.PushedAt => "PUSHED_AT",
			RepositoryOrderField.Name => "NAME",
			RepositoryOrderField.Stargazers => "STARGAZERS",
			_ => throw new ArgumentOutOfRangeException(nameof(field), field, "Unknown repository order field."),
		};
}
