using System.IO;
using Windows.Storage;

namespace FluentHub.Octokit
{
    internal class App
    {
        #region properties
        public static ProductHeaderValue ProductInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; set; }

        public static OctokitOriginal.GitHubClient Client { get; set; }
            = new OctokitOriginal.GitHubClient(new OctokitOriginal.ProductHeaderValue("FluentHub"));
        #endregion

        public App()
        {
        }
    }
}
