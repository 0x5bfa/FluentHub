using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentHub.Octokit
{
    internal class App
    {
        private static ProductHeaderValue productInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; private set; }
        public static global::Octokit.GitHubClient Client { get; private set; }
            = new global::Octokit.GitHubClient(new global::Octokit.ProductHeaderValue("FluentHub"));
        public static string AccessToken { get; private set; }
        public static bool InitializedLogger { get; private set; }

        public App()
        {
            if (InitializedLogger == false)
            {
                IntializeLogger();
                AccessToken = GetTokenFromApp();
            }

            if (Connection == null)
            {
                Connection= new Connection(productInformation, AccessToken);
                Client.Credentials = new global::Octokit.Credentials(AccessToken);
            }
        }

        public string GetTokenFromApp()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                string value = localSettings.Values["AccessToken"] as string;

                return AccessToken = value;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        private void IntializeLogger()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Octokit.Logs/Log.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            InitializedLogger = true;
        }
    }
}
