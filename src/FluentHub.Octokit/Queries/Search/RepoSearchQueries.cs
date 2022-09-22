using Octokit;

namespace FluentHub.Octokit.Queries.Search;

public class RepoSearchQueries : ISearchQueries
{
    public async Task<List<RepositorySearch>> GetAll(string query)
    {
        // you can also specify a search term here
        var request = new SearchRepositoriesRequest(query);

        var result = await App.Client.Search.SearchRepo(request);

        var searchResult = result.Items;

        List<RepositorySearch> repoList = new List<RepositorySearch>();
        foreach (var x in searchResult)
        {
            if (x.License != null) {
                repoList.Add(new RepositorySearch
                {
                    Name = x.Name,
                    Description = x.Description,
                    UpdatedAt = x.UpdatedAt,
                    IsArchived = x.Archived,
                    LicenseInfo = x.License.Name,
                    Owner = x.Owner.Login
                });
            }
            else
            {
                repoList.Add(new RepositorySearch
                {
                    Name = x.Name,
                    Description = x.Description,
                    UpdatedAt = x.UpdatedAt,
                    IsArchived = x.Archived,
                    LicenseInfo = "",
                    Owner = x.Owner.Login
                });
            }
        }
        return repoList;
    }
}