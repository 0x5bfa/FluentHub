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
				client => client.Repository.Commit.Get(owner, name, refs),
				cancellationToken);

			return new CommitChanges
			{
				Files = commit.Files.Select(Map).ToList(),
			};
		}

		public async Task<IReadOnlyList<FileChange>> GetPullRequestFilesAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var files = await _gitHub.RunRestAsync(
				client => client.Repository.PullRequest.Files(owner, name, number),
				cancellationToken);

			return files.Select(Map).ToList();
		}

		private static FileChange Map(OctokitV3.GitHubCommitFile file)
			=> new()
			{
				Additions = file.Additions,
				Changes = file.Changes,
				Deletions = file.Deletions,
				BlobUrl = file.BlobUrl,
				ContentsUrl = file.ContentsUrl,
				Filename = file.Filename,
				Patch = file.Patch,
				PreviousFileName = file.PreviousFileName,
				RawUrl = file.RawUrl,
				Sha = file.Sha,
				Status = file.Status,
			};

		private static FileChange Map(OctokitV3.PullRequestFile file)
			=> new()
			{
				Additions = file.Additions,
				Changes = file.Changes,
				Deletions = file.Deletions,
				BlobUrl = file.BlobUrl,
				ContentsUrl = file.ContentsUrl,
				Filename = file.FileName,
				Patch = file.Patch,
				PreviousFileName = file.PreviousFileName,
				RawUrl = file.RawUrl,
				Sha = file.Sha,
				Status = file.Status,
			};
	}
}
