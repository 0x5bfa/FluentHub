using Octokit;
namespace FluentHub.Octokit.Queries.Search;

public class UserSearchQueries : ISearchQueries
{
    public async Task<List<UserSearch>> GetAll(String query)
    {
        // you can also specify a search term here
        var request = new SearchUsersRequest(query);

        var result = await App.Client.Search.SearchUsers(request);

        var searchResult = result.Items;

        List<UserSearch> userList = new List<UserSearch>();
        foreach (var x in searchResult)
        {
            userList.Add(new UserSearch
            {
                RealName = x.Name,
                Username = x.Login,
                Email = x.Email,
                Description = x.Bio,
                Location = x.Location,
                Avatar = x.AvatarUrl
            });
        }
        return userList;
    }
}