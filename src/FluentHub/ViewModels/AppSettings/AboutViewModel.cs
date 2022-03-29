using FluentHub.Backend;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.System;

namespace FluentHub.ViewModels.AppSettings
{
    public class AboutViewModel
    {
        public AboutViewModel(ILogger logger = null)
        {
            _logger = logger;

            CopyVersionCommand = new RelayCommand(ExecuteCopyVersionCommand);
            OpenLogsCommand = new AsyncRelayCommand(ExecuteOpenLogsCommandAsync);
        }

        private readonly ILogger _logger;
        public string Version
        {
            get
            {
                string architecture = Package.Current.Id.Architecture.ToString();

#if DEBUG
                string buildConfiguration = "DEBUG";
#else
                string buildConfiguration = "RELEASE";
#endif

                return $"{App.AppVersion} | {architecture} | {buildConfiguration}";
            }
        }

        internal IRelayCommand CopyVersionCommand { get; }

        private void ExecuteCopyVersionCommand()
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

        internal IAsyncRelayCommand OpenLogsCommand { get; }

        private async Task ExecuteOpenLogsCommandAsync()
        {
            string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs");
            var result = await Launcher.LaunchFolderPathAsync(logsFolder);
            _logger?.Info("Open logs folder result: {0}", result);
        }
    }
}