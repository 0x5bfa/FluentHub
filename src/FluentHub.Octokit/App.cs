using Serilog;
using System.IO;

namespace FluentHub.Octokit
{
    public static class App
    {
        public static ProductHeaderValue ProductInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; set; }
        public static OctokitOriginal.GitHubClient Client { get; set; }
            = new OctokitOriginal.GitHubClient(new OctokitOriginal.ProductHeaderValue("FluentHub"));

        public static string AccessToken { get; set; }

        public static bool InitializedOctokit { get; set; }
        public static string SignedInUserName { get; set; }
    }
}
