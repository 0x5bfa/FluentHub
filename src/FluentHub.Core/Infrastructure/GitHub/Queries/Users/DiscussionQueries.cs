// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Discussions;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public partial class DiscussionQueries
	{
		[GeneratedGraphQLOperation<GraphQLResult<User>>]
		private const string Query = """
			query UserDiscussions($login: String!, $first: Int, $after: String, $last: Int, $before: String, $answered: Boolean, $orderBy: DiscussionOrder, $repositoryId: ID) {
			  result: user(login: $login) {
			    repositoryDiscussions(first: $first, after: $after, last: $last, before: $before, answered: $answered, orderBy: $orderBy, repositoryId: $repositoryId) {
			""" + DiscussionQuery.Connection + """
			    }
			  }
			}
			""" + DiscussionQuery.ListFields;

		private readonly IGitHubApiClient _gitHub;

		public DiscussionQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<Discussion>> GetPageAsync(
			string login,
			PageRequest page,
			DiscussionListFilters filters,
			CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetAuthorPageAsync(login, page, filters, cancellationToken);

		public Task<IReadOnlyList<string>> GetLabelNamesAsync(string login, CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetAuthorLabelNamesAsync(login, cancellationToken);

		public async Task<PageResult<Discussion>> GetPageAsync(
			string login,
			PageRequest page,
			bool? answered = null,
			DiscussionOrder? orderBy = null,
			ID? repositoryId = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			var response = await _gitHub.RunGraphQLAsync(
				QueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer =>
				{
					writer.WriteString("login", login);
					GraphQLInputWriter.WritePage(writer, page);
					GraphQLInputWriter.WriteOptionalBoolean(writer, "answered", answered);
					DiscussionQuery.WriteOrder(writer, orderBy);
					GraphQLInputWriter.WriteOptionalId(writer, "repositoryId", repositoryId);
				},
				cancellationToken);
			return DiscussionQuery.ToPage(response.Result?.RepositoryDiscussions
				?? throw new InvalidDataException("GitHub returned an incomplete user discussions response."));
		}
	}
}
