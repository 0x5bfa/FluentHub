// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class ReleaseQueries
	{
		private const string ReleaseFields = """
			fragment ReleaseFields on Release {
			  author { login avatarUrl(size: 500) }
			  description descriptionHTML isDraft isLatest isPrerelease name publishedAt tagName
			}
			""";

		private const string PageQuery = """
			query Releases($owner: String!, $name: String!, $first: Int, $after: String, $last: Int, $before: String, $orderBy: ReleaseOrder) {
			  result: repository(owner: $owner, name: $name) {
			    releases(first: $first, after: $after, last: $last, before: $before, orderBy: $orderBy) {
			      edges { node { ...ReleaseFields } }
			      pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			    }
			  }
			}
			""" + ReleaseFields;

		private const string ItemQuery = """
			query Release($owner: String!, $name: String!, $tagName: String!) {
			  result: repository(owner: $owner, name: $name) {
			    release(tagName: $tagName) {
			      ...ReleaseFields
			      releaseAssets(first: 10) { nodes { name contentType downloadCount downloadUrl size } }
			      tagCommit { abbreviatedOid }
			    }
			  }
			}
			""" + ReleaseFields;

		private readonly IGitHubApiClient _gitHub;

		public ReleaseQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<Release>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			ReleaseOrder? orderBy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				PageQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					WriteRepository(writer, owner, name);
					GraphQLInputWriter.WritePage(writer, page);
					writer.WriteStartObject("orderBy");
					writer.WriteString("direction", orderBy?.Direction == OrderDirection.Asc ? "ASC" : "DESC");
					if (orderBy?.Field is not null)
						writer.WriteString("field", orderBy.Field == ReleaseOrderField.Name ? "NAME" : "CREATED_AT");
					writer.WriteEndObject();
				},
				cancellationToken);
			var connection = response.Result?.Releases
				?? throw new InvalidDataException("GitHub returned an incomplete releases response.");
			var items = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
			foreach (var release in items)
				release.PublishedAtHumanized = release.PublishedAt.ToRelativeTime();
			return new(items, connection.PageInfo);
		}

		public async Task<Release> GetAsync(
			string owner,
			string name,
			string tagName,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				ItemQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					WriteRepository(writer, owner, name);
					writer.WriteString("tagName", tagName);
				},
				cancellationToken);
			var release = response.Result?.Release
				?? throw new InvalidDataException($"GitHub release '{owner}/{name}:{tagName}' was not found.");
			release.PublishedAtHumanized = release.PublishedAt.ToRelativeTime();
			return release;
		}

		private static void WriteRepository(System.Text.Json.Utf8JsonWriter writer, string owner, string name)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
		}
	}
}
