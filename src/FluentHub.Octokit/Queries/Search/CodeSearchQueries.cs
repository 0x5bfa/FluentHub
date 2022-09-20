using Octokit;

namespace FluentHub.Octokit.Queries.Search;

public class CodeSearchQueries : ISearchQueries
{
    public async Task<List<CodeSearch>> GetAll(string query)
    {
        // you can also specify a search term here
        var request = new SearchCodeRequest(query);

        var result = await App.Client.Search.SearchCode(request);

        var searchResult = result.Items;

        List<CodeSearch> codeResultsList = new List<CodeSearch>();
        foreach (var x in searchResult)
        {
            codeResultsList.Add(new CodeSearch
            {
                Repo = x.Repository,
                Path = x.Path,
                Name = x.Name
            });
        }
        return codeResultsList;
    }
}