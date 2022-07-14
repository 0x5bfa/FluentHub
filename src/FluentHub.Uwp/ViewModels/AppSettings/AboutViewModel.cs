using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.System;

namespace FluentHub.Uwp.ViewModels.AppSettings
{
    public class AboutViewModel : ObservableObject
    {
        public AboutViewModel(ILogger logger = null)
        {
            _logger = logger;

            CopyVersionCommand = new RelayCommand(ExecuteCopyVersion);
            OpenLogsCommand = new AsyncRelayCommand(ExecuteOpenLogsAsync);
        }

        private readonly ILogger _logger;
        public string Version
        {
            get
            {
                string architecture = Windows.ApplicationModel.Package.Current.Id.Architecture.ToString();

#if DEBUG
                string buildConfiguration = "DEBUG";
#else
                string buildConfiguration = "RELEASE";
#endif

                return $"{App.AppVersion} | {architecture} | {buildConfiguration}";
            }
        }

        internal IRelayCommand CopyVersionCommand { get; }
        internal IAsyncRelayCommand OpenLogsCommand { get; }

        private void ExecuteCopyVersion()
        {
            try
            {
                var data = new DataPackage
                {
                    RequestedOperation = DataPackageOperation.Copy
                };

                data.SetText(Version);

                Clipboard.SetContentWithOptions(data, new ClipboardContentOptions() { IsAllowedInHistory = true, IsRoamable = true });
                Clipboard.Flush();
            }
            catch (Exception ex)
            {
                _logger?.Error($"Failed to copy data from copy version command, version: ${Version}", ex);
            }
        }

        private async Task ExecuteOpenLogsAsync()
        {
            string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs");
            var result = await Launcher.LaunchFolderPathAsync(logsFolder);
            _logger?.Info("Open logs folder result: {0}", result);
        }
    }
}
