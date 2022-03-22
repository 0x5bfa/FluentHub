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
        private static ProductHeaderValue productInformation { get; set; } = new ProductHeaderValue("FluentHub", "1.0.0.0");
        public static Connection Connection { get; private set; }
        public static global::Octokit.GitHubClient Client { get; private set; }
            = new global::Octokit.GitHubClient(new global::Octokit.ProductHeaderValue("FluentHub"));
        public static string AccessToken { get; private set; }

        public App()
        {
            if (Log.Logger == null)
            {
                IntializeLogger();
            }

            if (Connection == null)
            {
                Connection= new Connection(productInformation, GetTokenFromApp());
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
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs/Log.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("Initialized logger in FluentHub.Octokit.");
        }
    }
}
