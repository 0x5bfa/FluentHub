// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;

namespace FluentHub.Core.Infrastructure.GitHub.Queries;

internal static class DiscussionQuery
{
	public const string ListFields = """
		fragment DiscussionListFields on Discussion {
		  answerChosenAt id locked number title updatedAt upvoteCount url
		  viewerCanDelete viewerDidAuthor viewerHasUpvoted
		  category { emoji id }
		  repository { name owner { avatarUrl(size: 500) id login } }
		}
		""";

	public const string Connection = """
		edges { node { ...DiscussionListFields } }
		pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
		""";

	public static void WriteOrder(Utf8JsonWriter writer, DiscussionOrder? orderBy)
	{
		if (orderBy is null)
			return;
		writer.WriteStartObject("orderBy");
		writer.WriteString("field", orderBy.Field == DiscussionOrderField.CreatedAt ? "CREATED_AT" : "UPDATED_AT");
		writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
		writer.WriteEndObject();
	}

	public static PageResult<Discussion> ToPage(DiscussionConnection connection)
	{
		var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
		foreach (var discussion in items)
			discussion.UpdatedAtHumanized = discussion.UpdatedAt.ToRelativeTime();
		return new(items, connection.PageInfo);
	}
}
