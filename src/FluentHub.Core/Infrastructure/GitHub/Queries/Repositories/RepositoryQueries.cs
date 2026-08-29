// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class RepositoryQueries
	{
		private const string RepositoryDetailsCacheCategory = "repository-details-v2";

		private const string SummaryFields = """
			fragment RepositorySummaryFields on Repository {
			  name description stargazerCount forkCount isFork isInOrganization viewerHasStarred updatedAt
			  licenseInfo { name }
			  issues(states: OPEN) { totalCount }
			  pullRequests(states: OPEN) { totalCount }
			  owner { avatarUrl(size: 500) id login }
			  primaryLanguage { name color }
			}
			""";

		private const string DetailsFields = """
			fragment RepositoryDetailsFields on Repository {
			  id homepageUrl forkingAllowed hasIssuesEnabled hasProjectsEnabled isArchived isEmpty isPrivate isTemplate
			  viewerSubscription name description stargazerCount forkCount isFork isInOrganization viewerHasStarred viewerPermission updatedAt
			  licenseInfo { name }
			  defaultBranchRef { name }
			  watchers { totalCount }
			  releases { totalCount }
			  issues(states: OPEN) { totalCount }
			  pullRequests(states: OPEN) { totalCount }
			  owner { avatarUrl(size: 500) id login }
			  latestRelease {
			    description descriptionHTML isDraft isLatest isPrerelease name publishedAt
			    author { login avatarUrl(size: 500) }
			  }
			  languages(first: 10) { nodes { color name } }
			}
			""";

		private const string SummaryQuery = """
			query RepositorySummary($owner: String!, $name: String!) {
			  result: repository(owner: $owner, name: $name) { ...RepositorySummaryFields }
			}
			""" + SummaryFields;

		private const string DetailsQuery = """
			query RepositoryDetails($owner: String!, $name: String!) {
			  result: repository(owner: $owner, name: $name) { ...RepositoryDetailsFields }
			}
			""" + DetailsFields;

		private const string CustomDetailsQuery = """
			query RepositoryCodeDetails($owner: String!, $name: String!) {
			  result: repository(owner: $owner, name: $name) {
			    ...RepositoryDetailsFields
			    heads: refs(refPrefix: "refs/heads/") { totalCount }
			    tags: refs(refPrefix: "refs/tags/") { totalCount }
			  }
			}
			""" + DetailsFields;

		private const string RefCountsQuery = """
			query RepositoryRefCounts($owner: String!, $name: String!) {
			  result: repository(owner: $owner, name: $name) {
			    heads: refs(refPrefix: "refs/heads/") { totalCount }
			    tags: refs(refPrefix: "refs/tags/") { totalCount }
			  }
			}
			""";

		private const string IssueOptionsQuery = """
			query RepositoryIssueOptions($owner: String!, $name: String!, $states: [MilestoneState!]) {
			  result: repository(owner: $owner, name: $name) {
			    assignableUsers(first: 100) { nodes { avatarUrl(size: 500) id login name } }
			    labels(first: 100) { nodes { color description id name } }
			    milestones(first: 100, states: $states) { nodes { id progressPercentage title } }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public RepositoryQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public Task<Repository> GetAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetUncachedAsync(owner, name, cancellationToken);
			return _cache.GetOrCreateAsync(
				CreateRepositoryKey("repositories", owner, name),
				CachePolicies.Repository,
				GitHubCacheSerializers.Repository,
				token => GetUncachedAsync(owner, name, token),
				cancellationToken);
		}

		private async Task<Repository> GetUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			var repository = await GetRepositoryAsync(SummaryQuery, owner, name, cancellationToken);
			StampRepository(repository);
			return repository;
		}

		public Task<Repository> GetDetailsAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetDetailsUncachedAsync(owner, name, cancellationToken);
			return _cache.GetOrCreateAsync(
				CreateRepositoryKey(RepositoryDetailsCacheCategory, owner, name),
				CachePolicies.Repository,
				GitHubCacheSerializers.Repository,
				token => GetDetailsUncachedAsync(owner, name, token),
				cancellationToken);
		}

		private async Task<Repository> GetDetailsUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			var repository = await GetRepositoryAsync(DetailsQuery, owner, name, cancellationToken);
			StampRepository(repository);
			return repository;
		}

		public Task InvalidateAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return Task.CompletedTask;
			return Task.WhenAll(
				_cache.RemoveAsync(CreateRepositoryKey("repositories", owner, name), cancellationToken),
				_cache.RemoveAsync(CreateRepositoryKey("repository-details", owner, name), cancellationToken),
				_cache.RemoveAsync(CreateRepositoryKey(RepositoryDetailsCacheCategory, owner, name), cancellationToken));
		}

		public async Task<CustomRepositoryResponseForCodePage> GetCustomDetailsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				CustomDetailsQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryDetailsResult,
				writer => WriteRepository(writer, owner, name),
				cancellationToken);
			var repository = response.Result
				?? throw new InvalidDataException($"GitHub repository '{owner}/{name}' was not found.");
			StampRepository(repository);
			return new()
			{
				Repository = repository,
				BranchesTotalCount = repository.Heads?.TotalCount ?? 0,
				TagsTotalCount = repository.Tags?.TotalCount ?? 0,
			};
		}

		public async Task<(int, int)> GetBranchAndTagCountAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				RefCountsQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryDetailsResult,
				writer => WriteRepository(writer, owner, name),
				cancellationToken);
			return (response.Result?.Heads?.TotalCount ?? 0, response.Result?.Tags?.TotalCount ?? 0);
		}

		public Task<Repository> GetIssueOptionsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> GetIssueOptionsAsync(owner, name, openOnly: true, cancellationToken);

		public Task<Repository> GetIssueListOptionsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> GetIssueOptionsAsync(owner, name, openOnly: false, cancellationToken);

		private async Task<Repository> GetIssueOptionsAsync(
			string owner,
			string name,
			bool openOnly,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				IssueOptionsQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer =>
				{
					WriteRepository(writer, owner, name);
					if (openOnly)
					{
						writer.WriteStartArray("states");
						writer.WriteStringValue("OPEN");
						writer.WriteEndArray();
					}
				},
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException($"GitHub repository '{owner}/{name}' was not found.");
		}

		public async Task<(IReadOnlyList<string> Branches, IReadOnlyList<string> Tags)> GetBranchAndTagNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			return await _gitHub.RunRestAsync(async (client, token) =>
			{
				var branchesTask = client.Repositories.GetBranchesAsync(owner, name, token);
				var tagsTask = client.Repositories.GetTagsAsync(owner, name, token);
				await Task.WhenAll(branchesTask, tagsTask);
				return (
					Branches: (IReadOnlyList<string>)(await branchesTask).Select(branch => branch.Name).Where(value => !string.IsNullOrWhiteSpace(value)).Distinct(StringComparer.Ordinal).ToList(),
					Tags: (IReadOnlyList<string>)(await tagsTask).Select(tag => tag.Name).Where(value => !string.IsNullOrWhiteSpace(value)).Distinct(StringComparer.Ordinal).ToList());
			}, cancellationToken);
		}

		public Task<string> GetReadmeMarkdownAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetReadmeMarkdownUncachedAsync(owner, name, cancellationToken);
			return _cache.GetOrCreateAsync(
				CreateRepositoryKey("repository-readme", owner, name),
				CachePolicies.Repository,
				CacheSerializers.String,
				token => GetReadmeMarkdownUncachedAsync(owner, name, token),
				cancellationToken);
		}

		private async Task<string> GetReadmeMarkdownUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			try
			{
				return await _gitHub.RunRestAsync(
					(client, token) => client.Repositories.GetReadmeMarkdownAsync(owner, name, token),
					cancellationToken);
			}
			catch (global::Octokit.Transport.GitHubApiException exception)
				when (exception.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return string.Empty;
			}
		}

		private async Task<Repository> GetRepositoryAsync(
			string query,
			string owner,
			string name,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				query,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer => WriteRepository(writer, owner, name),
				cancellationToken);
			return response.Result
				?? throw new InvalidDataException($"GitHub repository '{owner}/{name}' was not found.");
		}

		private static void StampRepository(Repository repository)
		{
			repository.UpdatedAtHumanized = repository.UpdatedAt.ToRelativeTime();
			if (repository.LatestRelease is { } release)
				release.PublishedAtHumanized = release.PublishedAt.ToRelativeTime();
		}

		private static void WriteRepository(System.Text.Json.Utf8JsonWriter writer, string owner, string name)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
		}

		private CacheKey CreateRepositoryKey(string category, string owner, string name)
			=> CacheKey.ForAccount(_gitHub.CachePartition, category, $"{owner.Trim().ToLowerInvariant()}/{name.Trim().ToLowerInvariant()}");

		private static void ValidateRepository(string owner, string name)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(owner);
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
		}
	}
}
