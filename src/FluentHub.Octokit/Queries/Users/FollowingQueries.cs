namespace FluentHub.Octokit.Queries.Users
{
    public class FollowingQueries
    {
        public FollowingQueries() => new App();

        public async Task<List<Models.User>> GetAllAsync(string login)
        {
            #region query
            var query = new Query()
                    .User(login)
                    .Following(first: 30)
                    .Nodes
                    .Select(x => new User
                    {
                        AvatarUrl = x.AvatarUrl(100),
                        Name = x.Name,
                        Bio = x.Bio,
                        Login = x.Login,
                        IsOrganization = UserTypeDetectionHelper.IsOrganization(x.Id),
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
