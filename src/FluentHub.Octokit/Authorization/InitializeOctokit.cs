using System.Net.Http.Headers;

namespace FluentHub.Octokit.Authorization
{
    public static class InitializeOctokit
    {
        public static void InitializeApiConnections(string accessToken)
        {
            App.AccessToken = accessToken;

            // Octokit.NET
            App.Client.Credentials = new OctokitV3.Credentials(accessToken);

            // Octokit.GraphQL.NET
            App.Connection = new Connection(App.ProductInformation, accessToken);

            // GraphQL.NET
            App.GraphQLHttpClient.HttpClient.DefaultRequestHeaders.Clear();
            App.GraphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {accessToken}");
            App.GraphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("User-Agent", "FluentHub");
        }
    }
}
