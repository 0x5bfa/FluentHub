namespace FluentHub.Octokit.Queries.Repositories
{
    public class DiffQueries
    {
        public async Task<OctokitOriginal.GitHubCommit> GetAllAsync(string owner, string name, string refs)
        {
            var commit = await App.Client.Repository.Commit.Get(owner, name, refs);

            return commit;
        }

        public async Task<List<OctokitOriginal.PullRequestFile>> GetAllAsync(string owner, string name, int number)
        {
            var files = await App.Client.Repository.PullRequest.Files(owner, name, number);

            return files.ToList();
        }
    }
}
