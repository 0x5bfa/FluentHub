using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class DiffQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public DiffQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<CommitChanges> GetCommitAsync(
			string owner,
			string name,
			string refs,
			CancellationToken cancellationToken = default)
		{
			var commit = await _gitHub.RunRestAsync(
				(client, token) => client.Repositories.GetCommitAsync(owner, name, refs, token),
				cancellationToken);

			return new CommitChanges
			{
				Files = commit.Files?.Select(Map).ToList() ?? [],
			};
		}

		public async Task<IReadOnlyList<FileChange>> GetPullRequestFilesAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var files = await _gitHub.RunRestAsync(
				(client, token) => client.Repositories.GetPullRequestFilesAsync(owner, name, number, token),
				cancellationToken);

			return files.Select(Map).ToList();
		}

		private static FileChange Map(OctokitRest.GitHubFileChange file)
			=> new()
			{
				Additions = file.Additions,
				Changes = file.Changes,
				Deletions = file.Deletions,
				BlobUrl = file.BlobUrl ?? string.Empty,
				ContentsUrl = file.ContentsUrl ?? string.Empty,
				Filename = file.Filename ?? string.Empty,
				Patch = file.Patch ?? string.Empty,
				PreviousFileName = file.PreviousFilename,
				RawUrl = file.RawUrl ?? string.Empty,
				Sha = file.Sha ?? string.Empty,
				Status = file.Status ?? string.Empty,
			};
	}
}
