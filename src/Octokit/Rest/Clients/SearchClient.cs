// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class SearchClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public Task<SearchResponse<GitHubCodeSearchItem>> SearchCodeAsync(
		string query,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"search/code?q={QueryValue(query, nameof(query))}",
			GitHubRestJsonContext.Default.SearchResponseGitHubCodeSearchItem,
			cancellationToken);

	public Task<SearchResponse<GitHubIssueSearchItem>> SearchIssuesAsync(
		string query,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"search/issues?q={QueryValue(query, nameof(query))}",
			GitHubRestJsonContext.Default.SearchResponseGitHubIssueSearchItem,
			cancellationToken);

	public Task<SearchResponse<GitHubRepository>> SearchRepositoriesAsync(
		string query,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"search/repositories?q={QueryValue(query, nameof(query))}",
			GitHubRestJsonContext.Default.SearchResponseGitHubRepository,
			cancellationToken);

	public Task<SearchResponse<GitHubUser>> SearchUsersAsync(
		string query,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"search/users?q={QueryValue(query, nameof(query))}",
			GitHubRestJsonContext.Default.SearchResponseGitHubUser,
			cancellationToken);
}
