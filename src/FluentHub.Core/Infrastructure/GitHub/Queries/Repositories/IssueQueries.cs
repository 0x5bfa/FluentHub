// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class IssueQueries
	{
		private const string ReactionFields = """
			reactionGroups { content viewerHasReacted reactors { totalCount } }
		""";

		private const string ItemQuery = """
			query Issue($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    issue(number: $number) {
			      authorAssociation body closed createdAt id lastEditedAt number state stateReason title updatedAt url
			      viewerCanClose: viewerCanUpdate viewerCanLabel: viewerCanUpdate viewerCanReact
			      viewerCanReopen: viewerCanUpdate viewerCanSubscribe viewerCanUpdate viewerDidAuthor viewerSubscription
			      author { avatarUrl(size: 500) login }
			      assignees(first: 6) { nodes { avatarUrl(size: 500) id login } }
			      comments { totalCount }
			      labels(first: 10) { nodes { color description id name } }
			      milestone { id title progressPercentage }
			      participants(first: 6) { nodes { avatarUrl(size: 500) login } }
			""" + ReactionFields + """
			      repository { name viewerPermission owner { avatarUrl(size: 500) id login } }
			    }
			  }
			}
			""";

		private const string BodyQuery = """
			query IssueBody($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    issue(number: $number) {
			      authorAssociation body createdAt id lastEditedAt updatedAt url
			      viewerCanReact viewerCanUpdate viewerDidAuthor
			      author { login avatarUrl(size: 500) }
			""" + ReactionFields + """
			    }
			  }
			}
			""";

		private const string PinnedQuery = """
			query PinnedIssues($owner: String!, $name: String!) {
			  result: repository(owner: $owner, name: $name) {
			    pinnedIssues(first: 3) {
			      nodes {
			        issue {
			          closed number title updatedAt
			          comments { totalCount }
			          labels(first: 10) { nodes { color description name } }
			        }
			        repository { name owner { avatarUrl(size: 500) id login } }
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public IssueQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<Issue>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			RepositoryItemListFilters? filters = null,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetIssuePageAsync(owner, name, page, filters ?? new(), cancellationToken);

		public Task<IReadOnlyList<string>> GetAuthorLoginsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetAuthorLoginsAsync(owner, name, false, cancellationToken);

		public Task<IReadOnlyList<string>> GetIssueTypeNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetIssueTypeNamesAsync(owner, name, cancellationToken);

		public async Task<Issue> GetAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await ExecuteRepositoryAsync(ItemQuery, owner, name, number, cancellationToken);
			var issue = response.Issue
				?? throw new InvalidDataException($"GitHub issue '{owner}/{name}#{number}' was not found.");
			issue.CreatedAtHumanized = issue.CreatedAt.ToRelativeTime();
			issue.UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime();
			return issue;
		}

		public async Task<IssueComment> GetBodyAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				BodyQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryBodyResult,
				writer => WriteVariables(writer, owner, name, number),
				cancellationToken);
			var body = response.Result?.Issue
				?? throw new InvalidDataException($"GitHub issue '{owner}/{name}#{number}' was not found.");
			StampBody(body);
			return body;
		}

		public async Task<List<Issue>> GetPinnedAllAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				PinnedQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					writer.WriteString("owner", owner);
					writer.WriteString("name", name);
				},
				cancellationToken);
			var pinned = response.Result?.PinnedIssues?.Nodes?.Where(item => item is not null).Select(item => item!).ToList() ?? [];
			var issues = new List<Issue>(pinned.Count);
			foreach (var item in pinned)
			{
				item.Issue.Repository = item.Repository;
				item.Issue.UpdatedAtHumanized = item.Issue.UpdatedAt.ToRelativeTime();
				issues.Add(item.Issue);
			}
			return issues;
		}

		private async Task<Repository> ExecuteRepositoryAsync(
			string query,
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				query,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer => WriteVariables(writer, owner, name, number),
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException($"GitHub repository '{owner}/{name}' was not found.");
		}

		private static void WriteVariables(System.Text.Json.Utf8JsonWriter writer, string owner, string name, int number)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
			writer.WriteNumber("number", number);
		}

		private static void StampBody(IssueComment body)
		{
			body.CreatedAtHumanized = body.CreatedAt.ToRelativeTime();
			body.UpdatedAtHumanized = body.UpdatedAt.ToRelativeTime();
		}
	}
}
