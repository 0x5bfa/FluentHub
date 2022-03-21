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
        private static ProductHeaderValue productInformation { get; set; }
        public static Connection Connection { get; private set; }

        public App()
        {
            IntializeLogger();

            productInformation = new ProductHeaderValue("FluentHub", "1.0.0.0");
            Connection = new Connection(productInformation, GetTokenFromApp());
        }

        public string GetTokenFromApp()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                if (localSettings.Values["AccessToken"] == null)
                {
                    localSettings.Values["AccessToken"] = "";
                }

                object value = localSettings.Values["AccessToken"];

                // Check type mismatch
                if (!(value is string))
                {
                    throw new ArgumentException("Type mismatch for \"" + "AccessToken" + "\" in local store. Got " + value.GetType());
                }

                return (string)value;
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
