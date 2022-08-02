namespace FluentHub.Octokit.Authorization
{
    public static class InitializeOctokit
    {
        public static void InitializeApiConnections(string accessToken)
        {
            // Octokit.NET
            App.Client.Credentials = new OctokitOriginal.Credentials(accessToken);

            // Octokit.GraphQL.NET
            App.Connection = new Connection(App.ProductInformation, accessToken);

            App.AccessToken = accessToken;
        }
    }
}
