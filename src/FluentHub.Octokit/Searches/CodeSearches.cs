namespace FluentHub.Octokit.Searches
{
    public class CodeSearches
    {
        public async Task<List<Models.v3.Searches.SearchCode>> GetAllAsync(string term)
        {
            var request = new OctokitV3.SearchCodeRequest(term);
            var response = await App.Client.Search.SearchCode(request);

            List<Models.v3.Searches.SearchCode> result = new();

            foreach (var item in response.Items)
            {
                result.Add(new()
                {
                    Name = item.Name,
                    Path = item.Path,

                    Repository = new()
                    {
                        Name = item.Repository.Name,

                        Owner = new RepositoryOwner()
                        {
                            AvatarUrl = item.Repository.Owner.AvatarUrl,
                            Login = item.Repository.Owner.Login,
                        }
                    },
                });
            }

            return result;
        }
    }
}
