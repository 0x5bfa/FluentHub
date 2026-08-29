// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class RepositoryQueries
	{
		private const string PageQuery = """
			query UserRepositories($login: String!, $first: Int, $after: String, $last: Int, $before: String, $affiliations: [RepositoryAffiliation], $isArchived: Boolean, $isFork: Boolean, $isLocked: Boolean, $orderBy: RepositoryOrder, $ownerAffiliations: [RepositoryAffiliation], $privacy: RepositoryPrivacy) {
			  result: user(login: $login) {
			    repositories(first: $first, after: $after, last: $last, before: $before, affiliations: $affiliations, isArchived: $isArchived, isFork: $isFork, isLocked: $isLocked, orderBy: $orderBy, ownerAffiliations: $ownerAffiliations, privacy: $privacy) {
			""" + RepositoryListQuery.Connection + """
			    }
			  }
			}
			""" + RepositoryListQuery.Fields;

		private const string LanguagesQuery = """
			query UserRepositoryLanguages($login: String!, $after: String) {
			  result: user(login: $login) {
			    repositories(first: 100, after: $after) {
			""" + RepositoryListQuery.LanguageConnection + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public RepositoryQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<Repository>> GetPageAsync(
			string login,
			PageRequest page,
			IEnumerable<RepositoryAffiliation?>? affiliations = null,
			bool? isArchived = null,
			bool? isFork = null,
			bool? isLocked = null,
			RepositoryOrder? orderBy = null,
			IEnumerable<RepositoryAffiliation?>? ownerAffiliations = null,
			RepositoryPrivacy? privacy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				PageQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					RepositoryListQuery.WriteRepositoryFilters(writer, page, affiliations, isArchived, isFork, isLocked, orderBy, ownerAffiliations, privacy);
				},
				cancellationToken);
			return RepositoryListQuery.ToPage(response.Result?.Repositories
				?? throw new InvalidDataException("GitHub returned an incomplete user repositories response."));
		}

		public Task<IReadOnlyList<Repository>> SearchAllAsync(
			string login,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken = default)
			=> new UserRepositorySearchQueries(_gitHub).GetAllAsync(login, filters, cancellationToken);

		public async Task<IReadOnlyList<string>> GetLanguagesAsync(
			string login,
			CancellationToken cancellationToken = default)
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
				var connection = response.Result?.Repositories
					?? throw new InvalidDataException("GitHub returned an incomplete repository languages response.");
				RepositoryListQuery.AddLanguages(languages, connection.Nodes);
				cursor = connection.PageInfo.HasNextPage ? connection.PageInfo.EndCursor : null;
			}
			while (cursor is not null);

			return languages.OrderBy(language => language, StringComparer.OrdinalIgnoreCase).ToList();
		}
	}
}
