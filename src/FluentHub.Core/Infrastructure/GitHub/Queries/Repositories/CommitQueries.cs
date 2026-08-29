// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class CommitQueries
	{
		private const string CommitFields = """
			fragment CommitListFields on Commit {
			  abbreviatedOid additions changedFilesIfAvailable committedDate deletions message messageHeadline oid
			  author { avatarUrl(size: 500) user { login } }
			  repository { name owner { login } }
			  signature {
			    isValid payload signature state wasSignedByGitHub
			    signer { avatarUrl(size: 500) login }
			  }
			}
			""";

		private const string PageQuery = """
			query CommitHistory($owner: String!, $name: String!, $ref: String!, $first: Int, $after: String, $last: Int, $before: String, $author: CommitAuthor, $path: String, $since: GitTimestamp, $until: GitTimestamp) {
			  result: repository(owner: $owner, name: $name) {
			    ref(qualifiedName: $ref) {
			      target {
			        ... on Commit {
			          history(first: $first, after: $after, last: $last, before: $before, author: $author, path: $path, since: $since, until: $until) {
			            edges { node { ...CommitListFields } }
			            pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			          }
			        }
			      }
			    }
			  }
			}
			""" + CommitFields;

		private const string LatestQuery = """
			query LatestCommit($owner: String!, $name: String!, $ref: String!, $path: String!) {
			  result: repository(owner: $owner, name: $name) {
			    ref(qualifiedName: $ref) {
			      target {
			        ... on Commit {
			          history(first: 1, path: $path) {
			            nodes {
			              abbreviatedOid additions changedFilesIfAvailable committedDate deletions message messageHeadline oid
			              author { avatarUrl(size: 500) user { login } }
			            }
			            totalCount
			          }
			        }
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public CommitQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<PageResult<Commit>> GetPageAsync(
			string owner,
			string name,
			string refs,
			PageRequest page,
			CommitAuthor? author = null,
			string? path = null,
			string? since = null,
			string? until = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);
			path = string.IsNullOrEmpty(path) ? "." : path;

			var response = await _gitHub.RunGraphQLAsync(
				PageQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryRefResult,
				writer =>
				{
					WriteRepositoryRef(writer, owner, name, refs);
					GraphQLInputWriter.WritePage(writer, page);
					if (author is not null)
					{
						writer.WriteStartObject("author");
						GraphQLInputWriter.WriteOptionalId(writer, "id", author.Id);
						GraphQLInputWriter.WriteOptionalStrings(writer, "emails", author.Emails);
						writer.WriteEndObject();
					}
					GraphQLInputWriter.WriteOptionalString(writer, "path", path);
					GraphQLInputWriter.WriteOptionalString(writer, "since", since);
					GraphQLInputWriter.WriteOptionalString(writer, "until", until);
				},
				cancellationToken);
			var connection = response.Result?.Ref?.Target?.History
				?? throw new InvalidDataException("GitHub returned an incomplete commit history response.");
			var commits = connection.Edges?.Where(edge => edge?.Node is not null).Select(edge => edge!.Node!).ToList() ?? [];
			foreach (var commit in commits)
				commit.CommittedDateHumanized = commit.CommittedDate.ToRelativeTime();
			return new(commits, connection.PageInfo);
		}

		public async Task<Commit> GetLatestAsync(
			string name,
			string owner,
			string refs,
			string path,
			CancellationToken cancellationToken = default)
		{
			path = string.IsNullOrEmpty(path) ? "." : path;
			var response = await _gitHub.RunGraphQLAsync(
				LatestQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryRefResult,
				writer =>
				{
					WriteRepositoryRef(writer, owner, name, refs);
					writer.WriteString("path", path);
				},
				cancellationToken);
			var commit = response.Result?.Ref?.Target
				?? throw new InvalidDataException("GitHub returned an incomplete latest commit response.");
			foreach (var item in commit.History?.Nodes?.Where(item => item is not null).Select(item => item!) ?? [])
				item.CommittedDateHumanized = item.CommittedDate.ToRelativeTime();
			return commit;
		}

		private static void WriteRepositoryRef(
			System.Text.Json.Utf8JsonWriter writer,
			string owner,
			string name,
			string refs)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
			writer.WriteString("ref", refs);
		}
	}
}
