using Serilog;
using System.IO;
using Windows.Storage;

namespace FluentHub.Octokit
{
    internal class App
    {
        private static ProductHeaderValue productInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; private set; }
        public static OctokitOriginal.GitHubClient Client { get; private set; }
            = new OctokitOriginal.GitHubClient(new OctokitOriginal.ProductHeaderValue("FluentHub"));
        public static string AccessToken { get; private set; }
        public static bool InitializedOctokit { get; private set; }
        public static string SignedInUserName { get; set; }

        public App()
        {
            if (InitializedOctokit == false)
            {
                InitializeLogger();
                InitializeOctokit();
            }
        }

        public void InitializeOctokit()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                AccessToken = localSettings.Values["AccessToken"] as string;
                SignedInUserName = localSettings.Values["SignedInUserName"] as string;

                Connection = new Connection(productInformation, AccessToken);
                Client.Credentials = new OctokitOriginal.Credentials(AccessToken);

                InitializedOctokit = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                InitializedOctokit = false;
            }
        }

        private void InitializeLogger()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Octokit.Logs/Log.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
