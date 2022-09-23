using Octokit;

namespace FluentHub.Octokit.Queries.Search;

public class IssueSearchQueries : ISearchQueries
{
    public async Task<List<IssueSearch>> GetAll(string query)
    {
        // you can also specify a search term here
        var request = new SearchIssuesRequest(query);

        var result = await App.Client.Search.SearchIssues(request);

        var searchResult = result.Items;

        List<IssueSearch> issueList = new List<IssueSearch>();
        foreach (var x in searchResult)
        {
            issueList.Add(new IssueSearch
            {
                State = x.State,
                Body = x.Body,
                Author = x.User,
                Repo = x.Repository,
                Name = x.Title,
                Id = x.Id,
                Comments = x.Comments,
                CreatedAt = x.CreatedAt,
                Labels = x.Labels
            });
        }
        return issueList;
    }
}