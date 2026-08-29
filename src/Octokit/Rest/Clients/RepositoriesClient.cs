// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text;
using Octokit.Transport;

namespace Octokit.Rest;

public sealed class RepositoriesClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public Task<GitHubCommit> GetCommitAsync(
		string owner,
		string name,
		string reference,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"{RepositoryEndpoint(owner, name)}/commits/{Segment(reference, nameof(reference))}",
			GitHubRestJsonContext.Default.GitHubCommit,
			cancellationToken);

	public Task<IReadOnlyList<GitHubFileChange>> GetPullRequestFilesAsync(
		string owner,
		string name,
		int number,
		CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(number, 1);
		var endpoint = RepositoryEndpoint(owner, name);
		return GetAllPagesAsync(
			page => $"{endpoint}/pulls/{number}/files?per_page=100&page={page}",
			GitHubRestJsonContext.Default.ListGitHubFileChange,
			cancellationToken);
	}

	public Task<IReadOnlyList<GitReferenceName>> GetBranchesAsync(
		string owner,
		string name,
		CancellationToken cancellationToken = default)
	{
		var endpoint = RepositoryEndpoint(owner, name);
		return GetAllPagesAsync(
			page => $"{endpoint}/branches?per_page=100&page={page}",
			GitHubRestJsonContext.Default.ListGitReferenceName,
			cancellationToken);
	}

	public Task<IReadOnlyList<GitReferenceName>> GetTagsAsync(
		string owner,
		string name,
		CancellationToken cancellationToken = default)
	{
		var endpoint = RepositoryEndpoint(owner, name);
		return GetAllPagesAsync(
			page => $"{endpoint}/tags?per_page=100&page={page}",
			GitHubRestJsonContext.Default.ListGitReferenceName,
			cancellationToken);
	}

	public async Task<string> GetReadmeMarkdownAsync(
		string owner,
		string name,
		CancellationToken cancellationToken = default)
	{
		var response = await Transport.GetAsync(
			$"{RepositoryEndpoint(owner, name)}/readme",
			GitHubRestJsonContext.Default.GitHubReadme,
			cancellationToken).ConfigureAwait(false);

		if (!string.Equals(response.Encoding, "base64", StringComparison.OrdinalIgnoreCase))
			throw new InvalidDataException($"GitHub returned unsupported README encoding '{response.Encoding}'.");

		try
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(response.Content ?? string.Empty));
		}
		catch (FormatException exception)
		{
			throw new InvalidDataException("GitHub returned invalid base64 README content.", exception);
		}
	}

	public Task<GitHubRepositoryIdentity> CreateForkAsync(
		string owner,
		string name,
		CreateForkOptions options,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentException.ThrowIfNullOrWhiteSpace(options.Name);

		return Transport.PostAsync(
			$"{RepositoryEndpoint(owner, name)}/forks",
			options,
			GitHubRestJsonContext.Default.CreateForkOptions,
			GitHubRestJsonContext.Default.GitHubRepositoryIdentity,
			cancellationToken);
	}

	public Task<GitHubRepositoryIdentity> UpdateDescriptionAsync(
		string owner,
		string name,
		string description,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(description);

		return Transport.PatchAsync(
			RepositoryEndpoint(owner, name),
			new UpdateRepositoryRequest { Description = description },
			GitHubRestJsonContext.Default.UpdateRepositoryRequest,
			GitHubRestJsonContext.Default.GitHubRepositoryIdentity,
			cancellationToken);
	}

	public Task<List<RepositoryIssueType>> GetIssueTypesAsync(
		string owner,
		string name,
		CancellationToken cancellationToken = default)
		=> Transport.GetAsync(
			$"{RepositoryEndpoint(owner, name)}/issue-types",
			GitHubRestJsonContext.Default.ListRepositoryIssueType,
			cancellationToken);

	private static string RepositoryEndpoint(string owner, string name)
		=> $"repos/{Segment(owner, nameof(owner))}/{Segment(name, nameof(name))}";
}
