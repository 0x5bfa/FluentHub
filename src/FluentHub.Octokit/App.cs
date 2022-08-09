using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace FluentHub.Octokit
{
    public class App
    {
        public static ProductHeaderValue ProductInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; set; }

        public static OctokitV3.GitHubClient Client { get; set; }
            = new OctokitV3.GitHubClient(new OctokitV3.ProductHeaderValue("FluentHub"));


        public static GraphQLHttpClient GraphQLHttpClient { get; set; }
            = new("https://api.github.com/graphql", new NewtonsoftJsonSerializer());

        public static string AccessToken { get; set; }
    }
}
