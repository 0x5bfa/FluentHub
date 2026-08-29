// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Organizations
{
	public partial class RepositoryQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<Organization>>]
		private const string PageQuery = """
			query OrganizationRepositories($login: String!, $first: Int, $after: String, $last: Int, $before: String, $isArchived: Boolean, $isFork: Boolean, $isLocked: Boolean, $orderBy: RepositoryOrder, $privacy: RepositoryPrivacy) {
			  result: organization(login: $login) {
			    repositories(first: $first, after: $after, last: $last, before: $before, isArchived: $isArchived, isFork: $isFork, isLocked: $isLocked, orderBy: $orderBy, privacy: $privacy) {
			""" + RepositoryListQuery.Connection + """
			    }
			  }
			}
			""" + RepositoryListQuery.Fields;

		[GeneratedGraphQLOperation<GraphQLResult<Organization>>]
		private const string LanguagesQuery = """
			query OrganizationRepositoryLanguages($login: String!, $after: String) {
			  result: organization(login: $login) {
			    repositories(first: 100, after: $after) {
			""" + RepositoryListQuery.LanguageConnection + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public RepositoryQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<Repository>> GetAllAsync(string organization, CancellationToken cancellationToken = default)
			=> (await GetPageAsync(organization, PageRequest.Forward(30), cancellationToken: cancellationToken)).Items.ToList();

		public async Task<PageResult<Repository>> GetPageAsync(
			string organization,
			PageRequest page,
			bool? isArchived = null,
			bool? isFork = null,
			bool? isLocked = null,
			RepositoryOrder? orderBy = null,
			RepositoryPrivacy? privacy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(organization);
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				PageQueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultOrganization,
				writer =>
				{
					writer.WriteString("login", organization);
					RepositoryListQuery.WriteRepositoryFilters(writer, page, null, isArchived, isFork, isLocked, orderBy, null, privacy);
				},
				cancellationToken);
			return RepositoryListQuery.ToPage(response.Result?.Repositories
				?? throw new InvalidDataException("GitHub returned an incomplete organization repositories response."));
		}

		public Task<IReadOnlyList<Repository>> SearchAllAsync(
			string organization,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken = default)
			=> new UserRepositorySearchQueries(_gitHub).GetOrganizationAllAsync(organization, filters, cancellationToken);

		public async Task<IReadOnlyList<string>> GetLanguagesAsync(
			string organization,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(organization);
			var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string? cursor = null;
			do
			{
				var response = await _gitHub.RunGraphQLAsync(
					LanguagesQueryOperation,
					GitHubGraphQLJsonContext.Default.GraphQLResultOrganization,
					writer =>
					{
						writer.WriteString("login", organization);
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
