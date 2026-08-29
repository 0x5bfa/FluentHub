// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class StarredRepoQueries
	{
		private const string PageQuery = """
			query StarredRepositories($login: String!, $first: Int, $after: String, $last: Int, $before: String, $orderBy: StarOrder, $ownedByViewer: Boolean) {
			  result: user(login: $login) {
			    starredRepositories(first: $first, after: $after, last: $last, before: $before, orderBy: $orderBy, ownedByViewer: $ownedByViewer) {
			""" + RepositoryListQuery.Connection + """
			    }
			  }
			}
			""" + RepositoryListQuery.Fields;

		private const string LanguagesQuery = """
			query StarredRepositoryLanguages($login: String!, $after: String) {
			  result: user(login: $login) {
			    starredRepositories(first: 100, after: $after) {
			""" + RepositoryListQuery.LanguageConnection + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public StarredRepoQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<Repository>> GetPageAsync(
			string login,
			PageRequest page,
			StarOrder? orderBy = null,
			bool? ownedByViewer = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				PageQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
					if (orderBy is not null)
					{
						writer.WriteStartObject("orderBy");
						writer.WriteString("field", "STARRED_AT");
						writer.WriteString("direction", orderBy.Direction == OrderDirection.Asc ? "ASC" : "DESC");
						writer.WriteEndObject();
					}
					GraphQLInputWriter.WriteOptionalBoolean(writer, "ownedByViewer", ownedByViewer);
				},
				cancellationToken);
			return RepositoryListQuery.ToPage(response.Result?.StarredRepositories
				?? throw new InvalidDataException("GitHub returned an incomplete starred repositories response."));
		}

		public async Task<IReadOnlyList<Repository>> GetAllAsync(string login, CancellationToken cancellationToken = default)
		{
			var repositories = new List<Repository>();
			PageRequest? page = PageRequest.Forward(100);
			var order = new StarOrder { Direction = OrderDirection.Desc, Field = StarOrderField.StarredAt };
			do
			{
				var result = await GetPageAsync(login, page, order, cancellationToken: cancellationToken);
				repositories.AddRange(result.Items);
				page = result.PageInfo.HasNextPage && !string.IsNullOrEmpty(result.PageInfo.EndCursor)
					? PageRequest.Forward(100, result.PageInfo.EndCursor)
					: null;
			}
			while (page is not null);
			return repositories;
		}

		public async Task<IReadOnlyList<string>> GetLanguagesAsync(string login, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string? cursor = null;
			do
			{
				var response = await _gitHub.RunGraphQLAsync(
					LanguagesQuery,
					GitHubGraphQLJsonContext.Default.GraphQLResultUser,
					writer =>
					{
						writer.WriteString("login", login);
						GraphQLInputWriter.WriteOptionalString(writer, "after", cursor);
					},
					cancellationToken);
				var connection = response.Result?.StarredRepositories
					?? throw new InvalidDataException("GitHub returned an incomplete starred repository languages response.");
				RepositoryListQuery.AddLanguages(languages, connection.Nodes);
				cursor = connection.PageInfo.HasNextPage ? connection.PageInfo.EndCursor : null;
			}
			while (cursor is not null);
			return languages.OrderBy(language => language, StringComparer.OrdinalIgnoreCase).ToList();
		}
	}
}
