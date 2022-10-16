namespace FluentHub.Octokit.Searches
{
    public class RepositorySearches
    {
        public async Task<List<Repository>> GetAllAsync(string term)
        {
            var request = new OctokitV3.SearchRepositoriesRequest(term);
            var response = await App.Client.Search.SearchRepo(request);

            List<Repository> result = new();

            foreach (var item in response.Items)
            {
                result.Add(new Repository
                {
                    Name = item.Name,
                    Description = item.Description,
                    ForkCount = item.ForksCount,
                    StargazerCount = item.StargazersCount,
                    UpdatedAt = item.UpdatedAt,
                    UpdatedAtHumanized = item.UpdatedAt.Humanize(null, null),

                    Issues = new()
                    {
                        TotalCount = item.OpenIssuesCount,
                    },

                    Owner = new RepositoryOwner()
                    {
                        AvatarUrl = item.Owner.AvatarUrl,
                        Login = item.Owner.Login,
                    },
                });
            }

            return result;
        }
    }
}
