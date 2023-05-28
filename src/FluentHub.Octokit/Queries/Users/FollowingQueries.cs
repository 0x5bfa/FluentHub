namespace FluentHub.Octokit.Queries.Users
{
    public class FollowingQueries
    {
        public async Task<List<User>> GetAllAsync(string login)
        {
            #region query
            var query = new Query()
                    .User(login)
                    .Following(first: 30)
                    .Nodes
                    .Select(x => new User
                    {
                        AvatarUrl = x.AvatarUrl(500),
                        Name = x.Name,
                        Bio = x.Bio,
                        Login = x.Login,
                        Id = x.Id,
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
