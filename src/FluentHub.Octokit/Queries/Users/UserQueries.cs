namespace FluentHub.Octokit.Queries.Users
{
    public class UserQueries
    {
        public UserQueries() => new App();

        public async Task<User> GetAsync(string login)
        {
            var response = await App.Client.User.Get(login);

            var mapped = UserMappings.Map(response);

            return mapped;
        }

        public async Task<string> GetViewerLogin()
        {
            var current = await App.Client.User.Current();

            return current.Login;
        }
    }
}
